// To parse this data:
//
//   import { Convert, DeviceConnectionData, CameraDeviceInfoData, DomeDeviceInfoData, DomeShutterData, FilterWheelDeviceInfoData, FlatPanelDeviceInfoData, FocuserDeviceInfoData, GuiderDeviceInfoData, MountDeviceInfoData, MountFlipData, MountMovedData, RotatorDeviceInfoData, SafetyDeviceInfoData, SafetyChangeData, SwitchDeviceInfoData, WeatherDeviceInfoData } from "./file";
//
//   const deviceConnectionData = Convert.toDeviceConnectionData(json);
//   const cameraDeviceInfoData = Convert.toCameraDeviceInfoData(json);
//   const domeDeviceInfoData = Convert.toDomeDeviceInfoData(json);
//   const domeShutterData = Convert.toDomeShutterData(json);
//   const filterWheelDeviceInfoData = Convert.toFilterWheelDeviceInfoData(json);
//   const flatPanelDeviceInfoData = Convert.toFlatPanelDeviceInfoData(json);
//   const focuserDeviceInfoData = Convert.toFocuserDeviceInfoData(json);
//   const guiderDeviceInfoData = Convert.toGuiderDeviceInfoData(json);
//   const mountDeviceInfoData = Convert.toMountDeviceInfoData(json);
//   const mountFlipData = Convert.toMountFlipData(json);
//   const mountMovedData = Convert.toMountMovedData(json);
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
    battery?:               number;
    bayer_offset_x?:        number;
    bayer_offset_y?:        number;
    bin_x?:                 number;
    bin_y?:                 number;
    binning_modes?:         BinningMode[];
    bit_depth?:             number;
    camera_state?:          CameraState;
    can_get_gain?:          boolean;
    can_set_gain:           boolean;
    can_set_offset:         boolean;
    can_set_temperature:    boolean;
    can_set_usb_limit?:     boolean;
    can_show_live_view?:    boolean;
    can_sub_sample?:        boolean;
    connected:              boolean;
    cooler_on?:             boolean;
    cooler_power?:          number;
    default_gain?:          number;
    default_offset?:        number;
    dew_heater_on?:         boolean;
    electrons_per_adu?:     number;
    exposure_end_time?:     Date;
    exposure_max?:          number;
    exposure_min?:          number;
    gain?:                  number;
    gain_max?:              number;
    gain_min?:              number;
    gains?:                 number[];
    has_battery?:           boolean;
    has_dew_heater?:        boolean;
    has_shutter:            boolean;
    is_exposing?:           boolean;
    is_sub_sample_enabled?: boolean;
    last_download_time?:    number;
    live_view_enabled?:     boolean;
    normal_readout_mode?:   number;
    offset?:                number;
    offset_max?:            number;
    offset_min?:            number;
    pixel_size?:            number;
    readout_mode?:          number;
    readout_modes?:         string[];
    sensor_type?:           SensorType;
    snap_readout_mode?:     number;
    sub_sample_height?:     number;
    sub_sample_width?:      number;
    sub_sample_x?:          number;
    sub_sample_y?:          number;
    temerature_set_point?:  number;
    temperature?:           number;
    usb_limit?:             number;
    usb_limit_max?:         number;
    usb_limit_min?:         number;
    x_size?:                number;
    y_size?:                number;
    [property: string]: any;
}

export interface BinningMode {
    x: number;
    y: number;
    [property: string]: any;
}

export enum CameraState {
    Download = "Download",
    Error = "Error",
    Exposing = "Exposing",
    Idle = "Idle",
    LoadingFile = "LoadingFile",
    None = "None",
    Reading = "Reading",
    Waiting = "Waiting",
}

