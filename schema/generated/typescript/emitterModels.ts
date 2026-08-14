// To parse this data:
//
//   import { Convert, DeviceConnectionData, CameraData, TelescopeData, ImagingData, WeatherDeviceInfoData, SafetyDeviceInfoData, SafetyChangeData, FlatPanelData, FilterWheelData } from "./file";
//
//   const deviceConnectionData = Convert.toDeviceConnectionData(json);
//   const cameraData = Convert.toCameraData(json);
//   const telescopeData = Convert.toTelescopeData(json);
//   const imagingData = Convert.toImagingData(json);
//   const weatherDeviceInfoData = Convert.toWeatherDeviceInfoData(json);
//   const safetyDeviceInfoData = Convert.toSafetyDeviceInfoData(json);
//   const safetyChangeData = Convert.toSafetyChangeData(json);
//   const flatPanelData = Convert.toFlatPanelData(json);
//   const filterWheelData = Convert.toFilterWheelData(json);
//
// These functions will throw an error if the JSON doesn't
// match the expected interface, even if the JSON is valid.

/**
 * General device connection data
 */
export interface DeviceConnectionData {
    connected: boolean;
    /**
     * Device description
     */
    description?: null | string;
    /**
     * Driver ID
     */
    device_id?:  null | string;
    device_type: DeviceType;
    /**
     * A friendly name of the device for dipslay
     */
    display_name?: null | string;
    /**
     * Driver name and info
     */
    driver_info?: null | string;
    /**
     * Driver version information
     */
    driver_version?: null | string;
    /**
     * The name of the device
     */
    name?: null | string;
    [property: string]: any;
}

export enum DeviceType {
    Camera = "Camera",
    Dome = "Dome",
    FilterWheel = "FilterWheel",
    FlatPanel = "FlatPanel",
    Focuser = "Focuser",
    Guider = "Guider",
    Mount = "Mount",
    Rotator = "Rotator",
    SafetyMonitor = "SafetyMonitor",
    Switch = "Switch",
    Weather = "Weather",
}

/**
 * Camera device state snapshot
 */
export interface CameraData {
    connected: boolean;
    cooler_on: boolean;
    /**
     * Cooler power as a percentage (0–100); null if unsupported by the device
     */
    cooler_power?: number | null;
    /**
     * UTC time when the current or last exposure ends; null if none
     */
    exposure_end_time?: Date | null;
    is_exposing:        boolean;
    /**
     * Duration of last image download in seconds
     */
    last_download_time: number;
    /**
     * Sensor temperature in degrees Celsius; null if unsupported by the device
     */
    temperature?: number | null;
    /**
     * Target cooler temperature in degrees Celsius; null if unsupported by the device
     */
    temperature_set_point?: number | null;
    [property: string]: any;
}

/**
 * Telescope mount state snapshot
 */
export interface TelescopeData {
    /**
     * Current altitude in decimal degrees; null if unsupported by the device
     */
    altitude?: number | null;
    /**
     * Current azimuth in decimal degrees (0–360); null if unsupported by the device
     */
    azimuth?:  number | null;
    connected: boolean;
    /**
     * Current Dec in decimal degrees (−90 to +90); null if unsupported by the device
     */
    declination?: number | null;
    /**
     * Current RA in decimal hours (0–24); null if unsupported by the device
     */
    right_ascension?: number | null;
    /**
     * Observer elevation in meters above sea level; null if unsupported by the device
     */
    site_elevation?: number | null;
    /**
     * Observer latitude in decimal degrees; null if unsupported by the device
     */
    site_latitude?: number | null;
    /**
     * Observer longitude in decimal degrees; null if unsupported by the device
     */
    site_longitude?:  number | null;
    slewing:          boolean;
    tracking_enabled: boolean;
    [property: string]: any;
}

/**
 * Image capture metadata at the time of save
 */
export interface ImagingData {
    /**
     * Sequential exposure number within the sequence
     */
    exposure_number: number;
    /**
     * Exposure duration in seconds
     */
    exposure_time: number;
    /**
     * Filter wheel filter name; null if no filter wheel
     */
    filter?: null | string;
    /**
     * Camera gain; −1 if not applicable
     */
    gain: number;
    /**
     * Image type — LIGHT, DARK, FLAT, BIAS, etc.; null if unknown
     */
    image_type?: null | string;
    /**
     * Camera offset; −1 if not applicable
     */
    offset: number;
    /**
     * Imaging target name from the sequence; null if not set
     */
    target_name?: null | string;
    [property: string]: any;
}

