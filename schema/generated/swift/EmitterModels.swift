// This file was generated from JSON Schema using quicktype, do not modify it directly.
// To parse the JSON, add this file to your project and do:
//
//   let cameraData = try CameraData(json)
//   let telescopeData = try TelescopeData(json)
//   let imagingData = try ImagingData(json)
//   let weatherData = try WeatherData(json)
//   let deviceData = try DeviceData(json)
//   let safetyData = try SafetyData(json)
//   let safetyChangeData = try SafetyChangeData(json)

import Foundation

/// Camera device state snapshot
// MARK: - CameraData
struct CameraData: Codable {
    let connected, coolerOn: Bool
    /// Cooler power as a percentage (0–100); null if unsupported by the device
    let coolerPower: Double?
    /// UTC time when the current or last exposure ends; null if none
    let exposureEndTime: Date?
    let isExposing: Bool
    /// Duration of last image download in seconds
    let lastDownloadTime: Double
    /// Camera device name; null if not connected
    let name: String?
    /// Sensor temperature in degrees Celsius; null if unsupported by the device
    let temperature: Double?
    /// Target cooler temperature in degrees Celsius; null if unsupported by the device
    let temperatureSetPoint: Double?

    enum CodingKeys: String, CodingKey {
        case connected
        case coolerOn = "cooler_on"
        case coolerPower = "cooler_power"
        case exposureEndTime = "exposure_end_time"
        case isExposing = "is_exposing"
        case lastDownloadTime = "last_download_time"
        case name, temperature
        case temperatureSetPoint = "temperature_set_point"
    }
}

// MARK: CameraData convenience initializers and mutators

extension CameraData {
    init(data: Data) throws {
        self = try newJSONDecoder().decode(CameraData.self, from: data)
    }

    init(_ json: String, using encoding: String.Encoding = .utf8) throws {
        guard let data = json.data(using: encoding) else {
            throw NSError(domain: "JSONDecoding", code: 0, userInfo: nil)
        }
        try self.init(data: data)
    }

    init(fromURL url: URL) throws {
        try self.init(data: try Data(contentsOf: url))
    }

    func with(
        connected: Bool? = nil,
        coolerOn: Bool? = nil,
        coolerPower: Double?? = nil,
        exposureEndTime: Date?? = nil,
        isExposing: Bool? = nil,
        lastDownloadTime: Double? = nil,
        name: String?? = nil,
        temperature: Double?? = nil,
        temperatureSetPoint: Double?? = nil
    ) -> CameraData {
        return CameraData(
            connected: connected ?? self.connected,
            coolerOn: coolerOn ?? self.coolerOn,
            coolerPower: coolerPower ?? self.coolerPower,
            exposureEndTime: exposureEndTime ?? self.exposureEndTime,
            isExposing: isExposing ?? self.isExposing,
            lastDownloadTime: lastDownloadTime ?? self.lastDownloadTime,
            name: name ?? self.name,
            temperature: temperature ?? self.temperature,
            temperatureSetPoint: temperatureSetPoint ?? self.temperatureSetPoint
        )
    }

    func jsonData() throws -> Data {
        return try newJSONEncoder().encode(self)
    }

    func jsonString(encoding: String.Encoding = .utf8) throws -> String? {
        return String(data: try self.jsonData(), encoding: encoding)
    }
}

/// Telescope mount state snapshot
// MARK: - TelescopeData
struct TelescopeData: Codable {
    /// Current altitude in decimal degrees; null if unsupported by the device
    let altitude: Double?
    /// Current azimuth in decimal degrees (0–360); null if unsupported by the device
    let azimuth: Double?
    let connected: Bool
    /// Current Dec in decimal degrees (−90 to +90); null if unsupported by the device
    let declination: Double?
    /// Mount device name; null if not connected
    let name: String?
    /// Current RA in decimal hours (0–24); null if unsupported by the device
    let rightAscension: Double?
    /// Observer elevation in meters above sea level; null if unsupported by the device
    let siteElevation: Double?
    /// Observer latitude in decimal degrees; null if unsupported by the device
    let siteLatitude: Double?
    /// Observer longitude in decimal degrees; null if unsupported by the device
    let siteLongitude: Double?
    let slewing, trackingEnabled: Bool

