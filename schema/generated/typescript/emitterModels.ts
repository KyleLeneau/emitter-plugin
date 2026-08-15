// To parse this data:
//
//   import { Convert, DeviceConnectionData, CameraDeviceInfoData, DomeDeviceInfoData, FilterWheelDeviceInfoData, FlatPanelDeviceInfoData, FocuserDeviceInfoData, GuiderDeviceInfoData, MountDeviceInfoData, RotatorDeviceInfoData, SafetyDeviceInfoData, SafetyChangeData, SwitchDeviceInfoData, WeatherDeviceInfoData } from "./file";
//
//   const deviceConnectionData = Convert.toDeviceConnectionData(json);
//   const cameraDeviceInfoData = Convert.toCameraDeviceInfoData(json);
//   const domeDeviceInfoData = Convert.toDomeDeviceInfoData(json);
//   const filterWheelDeviceInfoData = Convert.toFilterWheelDeviceInfoData(json);
//   const flatPanelDeviceInfoData = Convert.toFlatPanelDeviceInfoData(json);
//   const focuserDeviceInfoData = Convert.toFocuserDeviceInfoData(json);
//   const guiderDeviceInfoData = Convert.toGuiderDeviceInfoData(json);
//   const mountDeviceInfoData = Convert.toMountDeviceInfoData(json);
//   const rotatorDeviceInfoData = Convert.toRotatorDeviceInfoData(json);
//   const safetyDeviceInfoData = Convert.toSafetyDeviceInfoData(json);
//   const safetyChangeData = Convert.toSafetyChangeData(json);
//   const switchDeviceInfoData = Convert.toSwitchDeviceInfoData(json);
//   const weatherDeviceInfoData = Convert.toWeatherDeviceInfoData(json);
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
 * Periodic Camera event data
 */
export interface CameraDeviceInfoData {
    connected: boolean;
    [property: string]: any;
}

/**
 * Dome device state
 */
export interface DomeDeviceInfoData {
    altitude_degrees?:     number;
    application_following: boolean;
    at_home:               boolean;
    at_park:               boolean;
    azimuth_degrees?:      number;
    can_find_home:         boolean;
    can_park:              boolean;
    can_set_azimuth:       boolean;
    can_set_park:          boolean;
    can_set_shutter:       boolean;
    can_sync_azimuth:      boolean;
    connected:             boolean;
    driver_can_follow:     boolean;
    driver_following:      boolean;
    following_type?:       string;
    shutter_state:         ShutterState;
    slewing:               boolean;
    [property: string]: any;
}

export enum ShutterState {
    Closed = "Closed",
    Closing = "Closing",
    Error = "Error",
    None = "None",
    Open = "Open",
    Opening = "Opening",
}

/**
 * Filter Wheel device state
 */
export interface FilterWheelDeviceInfoData {
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

/**
 * Flat Panel device state
 */
export interface FlatPanelDeviceInfoData {
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
 * Focuser device state
 */
export interface FocuserDeviceInfoData {
    connected:            boolean;
    is_moving:            boolean;
    is_settling:          boolean;
    position?:            number;
    step_size?:           number;
    temp_comp?:           boolean;
    temp_comp_available?: boolean;
    temperature?:         number;
    [property: string]: any;
}

/**
 * Periodic Guider event data
 */
export interface GuiderDeviceInfoData {
    connected: boolean;
    [property: string]: any;
}

/**
 * Periodic Mount event data
 */
export interface MountDeviceInfoData {
    connected: boolean;
    [property: string]: any;
}

/**
 * Periodic Rotator event data
 */
export interface RotatorDeviceInfoData {
    connected: boolean;
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
 * Periodic Switch event data
 */
export interface SwitchDeviceInfoData {
    connected: boolean;
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

// Converts JSON strings to/from your types
// and asserts the results of JSON.parse at runtime
export class Convert {
    public static toDeviceConnectionData(json: string): DeviceConnectionData {
        return cast(JSON.parse(json), r("DeviceConnectionData"));
    }

    public static deviceConnectionDataToJson(value: DeviceConnectionData): string {
        return JSON.stringify(uncast(value, r("DeviceConnectionData")), null, 2);
    }

    public static toCameraDeviceInfoData(json: string): CameraDeviceInfoData {
        return cast(JSON.parse(json), r("CameraDeviceInfoData"));
    }

    public static cameraDeviceInfoDataToJson(value: CameraDeviceInfoData): string {
        return JSON.stringify(uncast(value, r("CameraDeviceInfoData")), null, 2);
    }

    public static toDomeDeviceInfoData(json: string): DomeDeviceInfoData {
        return cast(JSON.parse(json), r("DomeDeviceInfoData"));
    }

    public static domeDeviceInfoDataToJson(value: DomeDeviceInfoData): string {
        return JSON.stringify(uncast(value, r("DomeDeviceInfoData")), null, 2);
    }

    public static toFilterWheelDeviceInfoData(json: string): FilterWheelDeviceInfoData {
        return cast(JSON.parse(json), r("FilterWheelDeviceInfoData"));
    }

    public static filterWheelDeviceInfoDataToJson(value: FilterWheelDeviceInfoData): string {
        return JSON.stringify(uncast(value, r("FilterWheelDeviceInfoData")), null, 2);
    }

    public static toFlatPanelDeviceInfoData(json: string): FlatPanelDeviceInfoData {
        return cast(JSON.parse(json), r("FlatPanelDeviceInfoData"));
    }

    public static flatPanelDeviceInfoDataToJson(value: FlatPanelDeviceInfoData): string {
        return JSON.stringify(uncast(value, r("FlatPanelDeviceInfoData")), null, 2);
    }