export enum SensorType {
    Bggr = "BGGR",
    Bgrg = "BGRG",
    Cmyg = "CMYG",
    Cmyg2 = "CMYG2",
    Color = "Color",
    Gbgr = "GBGR",
    Gbrg = "GBRG",
    Grbg = "GRBG",
    Grgb = "GRGB",
    Lrgb = "LRGB",
    Monochrome = "Monochrome",
    Rgbg = "RGBG",
    Rggb = "RGGB",
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
 * Event that fires before or after a meridian flip
 */
export interface DomeShutterData {
    position: Position;
    [property: string]: any;
}

export enum Position {
    Closed = "closed",
    Homed = "homed",
    Opened = "opened",
    Parked = "parked",
    Slewed = "slewed",
    Synced = "synced",
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
    can_clear_calibration: boolean;
    can_get_lock_postion:  boolean;
    can_set_shift_rate:    boolean;
    connected:             boolean;
    pixel_scale?:          number;
    rms_error?:            RMSError;
    [property: string]: any;
}

export interface RMSError {
    dec:      RMSUnit;
    peak_dec: RMSUnit;
    peak_ra:  RMSUnit;
    ra:       RMSUnit;
    total:    RMSUnit;
    [property: string]: any;
}

export interface RMSUnit {
    arc_seconds: number;
    pixel:       number;
    [property: string]: any;
}

/**
 * Periodic Mount event data
 */
export interface MountDeviceInfoData {
    alignment_mode?:                 AlignmentMode;
    altitude?:                       number;
    at_home?:                        boolean;
    at_park?:                        boolean;
    azimuth?:                        number;
    can_find_home?:                  boolean;
    can_move_primary_axis?:          boolean;
    can_move_secondary_axis?:        boolean;
    can_park?:                       boolean;
    can_pulse_guide?:                boolean;
    can_set_declination_rate?:       boolean;
    can_set_park?:                   boolean;
    can_set_pier_side?:              boolean;
    can_set_right_ascension_rate?:   boolean;
    can_set_tracking?:               boolean;
    can_slew?:                       boolean;
    can_slew_alt_az?:                boolean;
    connected:                       boolean;
    declination?:                    number;
    equatorial_system?:              Epoch;
    guide_rate_dec_arc_sec_per_sec?: number;
    guide_rate_ra_arc_sec_per_sec?:  number;
    has_unknown_epoch?:              boolean;
    is_pulse_guiding?:               boolean;
    primary_axis_rates?:             Array<number[]>;
    right_ascension?:                number;
    secondary_axis_rates?:           Array<number[]>;
    side_of_pier?:                   PierSide;
    sidereal_time?:                  number;
    site_elevation?:                 number;
    site_latitude?:                  number;
    site_longitude?:                 number;
    slewing?:                        boolean;
    target_coordinates?:             Coordinates;
    target_side_of_pier?:            PierSide;
    time_to_meridian_flip?:          number;
    tracking_enabled?:               boolean;
    tracking_modes?:                 TrackingMode[];
    tracking_rate?:                  TrackingRate;
    utc_date?:                       Date;
    [property: string]: any;
}

export enum AlignmentMode {
    AltAz = "AltAz",
    GermanPolar = "GermanPolar",
    Polar = "Polar",
}

export enum Epoch {
    B1950 = "B1950",
    J2000 = "J2000",
    J2050 = "J2050",
    Jnow = "JNOW",
}

export enum PierSide {
    East = "East",
    Unknown = "Unknown",
    West = "West",
}

export interface Coordinates {
    dec_degrees?: number;
    epoch?:       Epoch;
    ra_degrees?:  number;
    [property: string]: any;
}

export enum TrackingMode {
    Custom = "Custom",
    King = "King",
    Lunar = "Lunar",
    Sidereal = "Sidereal",
    Solar = "Solar",
    Stopped = "Stopped",
}

export interface TrackingRate {
    custom_dec_rate?: number;
    custom_ra_rate?:  number;
    tracking_mode?:   TrackingMode;
    [property: string]: any;
}

/**
 * Event that fires before or after a meridian flip
 */
export interface MountFlipData {
    flip_event:          FlipEvent;
    success?:            boolean | null;
    target_coordinates?: Coordinates;
    [property: string]: any;
}

export enum FlipEvent {
    After = "after",
    Before = "before",
}

/**
 * Event after the mount has moved
 */
export interface MountMovedData {
    from_coordinates?: Coordinates;
    move_type:         MoveType;
    to_coordinates?:   Coordinates;
    [property: string]: any;
}

export enum MoveType {
    Homed = "homed",
    Parked = "parked",
    Slewed = "slewed",
    Unparked = "unparked",
}

/**
 * Periodic Rotator event data
 */
export interface RotatorDeviceInfoData {
    can_reverse:          boolean;
    connected:            boolean;
    is_moving:            boolean;
    mechanical_position?: number;
    position?:            number;
    reverse:              boolean;
    step_size?:           number;
    synced:               boolean;
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
    connected:           boolean;
    readable_switches?:  ReadableSwitch[];
    writeable_switches?: WriteableSwitch[];
    [property: string]: any;
}

export interface ReadableSwitch {
    description?: string;
    id:           number;
    name?:        string;
    value?:       number;
    [property: string]: any;
}

export interface WriteableSwitch {
    description?: string;
    id:           number;
    maximum:      number;
    minimum:      number;
    name?:        string;
    step_size:    number;
    target_value: number;
    value?:       number;
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

