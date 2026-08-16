// Example code that deserializes and serializes the model.
// extern crate serde;
// #[macro_use]
// extern crate serde_derive;
// extern crate serde_json;
//
// use generated_module::DeviceConnectionData;
//
// fn main() {
//     let json = r#"{"answer": 42}"#;
//     let model: DeviceConnectionData = serde_json::from_str(&json).unwrap();
// }

use serde::{Serialize, Deserialize};

/// General device connection data
#[derive(Serialize, Deserialize)]
pub struct DeviceConnectionData {
    pub connected: bool,

    /// Device description
    pub description: Option<String>,

    /// Driver ID
    pub device_id: Option<String>,

    pub device_type: DeviceType,

    /// A friendly name of the device for dipslay
    pub display_name: Option<String>,

    /// Driver name and info
    pub driver_info: Option<String>,

    /// Driver version information
    pub driver_version: Option<String>,

    /// The name of the device
    pub name: Option<String>,
}

#[derive(Serialize, Deserialize)]
pub enum DeviceType {
    Camera,

    Dome,

    #[serde(rename = "FilterWheel")]
    FilterWheel,

    #[serde(rename = "FlatPanel")]
    FlatPanel,

    Focuser,

    Guider,

    Mount,

    Rotator,

    #[serde(rename = "SafetyMonitor")]
    SafetyMonitor,

    Switch,

    Weather,
}

/// Periodic Camera event data
#[derive(Serialize, Deserialize)]
pub struct CameraDeviceInfoData {
    pub battery: Option<i64>,

    pub bayer_offset_x: Option<i64>,

    pub bayer_offset_y: Option<i64>,

    pub bin_x: Option<i64>,

    pub bin_y: Option<i64>,

    pub binning_modes: Option<Vec<BinningMode>>,

    pub bit_depth: Option<i64>,

    pub camera_state: Option<CameraState>,

    pub can_get_gain: Option<bool>,

    pub can_set_gain: bool,

    pub can_set_offset: bool,

    pub can_set_temperature: bool,

    pub can_set_usb_limit: Option<bool>,

    pub can_show_live_view: Option<bool>,

    pub can_sub_sample: Option<bool>,

    pub connected: bool,

    pub cooler_on: Option<bool>,

    pub cooler_power: Option<f64>,

    pub default_gain: Option<i64>,

    pub default_offset: Option<i64>,

    pub dew_heater_on: Option<bool>,

    pub electrons_per_adu: Option<f64>,

    pub exposure_end_time: Option<String>,

    pub exposure_max: Option<f64>,

    pub exposure_min: Option<f64>,

    pub gain: Option<i64>,

    pub gain_max: Option<i64>,

    pub gain_min: Option<i64>,

    pub gains: Option<Vec<i64>>,

    pub has_battery: Option<bool>,

    pub has_dew_heater: Option<bool>,

    pub has_shutter: bool,

    pub is_exposing: Option<bool>,

    pub is_sub_sample_enabled: Option<bool>,

    pub last_download_time: Option<f64>,

    pub live_view_enabled: Option<bool>,

    pub normal_readout_mode: Option<i64>,

    pub offset: Option<i64>,

    pub offset_max: Option<i64>,

    pub offset_min: Option<i64>,

    pub pixel_size: Option<f64>,

    pub readout_mode: Option<i64>,

    pub readout_modes: Option<Vec<String>>,

    pub sensor_type: Option<SensorType>,

    pub snap_readout_mode: Option<i64>,

    pub sub_sample_height: Option<i64>,

    pub sub_sample_width: Option<i64>,

    pub sub_sample_x: Option<i64>,

    pub sub_sample_y: Option<i64>,

    pub temerature_set_point: Option<f64>,

    pub temperature: Option<f64>,

    pub usb_limit: Option<i64>,

    pub usb_limit_max: Option<i64>,

    pub usb_limit_min: Option<i64>,

    pub x_size: Option<i64>,

    pub y_size: Option<i64>,
}

#[derive(Serialize, Deserialize)]
pub struct BinningMode {
    pub x: i64,

    pub y: i64,
}

#[derive(Serialize, Deserialize)]
pub enum CameraState {
    Download,

    Error,

    Exposing,

    Idle,

    #[serde(rename = "LoadingFile")]
    LoadingFile,

    None,

    Reading,

    Waiting,
}

#[derive(Serialize, Deserialize)]
pub enum SensorType {
    #[serde(rename = "BGGR")]
    Bggr,

    #[serde(rename = "BGRG")]
    Bgrg,

    #[serde(rename = "CMYG")]
    Cmyg,

    #[serde(rename = "CMYG2")]
    Cmyg2,

    Color,

    #[serde(rename = "GBGR")]
    Gbgr,

    #[serde(rename = "GBRG")]
    Gbrg,

    #[serde(rename = "GRBG")]
    Grbg,

    #[serde(rename = "GRGB")]
    Grgb,

    #[serde(rename = "LRGB")]
    Lrgb,

    Monochrome,

    #[serde(rename = "RGBG")]
    Rgbg,

    #[serde(rename = "RGGB")]
    Rggb,
}