/**
 * Weather station sensor readings
 */
export interface WeatherDeviceInfoData {
    /**
     * Cloud cover as a percentage (0–100); null if unsupported by the device
     */
    cloud_cover?: number | null;
    connected:    boolean;
    /**
     * Dew point in degrees Celsius; null if unsupported by the device
     */
    dew_point?: number | null;
    /**
     * Relative humidity as a percentage (0–100); null if unsupported by the device
     */
    humidity?: number | null;
    /**
     * Atmospheric pressure in hPa; null if unsupported by the device
     */
    pressure?: number | null;
    /**
     * Rain rate in mm/h; null if unsupported by the device
     */
    rain_rate?: number | null;
    /**
     * Sky quality in magnitudes per square arcsecond; null if unsupported by the device
     */
    sky_quality?: number | null;
    /**
     * Sky temperature in degrees Celsius; null if unsupported by the device
     */
    sky_temperature?: number | null;
    /**
     * Seeing estimate — star FWHM in arcseconds; null if unsupported by the device
     */
    star_fwhm?: number | null;
    /**
     * Ambient temperature in degrees Celsius; null if unsupported by the device
     */
    temperature?: number | null;
    /**
     * Wind direction in degrees (0–360); null if unsupported by the device
     */
    wind_direction?: number | null;
    /**
     * Wind gust speed in m/s; null if unsupported by the device
     */
    wind_gust?: number | null;
    /**
     * Wind speed in m/s; null if unsupported by the device
     */
    wind_speed?: number | null;
    [property: string]: any;
}

/**
 * Safety monitor event data
 */
export interface SafetyDeviceInfoData {
    connected: boolean;
    is_safe:   boolean;
    [property: string]: any;
}

/**
 * Safety monitor is_safe changed
 */
export interface SafetyChangeData {
    is_safe: boolean;
    [property: string]: any;
}

/**
 * Flat Panel device state
 */
export interface FlatPanelData {
    brightness:           number;
    connected:            boolean;
    cover_state:          string;
    light_on:             boolean;
    max_brightness?:      number;
    min_brightness?:      number;
    supports_on_off?:     boolean;
    supports_open_close?: boolean;
    [property: string]: any;
}

/**
 * Filter Wheel device state
 */
export interface FilterWheelData {
    connected:        boolean;
    is_moving:        boolean;
    selected_filter?: FilterInfo;
    [property: string]: any;
}

/**
 * Info on a specific filter
 */
export interface FilterInfo {
    auto_focus_binning?: null | string;
    auto_focus_gain?:    number | null;
    auto_focus_offset?:  number | null;
    auto_focus_time?:    number | null;
    is_af_filter?:       boolean;
    name?:               null | string;
    offset?:             number | null;
    postion?:            number | null;
    [property: string]: any;
}

// Converts JSON strings to/from your types
// and asserts the results of JSON.parse at runtime
export class Convert {
    public static toDeviceConnectionData(json: string): DeviceConnectionData {
        return cast(JSON.parse(json), r("DeviceConnectionData"));
    }

    public static deviceConnectionDataToJson(value: DeviceConnectionData): string {
        return JSON.stringify(uncast(value, r("DeviceConnectionData")), null, 2);
    }

    public static toCameraData(json: string): CameraData {
        return cast(JSON.parse(json), r("CameraData"));
    }

    public static cameraDataToJson(value: CameraData): string {
        return JSON.stringify(uncast(value, r("CameraData")), null, 2);
    }

    public static toTelescopeData(json: string): TelescopeData {
        return cast(JSON.parse(json), r("TelescopeData"));
    }

    public static telescopeDataToJson(value: TelescopeData): string {
        return JSON.stringify(uncast(value, r("TelescopeData")), null, 2);
    }

    public static toImagingData(json: string): ImagingData {
        return cast(JSON.parse(json), r("ImagingData"));
    }

    public static imagingDataToJson(value: ImagingData): string {
        return JSON.stringify(uncast(value, r("ImagingData")), null, 2);
    }

    public static toWeatherDeviceInfoData(json: string): WeatherDeviceInfoData {
        return cast(JSON.parse(json), r("WeatherDeviceInfoData"));
    }

