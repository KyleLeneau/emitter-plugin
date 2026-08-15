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
    pub connected: bool,
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
    pub connected: bool,
}

/// Periodic Mount event data
#[derive(Serialize, Deserialize)]
pub struct MountDeviceInfoData {
    pub connected: bool,
}

/// Periodic Rotator event data
#[derive(Serialize, Deserialize)]
pub struct RotatorDeviceInfoData {
    pub connected: bool,
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
