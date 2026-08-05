// Example code that deserializes and serializes the model.
// extern crate serde;
// #[macro_use]
// extern crate serde_derive;
// extern crate serde_json;
//
// use generated_module::CameraData;
//
// fn main() {
//     let json = r#"{"answer": 42}"#;
//     let model: CameraData = serde_json::from_str(&json).unwrap();
// }

use serde::{Serialize, Deserialize};

/// Camera device state snapshot
#[derive(Serialize, Deserialize)]
pub struct CameraData {
    pub connected: bool,

    pub cooler_on: bool,

    /// Cooler power as a percentage (0–100); null if unsupported by the device
    pub cooler_power: Option<f64>,

    /// UTC time when the current or last exposure ends; null if none
    pub exposure_end_time: Option<String>,

    pub is_exposing: bool,

    /// Duration of last image download in seconds
    pub last_download_time: f64,

    /// Camera device name; null if not connected
    pub name: Option<String>,

    /// Sensor temperature in degrees Celsius; null if unsupported by the device
    pub temperature: Option<f64>,

    /// Target cooler temperature in degrees Celsius; null if unsupported by the device
    pub temperature_set_point: Option<f64>,
}

/// Telescope mount state snapshot
#[derive(Serialize, Deserialize)]
pub struct TelescopeData {
    /// Current altitude in decimal degrees; null if unsupported by the device
    pub altitude: Option<f64>,

    /// Current azimuth in decimal degrees (0–360); null if unsupported by the device
    pub azimuth: Option<f64>,

    pub connected: bool,

    /// Current Dec in decimal degrees (−90 to +90); null if unsupported by the device
    pub declination: Option<f64>,

    /// Mount device name; null if not connected
    pub name: Option<String>,

    /// Current RA in decimal hours (0–24); null if unsupported by the device
    pub right_ascension: Option<f64>,

    /// Observer elevation in meters above sea level; null if unsupported by the device
    pub site_elevation: Option<f64>,

    /// Observer latitude in decimal degrees; null if unsupported by the device
    pub site_latitude: Option<f64>,

    /// Observer longitude in decimal degrees; null if unsupported by the device
    pub site_longitude: Option<f64>,

    pub slewing: bool,

    pub tracking_enabled: bool,
}

/// Image capture metadata at the time of save
#[derive(Serialize, Deserialize)]
pub struct ImagingData {
    /// Sequential exposure number within the sequence
    pub exposure_number: i64,

    /// Exposure duration in seconds
    pub exposure_time: f64,

    /// Filter wheel filter name; null if no filter wheel
    pub filter: Option<String>,

    /// Camera gain; −1 if not applicable
    pub gain: i64,

    /// Image type — LIGHT, DARK, FLAT, BIAS, etc.; null if unknown
    pub image_type: Option<String>,

    /// Camera offset; −1 if not applicable
    pub offset: i64,

    /// Imaging target name from the sequence; null if not set
    pub target_name: Option<String>,
}

/// Weather station sensor readings
#[derive(Serialize, Deserialize)]
pub struct WeatherData {
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

/// General device connection data
#[derive(Serialize, Deserialize)]
pub struct DeviceData {
    pub connected: bool,

    pub device_type: String,
}

/// Safety monitor event data
#[derive(Serialize, Deserialize)]
pub struct SafetyData {
    pub connected: bool,

    pub description: Option<String>,

    pub device_id: Option<String>,

    pub display_name: Option<String>,

    pub driver_info: Option<String>,

    pub driver_version: Option<String>,

    pub is_safe: bool,

    pub name: Option<String>,
}

/// Safety monitor is_safe changed
#[derive(Serialize, Deserialize)]
pub struct SafetyChangeData {
    pub is_safe: bool,
}