/// Dome device state
#[derive(Serialize, Deserialize)]
pub struct DomeDeviceInfoData {
    pub altitude_degrees: Option<f64>,

    pub application_following: bool,

    pub at_home: bool,

    pub at_park: bool,

    pub azimuth_degrees: Option<f64>,

    pub can_find_home: bool,

    pub can_park: bool,

    pub can_set_azimuth: bool,

    pub can_set_park: bool,

    pub can_set_shutter: bool,

    pub can_sync_azimuth: bool,

    pub connected: bool,

    pub driver_can_follow: bool,

    pub driver_following: bool,

    pub following_type: Option<String>,

    pub shutter_state: ShutterState,

    pub slewing: bool,
}

#[derive(Serialize, Deserialize)]
pub enum ShutterState {
    Closed,

    Closing,

    Error,

    None,

    Open,

    Opening,
}

/// Filter Wheel device state
#[derive(Serialize, Deserialize)]
pub struct FilterWheelDeviceInfoData {
    pub connected: bool,

    pub is_moving: bool,

    pub selected_filter: Option<FilterInfo>,
}

/// Info on a specific filter
#[derive(Serialize, Deserialize)]
pub struct FilterInfo {
    pub auto_focus_binning: Option<String>,

    pub auto_focus_gain: Option<f64>,

    pub auto_focus_offset: Option<f64>,

    pub auto_focus_time: Option<f64>,

    pub is_af_filter: Option<bool>,

    pub name: Option<String>,

    pub offset: Option<f64>,

    pub postion: Option<f64>,
}

/// Flat Panel device state
#[derive(Serialize, Deserialize)]
pub struct FlatPanelDeviceInfoData {
    pub brightness: i64,

    pub connected: bool,

    pub cover_state: String,

    pub light_on: bool,

    pub max_brightness: Option<i64>,

    pub min_brightness: Option<i64>,

    pub supports_on_off: Option<bool>,

    pub supports_open_close: Option<bool>,
}

/// Focuser device state
#[derive(Serialize, Deserialize)]
pub struct FocuserDeviceInfoData {
    pub connected: bool,

    pub is_moving: bool,

    pub is_settling: bool,

    pub position: Option<i64>,

    pub step_size: Option<f64>,

    pub temp_comp: Option<bool>,

    pub temp_comp_available: Option<bool>,

    pub temperature: Option<f64>,
}

/// Periodic Guider event data
#[derive(Serialize, Deserialize)]
pub struct GuiderDeviceInfoData {
    pub can_clear_calibration: bool,

    pub can_get_lock_postion: bool,

    pub can_set_shift_rate: bool,

    pub connected: bool,

    pub pixel_scale: Option<f64>,

    pub rms_error: Option<RmsError>,
}

#[derive(Serialize, Deserialize)]
pub struct RmsError {
    pub dec: RmsUnit,

    pub peak_dec: RmsUnit,

    pub peak_ra: RmsUnit,

    pub ra: RmsUnit,

    pub total: RmsUnit,
}

#[derive(Serialize, Deserialize)]
pub struct RmsUnit {
    pub arc_seconds: f64,

    pub pixel: f64,
}

/// Periodic Mount event data
#[derive(Serialize, Deserialize)]
pub struct MountDeviceInfoData {
    pub alignment_mode: Option<AlignmentMode>,

    pub altitude: Option<f64>,

    pub at_home: Option<bool>,

    pub at_park: Option<bool>,

    pub azimuth: Option<f64>,

    pub can_find_home: Option<bool>,

    pub can_move_primary_axis: Option<bool>,

    pub can_move_secondary_axis: Option<bool>,

    pub can_park: Option<bool>,

    pub can_pulse_guide: Option<bool>,

    pub can_set_declination_rate: Option<bool>,

    pub can_set_park: Option<bool>,

    pub can_set_pier_side: Option<bool>,

    pub can_set_right_ascension_rate: Option<bool>,

    pub can_set_tracking: Option<bool>,

    pub can_slew: Option<bool>,

    pub can_slew_alt_az: Option<bool>,

    pub connected: bool,

    pub declination: Option<f64>,

    pub equatorial_system: Option<Epoch>,

    pub guide_rate_dec_arc_sec_per_sec: Option<f64>,

    pub guide_rate_ra_arc_sec_per_sec: Option<f64>,

    pub has_unknown_epoch: Option<bool>,

    pub is_pulse_guiding: Option<bool>,

    pub primary_axis_rates: Option<Vec<Vec<f64>>>,

    pub right_ascension: Option<f64>,

    pub secondary_axis_rates: Option<Vec<Vec<f64>>>,

    pub side_of_pier: Option<PierSide>,

    pub sidereal_time: Option<f64>,

    pub site_elevation: Option<f64>,

    pub site_latitude: Option<f64>,

    pub site_longitude: Option<f64>,

    pub slewing: Option<bool>,

    pub target_coordinates: Option<Coordinates>,

    pub target_side_of_pier: Option<PierSide>,

    pub time_to_meridian_flip: Option<f64>,

    pub tracking_enabled: Option<bool>,