    enum CodingKeys: String, CodingKey {
        case altitude, azimuth, connected, declination, name
        case rightAscension = "right_ascension"
        case siteElevation = "site_elevation"
        case siteLatitude = "site_latitude"
        case siteLongitude = "site_longitude"
        case slewing
        case trackingEnabled = "tracking_enabled"
    }
}

// MARK: TelescopeData convenience initializers and mutators

extension TelescopeData {
    init(data: Data) throws {
        self = try newJSONDecoder().decode(TelescopeData.self, from: data)
    }

    init(_ json: String, using encoding: String.Encoding = .utf8) throws {
        guard let data = json.data(using: encoding) else {
            throw NSError(domain: "JSONDecoding", code: 0, userInfo: nil)
        }
        try self.init(data: data)
    }

    init(fromURL url: URL) throws {
        try self.init(data: try Data(contentsOf: url))
    }

    func with(
        altitude: Double?? = nil,
        azimuth: Double?? = nil,
        connected: Bool? = nil,
        declination: Double?? = nil,
        name: String?? = nil,
        rightAscension: Double?? = nil,
        siteElevation: Double?? = nil,
        siteLatitude: Double?? = nil,
        siteLongitude: Double?? = nil,
        slewing: Bool? = nil,
        trackingEnabled: Bool? = nil
    ) -> TelescopeData {
        return TelescopeData(
            altitude: altitude ?? self.altitude,
            azimuth: azimuth ?? self.azimuth,
            connected: connected ?? self.connected,
            declination: declination ?? self.declination,
            name: name ?? self.name,
            rightAscension: rightAscension ?? self.rightAscension,
            siteElevation: siteElevation ?? self.siteElevation,
            siteLatitude: siteLatitude ?? self.siteLatitude,
            siteLongitude: siteLongitude ?? self.siteLongitude,
            slewing: slewing ?? self.slewing,
            trackingEnabled: trackingEnabled ?? self.trackingEnabled
        )
    }

    func jsonData() throws -> Data {
        return try newJSONEncoder().encode(self)
    }

    func jsonString(encoding: String.Encoding = .utf8) throws -> String? {
        return String(data: try self.jsonData(), encoding: encoding)
    }
}

/// Image capture metadata at the time of save
// MARK: - ImagingData
struct ImagingData: Codable {
    /// Sequential exposure number within the sequence
    let exposureNumber: Int
    /// Exposure duration in seconds
    let exposureTime: Double
    /// Filter wheel filter name; null if no filter wheel
    let filter: String?
    /// Camera gain; −1 if not applicable
    let gain: Int
    /// Image type — LIGHT, DARK, FLAT, BIAS, etc.; null if unknown
    let imageType: String?
    /// Camera offset; −1 if not applicable
    let offset: Int
    /// Imaging target name from the sequence; null if not set
    let targetName: String?

    enum CodingKeys: String, CodingKey {
        case exposureNumber = "exposure_number"
        case exposureTime = "exposure_time"
        case filter, gain
        case imageType = "image_type"
        case offset
        case targetName = "target_name"
    }
}

// MARK: ImagingData convenience initializers and mutators

extension ImagingData {
    init(data: Data) throws {
        self = try newJSONDecoder().decode(ImagingData.self, from: data)
    }

    init(_ json: String, using encoding: String.Encoding = .utf8) throws {
        guard let data = json.data(using: encoding) else {
            throw NSError(domain: "JSONDecoding", code: 0, userInfo: nil)
        }
        try self.init(data: data)
    }

    init(fromURL url: URL) throws {
        try self.init(data: try Data(contentsOf: url))
    }

    func with(
        exposureNumber: Int? = nil,
        exposureTime: Double? = nil,
        filter: String?? = nil,
        gain: Int? = nil,
        imageType: String?? = nil,
        offset: Int? = nil,
        targetName: String?? = nil
    ) -> ImagingData {
        return ImagingData(
            exposureNumber: exposureNumber ?? self.exposureNumber,
            exposureTime: exposureTime ?? self.exposureTime,
            filter: filter ?? self.filter,
            gain: gain ?? self.gain,
            imageType: imageType ?? self.imageType,
            offset: offset ?? self.offset,
            targetName: targetName ?? self.targetName
        )
    }

    func jsonData() throws -> Data {
        return try newJSONEncoder().encode(self)
    }

    func jsonString(encoding: String.Encoding = .utf8) throws -> String? {
        return String(data: try self.jsonData(), encoding: encoding)
    }
}