    public static toDomeShutterData(json: string): DomeShutterData {
        return cast(JSON.parse(json), r("DomeShutterData"));
    }

    public static domeShutterDataToJson(value: DomeShutterData): string {
        return JSON.stringify(uncast(value, r("DomeShutterData")), null, 2);
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

    public static toMountFlipData(json: string): MountFlipData {
        return cast(JSON.parse(json), r("MountFlipData"));
    }

    public static mountFlipDataToJson(value: MountFlipData): string {
        return JSON.stringify(uncast(value, r("MountFlipData")), null, 2);
    }

    public static toMountMovedData(json: string): MountMovedData {
        return cast(JSON.parse(json), r("MountMovedData"));
    }

    public static mountMovedDataToJson(value: MountMovedData): string {
        return JSON.stringify(uncast(value, r("MountMovedData")), null, 2);
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
        { json: "battery", js: "battery", typ: u(undefined, 0) },
        { json: "bayer_offset_x", js: "bayer_offset_x", typ: u(undefined, 0) },
        { json: "bayer_offset_y", js: "bayer_offset_y", typ: u(undefined, 0) },
        { json: "bin_x", js: "bin_x", typ: u(undefined, 0) },
        { json: "bin_y", js: "bin_y", typ: u(undefined, 0) },
        { json: "binning_modes", js: "binning_modes", typ: u(undefined, a(r("BinningMode"))) },
        { json: "bit_depth", js: "bit_depth", typ: u(undefined, 0) },
        { json: "camera_state", js: "camera_state", typ: u(undefined, r("CameraState")) },
        { json: "can_get_gain", js: "can_get_gain", typ: u(undefined, true) },
        { json: "can_set_gain", js: "can_set_gain", typ: true },
        { json: "can_set_offset", js: "can_set_offset", typ: true },
        { json: "can_set_temperature", js: "can_set_temperature", typ: true },
        { json: "can_set_usb_limit", js: "can_set_usb_limit", typ: u(undefined, true) },
        { json: "can_show_live_view", js: "can_show_live_view", typ: u(undefined, true) },
        { json: "can_sub_sample", js: "can_sub_sample", typ: u(undefined, true) },
        { json: "connected", js: "connected", typ: true },
        { json: "cooler_on", js: "cooler_on", typ: u(undefined, true) },
        { json: "cooler_power", js: "cooler_power", typ: u(undefined, 3.14) },
        { json: "default_gain", js: "default_gain", typ: u(undefined, 0) },
        { json: "default_offset", js: "default_offset", typ: u(undefined, 0) },
        { json: "dew_heater_on", js: "dew_heater_on", typ: u(undefined, true) },
        { json: "electrons_per_adu", js: "electrons_per_adu", typ: u(undefined, 3.14) },
        { json: "exposure_end_time", js: "exposure_end_time", typ: u(undefined, Date) },
        { json: "exposure_max", js: "exposure_max", typ: u(undefined, 3.14) },
        { json: "exposure_min", js: "exposure_min", typ: u(undefined, 3.14) },
        { json: "gain", js: "gain", typ: u(undefined, 0) },
        { json: "gain_max", js: "gain_max", typ: u(undefined, 0) },
        { json: "gain_min", js: "gain_min", typ: u(undefined, 0) },
        { json: "gains", js: "gains", typ: u(undefined, a(0)) },
        { json: "has_battery", js: "has_battery", typ: u(undefined, true) },
        { json: "has_dew_heater", js: "has_dew_heater", typ: u(undefined, true) },
        { json: "has_shutter", js: "has_shutter", typ: true },
        { json: "is_exposing", js: "is_exposing", typ: u(undefined, true) },
        { json: "is_sub_sample_enabled", js: "is_sub_sample_enabled", typ: u(undefined, true) },
        { json: "last_download_time", js: "last_download_time", typ: u(undefined, 3.14) },
        { json: "live_view_enabled", js: "live_view_enabled", typ: u(undefined, true) },
        { json: "normal_readout_mode", js: "normal_readout_mode", typ: u(undefined, 0) },
        { json: "offset", js: "offset", typ: u(undefined, 0) },
        { json: "offset_max", js: "offset_max", typ: u(undefined, 0) },
        { json: "offset_min", js: "offset_min", typ: u(undefined, 0) },
        { json: "pixel_size", js: "pixel_size", typ: u(undefined, 3.14) },
        { json: "readout_mode", js: "readout_mode", typ: u(undefined, 0) },
        { json: "readout_modes", js: "readout_modes", typ: u(undefined, a("")) },
        { json: "sensor_type", js: "sensor_type", typ: u(undefined, r("SensorType")) },
        { json: "snap_readout_mode", js: "snap_readout_mode", typ: u(undefined, 0) },
        { json: "sub_sample_height", js: "sub_sample_height", typ: u(undefined, 0) },
        { json: "sub_sample_width", js: "sub_sample_width", typ: u(undefined, 0) },
        { json: "sub_sample_x", js: "sub_sample_x", typ: u(undefined, 0) },
        { json: "sub_sample_y", js: "sub_sample_y", typ: u(undefined, 0) },
        { json: "temerature_set_point", js: "temerature_set_point", typ: u(undefined, 3.14) },
        { json: "temperature", js: "temperature", typ: u(undefined, 3.14) },
        { json: "usb_limit", js: "usb_limit", typ: u(undefined, 0) },
        { json: "usb_limit_max", js: "usb_limit_max", typ: u(undefined, 0) },
        { json: "usb_limit_min", js: "usb_limit_min", typ: u(undefined, 0) },
        { json: "x_size", js: "x_size", typ: u(undefined, 0) },
        { json: "y_size", js: "y_size", typ: u(undefined, 0) },
    ], "any"),
    "BinningMode": o([
        { json: "x", js: "x", typ: 0 },
        { json: "y", js: "y", typ: 0 },
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
    "DomeShutterData": o([
        { json: "position", js: "position", typ: r("Position") },
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
        { json: "can_clear_calibration", js: "can_clear_calibration", typ: true },
        { json: "can_get_lock_postion", js: "can_get_lock_postion", typ: true },
        { json: "can_set_shift_rate", js: "can_set_shift_rate", typ: true },
        { json: "connected", js: "connected", typ: true },
        { json: "pixel_scale", js: "pixel_scale", typ: u(undefined, 3.14) },
        { json: "rms_error", js: "rms_error", typ: u(undefined, r("RMSError")) },
    ], "any"),
    "RMSError": o([
        { json: "dec", js: "dec", typ: r("RMSUnit") },
        { json: "peak_dec", js: "peak_dec", typ: r("RMSUnit") },
        { json: "peak_ra", js: "peak_ra", typ: r("RMSUnit") },
        { json: "ra", js: "ra", typ: r("RMSUnit") },
        { json: "total", js: "total", typ: r("RMSUnit") },
    ], "any"),
    "RMSUnit": o([
        { json: "arc_seconds", js: "arc_seconds", typ: 3.14 },
        { json: "pixel", js: "pixel", typ: 3.14 },
    ], "any"),
    "MountDeviceInfoData": o([
        { json: "alignment_mode", js: "alignment_mode", typ: u(undefined, r("AlignmentMode")) },
        { json: "altitude", js: "altitude", typ: u(undefined, 3.14) },
        { json: "at_home", js: "at_home", typ: u(undefined, true) },
        { json: "at_park", js: "at_park", typ: u(undefined, true) },
        { json: "azimuth", js: "azimuth", typ: u(undefined, 3.14) },
        { json: "can_find_home", js: "can_find_home", typ: u(undefined, true) },
        { json: "can_move_primary_axis", js: "can_move_primary_axis", typ: u(undefined, true) },
        { json: "can_move_secondary_axis", js: "can_move_secondary_axis", typ: u(undefined, true) },
        { json: "can_park", js: "can_park", typ: u(undefined, true) },
        { json: "can_pulse_guide", js: "can_pulse_guide", typ: u(undefined, true) },
        { json: "can_set_declination_rate", js: "can_set_declination_rate", typ: u(undefined, true) },
        { json: "can_set_park", js: "can_set_park", typ: u(undefined, true) },
        { json: "can_set_pier_side", js: "can_set_pier_side", typ: u(undefined, true) },
        { json: "can_set_right_ascension_rate", js: "can_set_right_ascension_rate", typ: u(undefined, true) },
        { json: "can_set_tracking", js: "can_set_tracking", typ: u(undefined, true) },
        { json: "can_slew", js: "can_slew", typ: u(undefined, true) },
        { json: "can_slew_alt_az", js: "can_slew_alt_az", typ: u(undefined, true) },
        { json: "connected", js: "connected", typ: true },
        { json: "declination", js: "declination", typ: u(undefined, 3.14) },
        { json: "equatorial_system", js: "equatorial_system", typ: u(undefined, r("Epoch")) },
        { json: "guide_rate_dec_arc_sec_per_sec", js: "guide_rate_dec_arc_sec_per_sec", typ: u(undefined, 3.14) },
        { json: "guide_rate_ra_arc_sec_per_sec", js: "guide_rate_ra_arc_sec_per_sec", typ: u(undefined, 3.14) },
        { json: "has_unknown_epoch", js: "has_unknown_epoch", typ: u(undefined, true) },
        { json: "is_pulse_guiding", js: "is_pulse_guiding", typ: u(undefined, true) },
        { json: "primary_axis_rates", js: "primary_axis_rates", typ: u(undefined, a(a(3.14))) },
        { json: "right_ascension", js: "right_ascension", typ: u(undefined, 3.14) },
        { json: "secondary_axis_rates", js: "secondary_axis_rates", typ: u(undefined, a(a(3.14))) },
        { json: "side_of_pier", js: "side_of_pier", typ: u(undefined, r("PierSide")) },
        { json: "sidereal_time", js: "sidereal_time", typ: u(undefined, 3.14) },
        { json: "site_elevation", js: "site_elevation", typ: u(undefined, 3.14) },
        { json: "site_latitude", js: "site_latitude", typ: u(undefined, 3.14) },
        { json: "site_longitude", js: "site_longitude", typ: u(undefined, 3.14) },
        { json: "slewing", js: "slewing", typ: u(undefined, true) },
        { json: "target_coordinates", js: "target_coordinates", typ: u(undefined, r("Coordinates")) },
        { json: "target_side_of_pier", js: "target_side_of_pier", typ: u(undefined, r("PierSide")) },
        { json: "time_to_meridian_flip", js: "time_to_meridian_flip", typ: u(undefined, 3.14) },
        { json: "tracking_enabled", js: "tracking_enabled", typ: u(undefined, true) },
        { json: "tracking_modes", js: "tracking_modes", typ: u(undefined, a(r("TrackingMode"))) },
        { json: "tracking_rate", js: "tracking_rate", typ: u(undefined, r("TrackingRate")) },
        { json: "utc_date", js: "utc_date", typ: u(undefined, Date) },
    ], "any"),
    "Coordinates": o([
        { json: "dec_degrees", js: "dec_degrees", typ: u(undefined, 3.14) },
        { json: "epoch", js: "epoch", typ: u(undefined, r("Epoch")) },
        { json: "ra_degrees", js: "ra_degrees", typ: u(undefined, 3.14) },
    ], "any"),
    "TrackingRate": o([
        { json: "custom_dec_rate", js: "custom_dec_rate", typ: u(undefined, 3.14) },
        { json: "custom_ra_rate", js: "custom_ra_rate", typ: u(undefined, 3.14) },
        { json: "tracking_mode", js: "tracking_mode", typ: u(undefined, r("TrackingMode")) },
    ], "any"),
    "MountFlipData": o([
        { json: "flip_event", js: "flip_event", typ: r("FlipEvent") },
        { json: "success", js: "success", typ: u(undefined, u(true, null)) },
        { json: "target_coordinates", js: "target_coordinates", typ: u(undefined, r("Coordinates")) },
    ], "any"),
    "MountMovedData": o([
        { json: "from_coordinates", js: "from_coordinates", typ: u(undefined, r("Coordinates")) },
        { json: "move_type", js: "move_type", typ: r("MoveType") },
        { json: "to_coordinates", js: "to_coordinates", typ: u(undefined, r("Coordinates")) },
    ], "any"),
    "RotatorDeviceInfoData": o([
        { json: "can_reverse", js: "can_reverse", typ: true },
        { json: "connected", js: "connected", typ: true },
        { json: "is_moving", js: "is_moving", typ: true },
        { json: "mechanical_position", js: "mechanical_position", typ: u(undefined, 3.14) },
        { json: "position", js: "position", typ: u(undefined, 3.14) },
        { json: "reverse", js: "reverse", typ: true },
        { json: "step_size", js: "step_size", typ: u(undefined, 3.14) },
        { json: "synced", js: "synced", typ: true },
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
        { json: "readable_switches", js: "readable_switches", typ: u(undefined, a(r("ReadableSwitch"))) },
        { json: "writeable_switches", js: "writeable_switches", typ: u(undefined, a(r("WriteableSwitch"))) },
    ], "any"),
    "ReadableSwitch": o([
        { json: "description", js: "description", typ: u(undefined, "") },
        { json: "id", js: "id", typ: 0 },
        { json: "name", js: "name", typ: u(undefined, "") },
        { json: "value", js: "value", typ: u(undefined, 3.14) },
    ], "any"),
    "WriteableSwitch": o([
        { json: "description", js: "description", typ: u(undefined, "") },
        { json: "id", js: "id", typ: 0 },
        { json: "maximum", js: "maximum", typ: 3.14 },
        { json: "minimum", js: "minimum", typ: 3.14 },
        { json: "name", js: "name", typ: u(undefined, "") },
        { json: "step_size", js: "step_size", typ: 3.14 },
        { json: "target_value", js: "target_value", typ: 3.14 },
        { json: "value", js: "value", typ: u(undefined, 3.14) },
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
    "CameraState": [
        "Download",
        "Error",
        "Exposing",
        "Idle",
        "LoadingFile",
        "None",
        "Reading",
        "Waiting",
    ],
    "SensorType": [
        "BGGR",
        "BGRG",
        "CMYG",
        "CMYG2",
        "Color",
        "GBGR",
        "GBRG",
        "GRBG",
        "GRGB",
        "LRGB",
        "Monochrome",
        "RGBG",
        "RGGB",
    ],
    "ShutterState": [
        "Closed",
        "Closing",
        "Error",
        "None",
        "Open",
        "Opening",
    ],
    "Position": [
        "closed",
        "homed",
        "opened",
        "parked",
        "slewed",
        "synced",
    ],
    "AlignmentMode": [
        "AltAz",
        "GermanPolar",
        "Polar",
    ],
    "Epoch": [
        "B1950",
        "J2000",
        "J2050",
        "JNOW",
    ],
    "PierSide": [
        "East",
        "Unknown",
        "West",
    ],
    "TrackingMode": [
        "Custom",
        "King",
        "Lunar",
        "Sidereal",
        "Solar",
        "Stopped",
    ],
    "FlipEvent": [
        "after",
        "before",
    ],
    "MoveType": [
        "homed",
        "parked",
        "slewed",
        "unparked",
    ],
};