    public static weatherDeviceInfoDataToJson(value: WeatherDeviceInfoData): string {
        return JSON.stringify(uncast(value, r("WeatherDeviceInfoData")), null, 2);
    }

    public static toSafetyDeviceInfoData(json: string): SafetyDeviceInfoData {
        return cast(JSON.parse(json), r("SafetyDeviceInfoData"));
    }

    public static safetyDeviceInfoDataToJson(value: SafetyDeviceInfoData): string {
        return JSON.stringify(uncast(value, r("SafetyDeviceInfoData")), null, 2);
    }

    public static toSafetyChangeData(json: string): SafetyChangeData {
        return cast(JSON.parse(json), r("SafetyChangeData"));
    }

    public static safetyChangeDataToJson(value: SafetyChangeData): string {
        return JSON.stringify(uncast(value, r("SafetyChangeData")), null, 2);
    }

    public static toFlatPanelData(json: string): FlatPanelData {
        return cast(JSON.parse(json), r("FlatPanelData"));
    }

    public static flatPanelDataToJson(value: FlatPanelData): string {
        return JSON.stringify(uncast(value, r("FlatPanelData")), null, 2);
    }

    public static toFilterWheelData(json: string): FilterWheelData {
        return cast(JSON.parse(json), r("FilterWheelData"));
    }

    public static filterWheelDataToJson(value: FilterWheelData): string {
        return JSON.stringify(uncast(value, r("FilterWheelData")), null, 2);
    }
}

function invalidValue(typ: any, val: any, key: any, parent: any = ''): never {
    const prettyTyp = prettyTypeName(typ);
    const parentText = parent ? ` on ${parent}` : '';
    const keyText = key ? ` for key "${key}"` : '';
    throw Error(`Invalid value${keyText}${parentText}. Expected ${prettyTyp} but got ${JSON.stringify(val)}`);
}

function prettyTypeName(typ: any): string {
    if (Array.isArray(typ)) {
        if (typ.length === 2 && typ[0] === undefined) {
            return `an optional ${prettyTypeName(typ[1])}`;
        } else {
            return `one of [${typ.map(a => { return prettyTypeName(a); }).join(", ")}]`;
        }
    } else if (typeof typ === "object" && typ.literal !== undefined) {
        return typ.literal;
    } else {
        return typeof typ;
    }
}

function jsonToJSProps(typ: any): any {
    if (typ.jsonToJS === undefined) {
        const map: any = {};
        typ.props.forEach((p: any) => map[p.json] = { key: p.js, typ: p.typ });
        typ.jsonToJS = map;
    }
    return typ.jsonToJS;
}

function jsToJSONProps(typ: any): any {
    if (typ.jsToJSON === undefined) {
        const map: any = {};
        typ.props.forEach((p: any) => map[p.js] = { key: p.json, typ: p.typ });
        typ.jsToJSON = map;
    }
    return typ.jsToJSON;
}