/// Weather station sensor readings
// MARK: - WeatherData
struct WeatherData: Codable {
    /// Cloud cover as a percentage (0–100); null if unsupported by the device
    let cloudCover: Double?
    let connected: Bool
    /// Dew point in degrees Celsius; null if unsupported by the device
    let dewPoint: Double?
    /// Relative humidity as a percentage (0–100); null if unsupported by the device
    let humidity: Double?
    /// Atmospheric pressure in hPa; null if unsupported by the device
    let pressure: Double?
    /// Rain rate in mm/h; null if unsupported by the device
    let rainRate: Double?
    /// Sky quality in magnitudes per square arcsecond; null if unsupported by the device
    let skyQuality: Double?
    /// Sky temperature in degrees Celsius; null if unsupported by the device
    let skyTemperature: Double?
    /// Seeing estimate — star FWHM in arcseconds; null if unsupported by the device
    let starFwhm: Double?
    /// Ambient temperature in degrees Celsius; null if unsupported by the device
    let temperature: Double?
    /// Wind direction in degrees (0–360); null if unsupported by the device
    let windDirection: Double?
    /// Wind gust speed in m/s; null if unsupported by the device
    let windGust: Double?
    /// Wind speed in m/s; null if unsupported by the device
    let windSpeed: Double?

    enum CodingKeys: String, CodingKey {
        case cloudCover = "cloud_cover"
        case connected
        case dewPoint = "dew_point"
        case humidity, pressure
        case rainRate = "rain_rate"
        case skyQuality = "sky_quality"
        case skyTemperature = "sky_temperature"
        case starFwhm = "star_fwhm"
        case temperature
        case windDirection = "wind_direction"
        case windGust = "wind_gust"
        case windSpeed = "wind_speed"
    }
}

// MARK: WeatherData convenience initializers and mutators

extension WeatherData {
    init(data: Data) throws {
        self = try newJSONDecoder().decode(WeatherData.self, from: data)
    }

    init(_ json: String, using encoding: String.Encoding = .utf8) throws {
        guard let data = json.data(using: encoding) else {
            throw NSError(domain: "JSONDecoding", code: 0, userInfo: nil)
        }
        try self.init(data: data)
    }

    init(fromURL url: URL) throws {
        try self.init(data: try Data(contentsOf: url))
    }

    func with(
        cloudCover: Double?? = nil,
        connected: Bool? = nil,
        dewPoint: Double?? = nil,
        humidity: Double?? = nil,
        pressure: Double?? = nil,
        rainRate: Double?? = nil,
        skyQuality: Double?? = nil,
        skyTemperature: Double?? = nil,
        starFwhm: Double?? = nil,
        temperature: Double?? = nil,
        windDirection: Double?? = nil,
        windGust: Double?? = nil,
        windSpeed: Double?? = nil
    ) -> WeatherData {
        return WeatherData(
            cloudCover: cloudCover ?? self.cloudCover,
            connected: connected ?? self.connected,
            dewPoint: dewPoint ?? self.dewPoint,
            humidity: humidity ?? self.humidity,
            pressure: pressure ?? self.pressure,
            rainRate: rainRate ?? self.rainRate,
            skyQuality: skyQuality ?? self.skyQuality,
            skyTemperature: skyTemperature ?? self.skyTemperature,
            starFwhm: starFwhm ?? self.starFwhm,
            temperature: temperature ?? self.temperature,
            windDirection: windDirection ?? self.windDirection,
            windGust: windGust ?? self.windGust,
            windSpeed: windSpeed ?? self.windSpeed
        )
    }

    func jsonData() throws -> Data {
        return try newJSONEncoder().encode(self)
    }

    func jsonString(encoding: String.Encoding = .utf8) throws -> String? {
        return String(data: try self.jsonData(), encoding: encoding)
    }
}

/// General device connection data
// MARK: - DeviceData
struct DeviceData: Codable {
    let connected: Bool
    let deviceType: String

    enum CodingKeys: String, CodingKey {
        case connected
        case deviceType = "device_type"
    }
}

// MARK: DeviceData convenience initializers and mutators

extension DeviceData {
    init(data: Data) throws {
        self = try newJSONDecoder().decode(DeviceData.self, from: data)
    }

    init(_ json: String, using encoding: String.Encoding = .utf8) throws {
        guard let data = json.data(using: encoding) else {
            throw NSError(domain: "JSONDecoding", code: 0, userInfo: nil)
        }
        try self.init(data: data)
    }

