#!/usr/bin/env node
'use strict';

// Builds the NINA plugin manifests file: a JSON array of manifest objects, one per
// published GitHub release, newest first. Each entry is the manifest template with
// dynamic fields (Installer.Checksum, Installer.URL, ChangelogURL, Version) filled in
// from the release's own data. See docs/adr/003-github-manifests.md.
//
// Draft and prerelease releases are excluded. A release is only included if it has a
// `<version>-Emitter-Release.zip` asset with a sibling `<same>.zip.sha256` checksum
// asset (both produced by NINAPlugin.targets during `dotnet build -c Release`); a
// release missing either fails the action rather than being silently skipped.
//
// Zero npm dependencies by design (Node 24's built-in `fetch`/`structuredClone` are
// enough) so there's nothing to install or bundle before this action can run.

const fs = require('fs');
const path = require('path');

const ZIP_SUFFIX = '-Emitter-Release.zip';
const VERSION_RE = /^([0-9]+)\.([0-9]+)\.([0-9]+)\.([0-9]+)-Emitter-Release\.zip$/;

function getInput(name, fallback) {
  const value = process.env[`INPUT_${name.toUpperCase()}`];
  return value && value.length > 0 ? value : fallback;
}

function fail(message) {
  console.log(`::error::${message}`);
  process.exit(1);
}

function parseNextLink(linkHeader) {
  if (!linkHeader) return null;
  for (const part of linkHeader.split(',')) {
    const match = part.match(/<([^>]+)>;\s*rel="next"/);
    if (match) return match[1];
  }
  return null;
}

async function githubFetch(url, token) {
  const headers = {
    Accept: 'application/vnd.github+json',
    'X-GitHub-Api-Version': '2022-11-28',
    'User-Agent': 'emitter-plugin-build-manifests',
  };
  if (token) headers.Authorization = `Bearer ${token}`;

  const response = await fetch(url, { headers });
  if (!response.ok) {
    throw new Error(`GitHub API request to ${url} failed: ${response.status} ${response.statusText}`);
  }
  return response;
}

// Fetches every release for the repo, following pagination via the Link header.
async function fetchAllReleases(apiUrl, repo, token) {
  const releases = [];
  let url = `${apiUrl}/repos/${repo}/releases?per_page=100`;
  while (url) {
    const response = await githubFetch(url, token);
    releases.push(...(await response.json()));
    url = parseNextLink(response.headers.get('link'));
  }
  return releases;
}

async function fetchChecksum(url, token) {
  const response = await githubFetch(url, token);
  const text = await response.text();
  return text.trim().split(/\s+/)[0];
}

async function main() {
  const outputPath = getInput('output_path', 'docs/pages/plugins/manifests');
  const templatePath = getInput('template_path', '.github/manifest_template.json');
  const token = process.env.GITHUB_TOKEN || process.env.GH_TOKEN || '';
  const repo = process.env.GITHUB_REPOSITORY;
  const apiUrl = process.env.GITHUB_API_URL || 'https://api.github.com';

  if (!repo) fail('GITHUB_REPOSITORY is not set (expected to run inside GitHub Actions).');

  const template = JSON.parse(fs.readFileSync(templatePath, 'utf8'));

  console.log(`Fetching releases for ${repo}...`);
  const allReleases = await fetchAllReleases(apiUrl, repo, token);
  const releases = allReleases.filter((release) => !release.draft && !release.prerelease);

  fs.mkdirSync(path.dirname(outputPath), { recursive: true });

  if (releases.length === 0) {
    console.log(`No published releases found; writing empty manifests array to ${outputPath}`);
    fs.writeFileSync(outputPath, '[]\n');
    return;
  }

  const entries = [];
  for (const release of releases) {
    const tag = release.tag_name;
    const assets = release.assets || [];

    const zipAsset = assets.find((asset) => asset.name.endsWith(ZIP_SUFFIX));
    if (!zipAsset) fail(`Release ${tag} has no asset matching *${ZIP_SUFFIX}`);

    const shaName = `${zipAsset.name}.sha256`;
    const shaAsset = assets.find((asset) => asset.name === shaName);
    if (!shaAsset) fail(`Release ${tag} has no checksum asset named ${shaName}`);

    const checksum = await fetchChecksum(shaAsset.browser_download_url, token);
    if (!checksum) fail(`Failed to read checksum from ${shaName} for release ${tag}`);

    const match = zipAsset.name.match(VERSION_RE);
    if (!match) fail(`Could not parse Major.Minor.Patch.Build from asset name '${zipAsset.name}'`);
    const [, major, minor, patch, build] = match;

    const entry = structuredClone(template);
    entry.Installer.Checksum = checksum;
    entry.Installer.URL = zipAsset.browser_download_url;
    entry.ChangelogURL = release.html_url;
    entry.Version = { Major: major, Minor: minor, Patch: patch, Build: build };

    entries.push(entry);
    console.log(`Added manifest entry for ${tag}`);
  }

  fs.writeFileSync(outputPath, `${JSON.stringify(entries, null, 4)}\n`);
  console.log(`Wrote ${entries.length} manifest entries to ${outputPath}`);
}

main().catch((error) => fail(error.stack || String(error)));