function transform(val: any, typ: any, getProps: any, key: any = '', parent: any = ''): any {
    function transformPrimitive(typ: string, val: any): any {
        if (typeof typ === typeof val) return val;
        return invalidValue(typ, val, key, parent);
    }

    function transformUnion(typs: any[], val: any): any {
        // val must validate against one typ in typs
        const l = typs.length;
        for (let i = 0; i < l; i++) {
            const typ = typs[i];
            try {
                return transform(val, typ, getProps);
            } catch (_) {}
        }
        return invalidValue(typs, val, key, parent);
    }

    function transformEnum(cases: string[], val: any): any {
        if (cases.indexOf(val) !== -1) return val;
        return invalidValue(cases.map(a => { return l(a); }), val, key, parent);
    }

    function transformArray(typ: any, val: any): any {
        // val must be an array with no invalid elements
        if (!Array.isArray(val)) return invalidValue(l("array"), val, key, parent);
        return val.map(el => transform(el, typ, getProps));
    }

    function transformDate(val: any): any {
        if (val === null) {
            return null;
        }
        const d = new Date(val);
        if (isNaN(d.valueOf())) {
            return invalidValue(l("Date"), val, key, parent);
        }
        return d;
    }

    function transformObject(props: { [k: string]: any }, additional: any, val: any): any {
        if (val === null || typeof val !== "object" || Array.isArray(val)) {
            return invalidValue(l(ref || "object"), val, key, parent);
        }
        const result: any = {};
        Object.getOwnPropertyNames(props).forEach(key => {
            const prop = props[key];
            const v = Object.prototype.hasOwnProperty.call(val, key) ? val[key] : undefined;
            result[prop.key] = transform(v, prop.typ, getProps, key, ref);
        });
        Object.getOwnPropertyNames(val).forEach(key => {
            if (!Object.prototype.hasOwnProperty.call(props, key)) {
                result[key] = transform(val[key], additional, getProps, key, ref);
            }
        });
        return result;
    }

    if (typ === "any") return val;
    if (typ === null) {
        if (val === null) return val;
        return invalidValue(typ, val, key, parent);
    }
    if (typ === false) return invalidValue(typ, val, key, parent);
    let ref: any = undefined;
    while (typeof typ === "object" && typ.ref !== undefined) {
        ref = typ.ref;
        typ = typeMap[typ.ref];
    }
    if (Array.isArray(typ)) return transformEnum(typ, val);
    if (typeof typ === "object") {
        return typ.hasOwnProperty("unionMembers") ? transformUnion(typ.unionMembers, val)
            : typ.hasOwnProperty("arrayItems")    ? transformArray(typ.arrayItems, val)
            : typ.hasOwnProperty("props")         ? transformObject(getProps(typ), typ.additional, val)
            : invalidValue(typ, val, key, parent);
    }
    // Numbers can be parsed by Date but shouldn't be.
    if (typ === Date && typeof val !== "number") return transformDate(val);
    return transformPrimitive(typ, val);
}

function cast<T>(val: any, typ: any): T {
    return transform(val, typ, jsonToJSProps);
}

function uncast<T>(val: T, typ: any): any {
    return transform(val, typ, jsToJSONProps);
}

function l(typ: any) {
    return { literal: typ };
}

function a(typ: any) {
    return { arrayItems: typ };
}

function u(...typs: any[]) {
    return { unionMembers: typs };
}

function o(props: any[], additional: any) {
    return { props, additional };
}

function m(additional: any) {
    return { props: [], additional };
}

function r(name: string) {
    return { ref: name };
}