    pub tracking_modes: Option<Vec<TrackingMode>>,

    pub tracking_rate: Option<TrackingRate>,

    pub utc_date: Option<String>,
}

#[derive(Serialize, Deserialize)]
pub enum AlignmentMode {
    #[serde(rename = "AltAz")]
    AltAz,

    #[serde(rename = "GermanPolar")]
    GermanPolar,

    Polar,
}

#[derive(Serialize, Deserialize)]
pub enum Epoch {
    B1950,

    J2000,

    J2050,

    #[serde(rename = "JNOW")]
    Jnow,
}

#[derive(Serialize, Deserialize)]
pub enum PierSide {
    East,

    Unknown,

    West,
}

#[derive(Serialize, Deserialize)]
pub struct Coordinates {
    pub dec_degrees: Option<f64>,

    pub epoch: Option<Epoch>,

    pub ra_degrees: Option<f64>,
}

#[derive(Serialize, Deserialize)]
pub enum TrackingMode {
    Custom,

    King,

    Lunar,

    Sidereal,

    Solar,

    Stopped,
}

#[derive(Serialize, Deserialize)]
pub struct TrackingRate {
    pub custom_dec_rate: Option<f64>,

    pub custom_ra_rate: Option<f64>,

    pub tracking_mode: Option<TrackingMode>,
}

/// Event that fires before or after a meridian flip
#[derive(Serialize, Deserialize)]
pub struct MountFlipData {
    pub flip_event: FlipEvent,

    pub success: Option<bool>,

    pub target_coordinates: Option<Coordinates>,
}

#[derive(Serialize, Deserialize)]
#[serde(rename_all = "snake_case")]
pub enum FlipEvent {
    After,

    Before,
}

/// Event after the mount has moved
#[derive(Serialize, Deserialize)]
pub struct MountMovedData {
    pub from_coordinates: Option<Coordinates>,

    pub move_type: MoveType,

    pub to_coordinates: Option<Coordinates>,
}

#[derive(Serialize, Deserialize)]
#[serde(rename_all = "snake_case")]
pub enum MoveType {
    Homed,

    Parked,

    Slewed,

    Unparked,
}

/// Periodic Rotator event data
#[derive(Serialize, Deserialize)]
pub struct RotatorDeviceInfoData {
    pub can_reverse: bool,

    pub connected: bool,

    pub is_moving: bool,

    pub mechanical_position: Option<f64>,

    pub position: Option<f64>,

    pub reverse: bool,

    pub step_size: Option<f64>,

    pub synced: bool,
}

/// Safety monitor event data
#[derive(Serialize, Deserialize)]
pub struct SafetyDeviceInfoData {
    pub connected: bool,

    pub is_safe: bool,
}

/// Safety monitor is_safe changed
#[derive(Serialize, Deserialize)]
pub struct SafetyChangeData {
    pub is_safe: bool,
}

/// Periodic Switch event data
#[derive(Serialize, Deserialize)]
pub struct SwitchDeviceInfoData {
    pub connected: bool,

    pub readable_switches: Option<Vec<ReadableSwitch>>,

    pub writeable_switches: Option<Vec<WriteableSwitch>>,
}

#[derive(Serialize, Deserialize)]
pub struct ReadableSwitch {
    pub description: Option<String>,

    pub id: i64,

    pub name: Option<String>,

    pub value: Option<f64>,
}

#[derive(Serialize, Deserialize)]
pub struct WriteableSwitch {
    pub description: Option<String>,

    pub id: i64,

    pub maximum: f64,

    pub minimum: f64,

    pub name: Option<String>,

    pub step_size: f64,

    pub target_value: f64,

    pub value: Option<f64>,
}

/// Weather station sensor readings
#[derive(Serialize, Deserialize)]
pub struct WeatherDeviceInfoData {
    /// Cloud cover as a percentage (0–100); null if unsupported by the device
    pub cloud_cover: Option<f64>,

    pub connected: bool,

    /// Dew point in degrees Celsius; null if unsupported by the device
    pub dew_point: Option<f64>,

    /// Relative humidity as a percentage (0–100); null if unsupported by the device
    pub humidity: Option<f64>,

    /// Atmospheric pressure in hPa; null if unsupported by the device
    pub pressure: Option<f64>,

    /// Rain rate in mm/h; null if unsupported by the device
    pub rain_rate: Option<f64>,

    /// Sky quality in magnitudes per square arcsecond; null if unsupported by the device
    pub sky_quality: Option<f64>,

    /// Sky temperature in degrees Celsius; null if unsupported by the device
    pub sky_temperature: Option<f64>,

    /// Seeing estimate — star FWHM in arcseconds; null if unsupported by the device
    pub star_fwhm: Option<f64>,

    /// Ambient temperature in degrees Celsius; null if unsupported by the device
    pub temperature: Option<f64>,

    /// Wind direction in degrees (0–360); null if unsupported by the device
    pub wind_direction: Option<f64>,

    /// Wind gust speed in m/s; null if unsupported by the device
    pub wind_gust: Option<f64>,

    /// Wind speed in m/s; null if unsupported by the device
    pub wind_speed: Option<f64>,
}