    public static toFocuserDeviceInfoData(json: string): FocuserDeviceInfoData {
        return cast(JSON.parse(json), r("FocuserDeviceInfoData"));
    }

    public static focuserDeviceInfoDataToJson(value: FocuserDeviceInfoData): string {
        return JSON.stringify(uncast(value, r("FocuserDeviceInfoData")), null, 2);
    }

    public static toGuiderDeviceInfoData(json: string): GuiderDeviceInfoData {
        return cast(JSON.parse(json), r("GuiderDeviceInfoData"));
    }

    public static guiderDeviceInfoDataToJson(value: GuiderDeviceInfoData): string {
        return JSON.stringify(uncast(value, r("GuiderDeviceInfoData")), null, 2);
    }

    public static toMountDeviceInfoData(json: string): MountDeviceInfoData {
        return cast(JSON.parse(json), r("MountDeviceInfoData"));
    }

    public static mountDeviceInfoDataToJson(value: MountDeviceInfoData): string {
        return JSON.stringify(uncast(value, r("MountDeviceInfoData")), null, 2);
    }

    public static toRotatorDeviceInfoData(json: string): RotatorDeviceInfoData {
        return cast(JSON.parse(json), r("RotatorDeviceInfoData"));
    }

    public static rotatorDeviceInfoDataToJson(value: RotatorDeviceInfoData): string {
        return JSON.stringify(uncast(value, r("RotatorDeviceInfoData")), null, 2);
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

    public static toSwitchDeviceInfoData(json: string): SwitchDeviceInfoData {
        return cast(JSON.parse(json), r("SwitchDeviceInfoData"));
    }

    public static switchDeviceInfoDataToJson(value: SwitchDeviceInfoData): string {
        return JSON.stringify(uncast(value, r("SwitchDeviceInfoData")), null, 2);
    }

    public static toWeatherDeviceInfoData(json: string): WeatherDeviceInfoData {
        return cast(JSON.parse(json), r("WeatherDeviceInfoData"));
    }

    public static weatherDeviceInfoDataToJson(value: WeatherDeviceInfoData): string {
        return JSON.stringify(uncast(value, r("WeatherDeviceInfoData")), null, 2);
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
    "CameraDeviceInfoData": o([
        { json: "connected", js: "connected", typ: true },
    ], "any"),
    "DomeDeviceInfoData": o([
        { json: "altitude_degrees", js: "altitude_degrees", typ: u(undefined, 3.14) },
        { json: "application_following", js: "application_following", typ: true },
        { json: "at_home", js: "at_home", typ: true },
        { json: "at_park", js: "at_park", typ: true },
        { json: "azimuth_degrees", js: "azimuth_degrees", typ: u(undefined, 3.14) },
        { json: "can_find_home", js: "can_find_home", typ: true },
        { json: "can_park", js: "can_park", typ: true },
        { json: "can_set_azimuth", js: "can_set_azimuth", typ: true },
        { json: "can_set_park", js: "can_set_park", typ: true },
        { json: "can_set_shutter", js: "can_set_shutter", typ: true },
        { json: "can_sync_azimuth", js: "can_sync_azimuth", typ: true },
        { json: "connected", js: "connected", typ: true },
        { json: "driver_can_follow", js: "driver_can_follow", typ: true },
        { json: "driver_following", js: "driver_following", typ: true },
        { json: "following_type", js: "following_type", typ: u(undefined, "") },
        { json: "shutter_state", js: "shutter_state", typ: r("ShutterState") },
        { json: "slewing", js: "slewing", typ: true },
    ], "any"),
    "FilterWheelDeviceInfoData": o([
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
    "FlatPanelDeviceInfoData": o([
        { json: "brightness", js: "brightness", typ: 0 },
        { json: "connected", js: "connected", typ: true },
        { json: "cover_state", js: "cover_state", typ: "" },
        { json: "light_on", js: "light_on", typ: true },
        { json: "max_brightness", js: "max_brightness", typ: u(undefined, 0) },
        { json: "min_brightness", js: "min_brightness", typ: u(undefined, 0) },
        { json: "supports_on_off", js: "supports_on_off", typ: u(undefined, true) },
        { json: "supports_open_close", js: "supports_open_close", typ: u(undefined, true) },
    ], "any"),
    "FocuserDeviceInfoData": o([
        { json: "connected", js: "connected", typ: true },
        { json: "is_moving", js: "is_moving", typ: true },
        { json: "is_settling", js: "is_settling", typ: true },
        { json: "position", js: "position", typ: u(undefined, 0) },
        { json: "step_size", js: "step_size", typ: u(undefined, 3.14) },
        { json: "temp_comp", js: "temp_comp", typ: u(undefined, true) },
        { json: "temp_comp_available", js: "temp_comp_available", typ: u(undefined, true) },
        { json: "temperature", js: "temperature", typ: u(undefined, 3.14) },
    ], "any"),
    "GuiderDeviceInfoData": o([
        { json: "connected", js: "connected", typ: true },
    ], "any"),
    "MountDeviceInfoData": o([
        { json: "connected", js: "connected", typ: true },
    ], "any"),
    "RotatorDeviceInfoData": o([
        { json: "connected", js: "connected", typ: true },
    ], "any"),
    "SafetyDeviceInfoData": o([
        { json: "connected", js: "connected", typ: true },
        { json: "is_safe", js: "is_safe", typ: true },
    ], "any"),
    "SafetyChangeData": o([
        { json: "is_safe", js: "is_safe", typ: true },
    ], "any"),
    "SwitchDeviceInfoData": o([
        { json: "connected", js: "connected", typ: true },
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
    "ShutterState": [
        "Closed",
        "Closing",
        "Error",
        "None",
        "Open",
        "Opening",
    ],
};