    init(fromURL url: URL) throws {
        try self.init(data: try Data(contentsOf: url))
    }

    func with(
        connected: Bool? = nil,
        deviceType: String? = nil
    ) -> DeviceData {
        return DeviceData(
            connected: connected ?? self.connected,
            deviceType: deviceType ?? self.deviceType
        )
    }

    func jsonData() throws -> Data {
        return try newJSONEncoder().encode(self)
    }

    func jsonString(encoding: String.Encoding = .utf8) throws -> String? {
        return String(data: try self.jsonData(), encoding: encoding)
    }
}

/// Safety monitor event data
// MARK: - SafetyData
struct SafetyData: Codable {
    let connected: Bool
    let description, deviceID, displayName, driverInfo: String?
    let driverVersion: String?
    let isSafe: Bool
    let name: String?

    enum CodingKeys: String, CodingKey {
        case connected, description
        case deviceID = "device_id"
        case displayName = "display_name"
        case driverInfo = "driver_info"
        case driverVersion = "driver_version"
        case isSafe = "is_safe"
        case name
    }
}

// MARK: SafetyData convenience initializers and mutators

extension SafetyData {
    init(data: Data) throws {
        self = try newJSONDecoder().decode(SafetyData.self, from: data)
    }

    init(_ json: String, using encoding: String.Encoding = .utf8) throws {
        guard let data = json.data(using: encoding) else {
            throw NSError(domain: "JSONDecoding", code: 0, userInfo: nil)
        }
        try self.init(data: data)
    }

    init(fromURL url: URL) throws {
        try self.init(data: try Data(contentsOf: url))
    }

    func with(
        connected: Bool? = nil,
        description: String?? = nil,
        deviceID: String?? = nil,
        displayName: String?? = nil,
        driverInfo: String?? = nil,
        driverVersion: String?? = nil,
        isSafe: Bool? = nil,
        name: String?? = nil
    ) -> SafetyData {
        return SafetyData(
            connected: connected ?? self.connected,
            description: description ?? self.description,
            deviceID: deviceID ?? self.deviceID,
            displayName: displayName ?? self.displayName,
            driverInfo: driverInfo ?? self.driverInfo,
            driverVersion: driverVersion ?? self.driverVersion,
            isSafe: isSafe ?? self.isSafe,
            name: name ?? self.name
        )
    }

    func jsonData() throws -> Data {
        return try newJSONEncoder().encode(self)
    }

    func jsonString(encoding: String.Encoding = .utf8) throws -> String? {
        return String(data: try self.jsonData(), encoding: encoding)
    }
}

/// Safety monitor is_safe changed
// MARK: - SafetyChangeData
struct SafetyChangeData: Codable {
    let isSafe: Bool

    enum CodingKeys: String, CodingKey {
        case isSafe = "is_safe"
    }
}

// MARK: SafetyChangeData convenience initializers and mutators

extension SafetyChangeData {
    init(data: Data) throws {
        self = try newJSONDecoder().decode(SafetyChangeData.self, from: data)
    }

    init(_ json: String, using encoding: String.Encoding = .utf8) throws {
        guard let data = json.data(using: encoding) else {
            throw NSError(domain: "JSONDecoding", code: 0, userInfo: nil)
        }
        try self.init(data: data)
    }

    init(fromURL url: URL) throws {
        try self.init(data: try Data(contentsOf: url))
    }

    func with(
        isSafe: Bool? = nil
    ) -> SafetyChangeData {
        return SafetyChangeData(
            isSafe: isSafe ?? self.isSafe
        )
    }

    func jsonData() throws -> Data {
        return try newJSONEncoder().encode(self)
    }

    func jsonString(encoding: String.Encoding = .utf8) throws -> String? {
        return String(data: try self.jsonData(), encoding: encoding)
    }
}

// MARK: - Helper functions for creating encoders and decoders

func newJSONDecoder() -> JSONDecoder {
    let decoder = JSONDecoder()
    if #available(iOS 10.0, OSX 10.12, tvOS 10.0, watchOS 3.0, *) {
        decoder.dateDecodingStrategy = .iso8601
    }
    return decoder
}

func newJSONEncoder() -> JSONEncoder {
    let encoder = JSONEncoder()
    if #available(iOS 10.0, OSX 10.12, tvOS 10.0, watchOS 3.0, *) {
        encoder.dateEncodingStrategy = .iso8601
    }
    return encoder
}
