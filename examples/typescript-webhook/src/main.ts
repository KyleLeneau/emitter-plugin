import 'reflect-metadata';
import { NestFactory } from '@nestjs/core';
import { FastifyAdapter, NestFastifyApplication } from '@nestjs/platform-fastify';
import { AppModule } from './app.module';

async function bootstrap() {
  const app = await NestFactory.create<NestFastifyApplication>(AppModule, new FastifyAdapter());

  // Fastify only parses `application/json` out of the box. Cloud events are sent as
  // `application/cloudevents+json`, so treat any `+json` content type as JSON too.
  app.getHttpAdapter().getInstance().addContentTypeParser(
    /^application\/(.+\+)?json$/,
    { parseAs: 'string' },
    (_req, body: string, done) => {
      try {
        done(null, JSON.parse(body));
      } catch (err) {
        done(err as Error, undefined);
      }
    },
  );

  await app.listen(8000, '0.0.0.0');
  console.log('Listening on http://0.0.0.0:8000');
}

bootstrap();