const typeMap: any = {
    "DeviceConnectionData": o([
        { json: "connected", js: "connected", typ: true },
        { json: "description", js: "description", typ: u(undefined, u(null, "")) },
        { json: "device_id", js: "device_id", typ: u(undefined, u(null, "")) },
        { json: "device_type", js: "device_type", typ: r("DeviceType") },
        { json: "display_name", js: "display_name", typ: u(undefined, u(null, "")) },
        { json: "driver_info", js: "driver_info", typ: u(undefined, u(null, "")) },
        { json: "driver_version", js: "driver_version", typ: u(undefined, u(null, "")) },
        { json: "name", js: "name", typ: u(undefined, u(null, "")) },
    ], "any"),
    "CameraData": o([
        { json: "connected", js: "connected", typ: true },
        { json: "cooler_on", js: "cooler_on", typ: true },
        { json: "cooler_power", js: "cooler_power", typ: u(undefined, u(3.14, null)) },
        { json: "exposure_end_time", js: "exposure_end_time", typ: u(undefined, u(Date, null)) },
        { json: "is_exposing", js: "is_exposing", typ: true },
        { json: "last_download_time", js: "last_download_time", typ: 3.14 },
        { json: "temperature", js: "temperature", typ: u(undefined, u(3.14, null)) },
        { json: "temperature_set_point", js: "temperature_set_point", typ: u(undefined, u(3.14, null)) },
    ], "any"),
    "TelescopeData": o([
        { json: "altitude", js: "altitude", typ: u(undefined, u(3.14, null)) },
        { json: "azimuth", js: "azimuth", typ: u(undefined, u(3.14, null)) },
        { json: "connected", js: "connected", typ: true },
        { json: "declination", js: "declination", typ: u(undefined, u(3.14, null)) },
        { json: "right_ascension", js: "right_ascension", typ: u(undefined, u(3.14, null)) },
        { json: "site_elevation", js: "site_elevation", typ: u(undefined, u(3.14, null)) },
        { json: "site_latitude", js: "site_latitude", typ: u(undefined, u(3.14, null)) },
        { json: "site_longitude", js: "site_longitude", typ: u(undefined, u(3.14, null)) },
        { json: "slewing", js: "slewing", typ: true },
        { json: "tracking_enabled", js: "tracking_enabled", typ: true },
    ], "any"),
    "ImagingData": o([
        { json: "exposure_number", js: "exposure_number", typ: 0 },
        { json: "exposure_time", js: "exposure_time", typ: 3.14 },
        { json: "filter", js: "filter", typ: u(undefined, u(null, "")) },
        { json: "gain", js: "gain", typ: 0 },
        { json: "image_type", js: "image_type", typ: u(undefined, u(null, "")) },
        { json: "offset", js: "offset", typ: 0 },
        { json: "target_name", js: "target_name", typ: u(undefined, u(null, "")) },
    ], "any"),
    "WeatherDeviceInfoData": o([
        { json: "cloud_cover", js: "cloud_cover", typ: u(undefined, u(3.14, null)) },
        { json: "connected", js: "connected", typ: true },
        { json: "dew_point", js: "dew_point", typ: u(undefined, u(3.14, null)) },
        { json: "humidity", js: "humidity", typ: u(undefined, u(3.14, null)) },
        { json: "pressure", js: "pressure", typ: u(undefined, u(3.14, null)) },
        { json: "rain_rate", js: "rain_rate", typ: u(undefined, u(3.14, null)) },
        { json: "sky_quality", js: "sky_quality", typ: u(undefined, u(3.14, null)) },
        { json: "sky_temperature", js: "sky_temperature", typ: u(undefined, u(3.14, null)) },
        { json: "star_fwhm", js: "star_fwhm", typ: u(undefined, u(3.14, null)) },
        { json: "temperature", js: "temperature", typ: u(undefined, u(3.14, null)) },
        { json: "wind_direction", js: "wind_direction", typ: u(undefined, u(3.14, null)) },
        { json: "wind_gust", js: "wind_gust", typ: u(undefined, u(3.14, null)) },
        { json: "wind_speed", js: "wind_speed", typ: u(undefined, u(3.14, null)) },
    ], "any"),
    "SafetyDeviceInfoData": o([
        { json: "connected", js: "connected", typ: true },
        { json: "is_safe", js: "is_safe", typ: true },
    ], "any"),
    "SafetyChangeData": o([
        { json: "is_safe", js: "is_safe", typ: true },
    ], "any"),
    "FlatPanelData": o([
        { json: "brightness", js: "brightness", typ: 0 },
        { json: "connected", js: "connected", typ: true },
        { json: "cover_state", js: "cover_state", typ: "" },
        { json: "light_on", js: "light_on", typ: true },
        { json: "max_brightness", js: "max_brightness", typ: u(undefined, 0) },
        { json: "min_brightness", js: "min_brightness", typ: u(undefined, 0) },
        { json: "supports_on_off", js: "supports_on_off", typ: u(undefined, true) },
        { json: "supports_open_close", js: "supports_open_close", typ: u(undefined, true) },
    ], "any"),
    "FilterWheelData": o([
        { json: "connected", js: "connected", typ: true },
        { json: "is_moving", js: "is_moving", typ: true },
        { json: "selected_filter", js: "selected_filter", typ: u(undefined, r("FilterInfo")) },
    ], "any"),
    "FilterInfo": o([
        { json: "auto_focus_binning", js: "auto_focus_binning", typ: u(undefined, u(null, "")) },
        { json: "auto_focus_gain", js: "auto_focus_gain", typ: u(undefined, u(3.14, null)) },
        { json: "auto_focus_offset", js: "auto_focus_offset", typ: u(undefined, u(3.14, null)) },
        { json: "auto_focus_time", js: "auto_focus_time", typ: u(undefined, u(3.14, null)) },
        { json: "is_af_filter", js: "is_af_filter", typ: u(undefined, true) },
        { json: "name", js: "name", typ: u(undefined, u(null, "")) },
        { json: "offset", js: "offset", typ: u(undefined, u(3.14, null)) },
        { json: "postion", js: "postion", typ: u(undefined, u(3.14, null)) },
    ], "any"),
    "DeviceType": [
        "Camera",
        "Dome",
        "FilterWheel",
        "FlatPanel",
        "Focuser",
        "Guider",
        "Mount",
        "Rotator",
        "SafetyMonitor",
        "Switch",
        "Weather",
    ],
};
