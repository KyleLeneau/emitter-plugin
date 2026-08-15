// This file was generated from JSON Schema using quicktype, do not modify it directly.
// To parse the JSON, add this file to your project and do:
//
//   let deviceConnectionData = try DeviceConnectionData(json)
//   let domeDeviceInfoData = try DomeDeviceInfoData(json)
//   let filterWheelDeviceInfoData = try FilterWheelDeviceInfoData(json)
//   let flatPanelDeviceInfoData = try FlatPanelDeviceInfoData(json)
//   let safetyDeviceInfoData = try SafetyDeviceInfoData(json)
//   let safetyChangeData = try SafetyChangeData(json)
//   let weatherDeviceInfoData = try WeatherDeviceInfoData(json)

import Foundation

/// General device connection data
// MARK: - DeviceConnectionData
struct DeviceConnectionData: Codable {
    let connected: Bool
    /// Device description
    let description: String?
    /// Driver ID
    let deviceID: String?
    let deviceType: DeviceType
    /// A friendly name of the device for dipslay
    let displayName: String?
    /// Driver name and info
    let driverInfo: String?
    /// Driver version information
    let driverVersion: String?
    /// The name of the device
    let name: String?

    enum CodingKeys: String, CodingKey {
        case connected, description
        case deviceID = "device_id"
        case deviceType = "device_type"
        case displayName = "display_name"
        case driverInfo = "driver_info"
        case driverVersion = "driver_version"
        case name
    }
}

// MARK: DeviceConnectionData convenience initializers and mutators

extension DeviceConnectionData {
    init(data: Data) throws {
        self = try newJSONDecoder().decode(DeviceConnectionData.self, from: data)
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
        deviceType: DeviceType? = nil,
        displayName: String?? = nil,
        driverInfo: String?? = nil,
        driverVersion: String?? = nil,
        name: String?? = nil
    ) -> DeviceConnectionData {
        return DeviceConnectionData(
            connected: connected ?? self.connected,
            description: description ?? self.description,
            deviceID: deviceID ?? self.deviceID,
            deviceType: deviceType ?? self.deviceType,
            displayName: displayName ?? self.displayName,
            driverInfo: driverInfo ?? self.driverInfo,
            driverVersion: driverVersion ?? self.driverVersion,
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

enum DeviceType: String, Codable {
    case camera = "Camera"
    case deviceTypeSwitch = "Switch"
    case dome = "Dome"
    case filterWheel = "FilterWheel"
    case flatPanel = "FlatPanel"
    case focuser = "Focuser"
    case guider = "Guider"
    case mount = "Mount"
    case rotator = "Rotator"
    case safetyMonitor = "SafetyMonitor"
    case weather = "Weather"
}

/// Dome device state
// MARK: - DomeDeviceInfoData
struct DomeDeviceInfoData: Codable {
    let altitudeDegrees: Double?
    let applicationFollowing, atHome, atPark: Bool
    let azimuthDegrees: Double?
    let canFindHome, canPark, canSetAzimuth, canSetPark: Bool
    let canSetShutter, canSyncAzimuth, connected, driverCanFollow: Bool
    let driverFollowing: Bool
    let followingType: String?
    let shutterState: ShutterState
    let slewing: Bool

    enum CodingKeys: String, CodingKey {
        case altitudeDegrees = "altitude_degrees"
        case applicationFollowing = "application_following"
        case atHome = "at_home"
        case atPark = "at_park"
        case azimuthDegrees = "azimuth_degrees"
        case canFindHome = "can_find_home"
        case canPark = "can_park"
        case canSetAzimuth = "can_set_azimuth"
        case canSetPark = "can_set_park"
        case canSetShutter = "can_set_shutter"
        case canSyncAzimuth = "can_sync_azimuth"
        case connected
        case driverCanFollow = "driver_can_follow"
        case driverFollowing = "driver_following"
        case followingType = "following_type"
        case shutterState = "shutter_state"
        case slewing
    }
}

// MARK: DomeDeviceInfoData convenience initializers and mutators

extension DomeDeviceInfoData {
    init(data: Data) throws {
        self = try newJSONDecoder().decode(DomeDeviceInfoData.self, from: data)
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
        altitudeDegrees: Double?? = nil,
        applicationFollowing: Bool? = nil,
        atHome: Bool? = nil,
        atPark: Bool? = nil,
        azimuthDegrees: Double?? = nil,
        canFindHome: Bool? = nil,
        canPark: Bool? = nil,
        canSetAzimuth: Bool? = nil,
        canSetPark: Bool? = nil,
        canSetShutter: Bool? = nil,
        canSyncAzimuth: Bool? = nil,
        connected: Bool? = nil,
        driverCanFollow: Bool? = nil,
        driverFollowing: Bool? = nil,
        followingType: String?? = nil,
        shutterState: ShutterState? = nil,
        slewing: Bool? = nil
    ) -> DomeDeviceInfoData {
        return DomeDeviceInfoData(
            altitudeDegrees: altitudeDegrees ?? self.altitudeDegrees,
            applicationFollowing: applicationFollowing ?? self.applicationFollowing,
            atHome: atHome ?? self.atHome,
            atPark: atPark ?? self.atPark,
            azimuthDegrees: azimuthDegrees ?? self.azimuthDegrees,
            canFindHome: canFindHome ?? self.canFindHome,
            canPark: canPark ?? self.canPark,
            canSetAzimuth: canSetAzimuth ?? self.canSetAzimuth,
            canSetPark: canSetPark ?? self.canSetPark,
            canSetShutter: canSetShutter ?? self.canSetShutter,
            canSyncAzimuth: canSyncAzimuth ?? self.canSyncAzimuth,
            connected: connected ?? self.connected,
            driverCanFollow: driverCanFollow ?? self.driverCanFollow,
            driverFollowing: driverFollowing ?? self.driverFollowing,
            followingType: followingType ?? self.followingType,
            shutterState: shutterState ?? self.shutterState,
            slewing: slewing ?? self.slewing
        )
    }

    func jsonData() throws -> Data {
        return try newJSONEncoder().encode(self)
    }

    func jsonString(encoding: String.Encoding = .utf8) throws -> String? {
        return String(data: try self.jsonData(), encoding: encoding)
    }
}

enum ShutterState: String, Codable {
    case closed = "Closed"
    case closing = "Closing"
    case error = "Error"
    case none = "None"
    case opening = "Opening"
    case shutterStateOpen = "Open"
}

/// Filter Wheel device state
// MARK: - FilterWheelDeviceInfoData
struct FilterWheelDeviceInfoData: Codable {
    let connected, isMoving: Bool
    let selectedFilter: FilterInfo?

    enum CodingKeys: String, CodingKey {
        case connected
        case isMoving = "is_moving"
        case selectedFilter = "selected_filter"
    }
}

// MARK: FilterWheelDeviceInfoData convenience initializers and mutators

extension FilterWheelDeviceInfoData {
    init(data: Data) throws {
        self = try newJSONDecoder().decode(FilterWheelDeviceInfoData.self, from: data)
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
        isMoving: Bool? = nil,
        selectedFilter: FilterInfo?? = nil
    ) -> FilterWheelDeviceInfoData {
        return FilterWheelDeviceInfoData(
            connected: connected ?? self.connected,
            isMoving: isMoving ?? self.isMoving,
            selectedFilter: selectedFilter ?? self.selectedFilter
        )
    }

    func jsonData() throws -> Data {
        return try newJSONEncoder().encode(self)
    }

    func jsonString(encoding: String.Encoding = .utf8) throws -> String? {
        return String(data: try self.jsonData(), encoding: encoding)
    }
}

/// Info on a specific filter
// MARK: - FilterInfo
struct FilterInfo: Codable {
    let autoFocusBinning: String?
    let autoFocusGain, autoFocusOffset, autoFocusTime: Double?
    let isAFFilter: Bool?
    let name: String?
    let offset, postion: Double?

    enum CodingKeys: String, CodingKey {
        case autoFocusBinning = "auto_focus_binning"
        case autoFocusGain = "auto_focus_gain"
        case autoFocusOffset = "auto_focus_offset"
        case autoFocusTime = "auto_focus_time"
        case isAFFilter = "is_af_filter"
        case name, offset, postion
    }
}

// MARK: FilterInfo convenience initializers and mutators

extension FilterInfo {
    init(data: Data) throws {
        self = try newJSONDecoder().decode(FilterInfo.self, from: data)
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
        autoFocusBinning: String?? = nil,
        autoFocusGain: Double?? = nil,
        autoFocusOffset: Double?? = nil,
        autoFocusTime: Double?? = nil,
        isAFFilter: Bool?? = nil,
        name: String?? = nil,
        offset: Double?? = nil,
        postion: Double?? = nil
    ) -> FilterInfo {
        return FilterInfo(
            autoFocusBinning: autoFocusBinning ?? self.autoFocusBinning,
            autoFocusGain: autoFocusGain ?? self.autoFocusGain,
            autoFocusOffset: autoFocusOffset ?? self.autoFocusOffset,
            autoFocusTime: autoFocusTime ?? self.autoFocusTime,
            isAFFilter: isAFFilter ?? self.isAFFilter,
            name: name ?? self.name,
            offset: offset ?? self.offset,
            postion: postion ?? self.postion
        )
    }

    func jsonData() throws -> Data {
        return try newJSONEncoder().encode(self)
    }

    func jsonString(encoding: String.Encoding = .utf8) throws -> String? {
        return String(data: try self.jsonData(), encoding: encoding)
    }
}

/// Flat Panel device state
// MARK: - FlatPanelDeviceInfoData
struct FlatPanelDeviceInfoData: Codable {
    let brightness: Int
    let connected: Bool
    let coverState: String
    let lightOn: Bool
    let maxBrightness, minBrightness: Int?
    let supportsOnOff, supportsOpenClose: Bool?

    enum CodingKeys: String, CodingKey {
        case brightness, connected
        case coverState = "cover_state"
        case lightOn = "light_on"
        case maxBrightness = "max_brightness"
        case minBrightness = "min_brightness"
        case supportsOnOff = "supports_on_off"
        case supportsOpenClose = "supports_open_close"
    }
}

// MARK: FlatPanelDeviceInfoData convenience initializers and mutators

extension FlatPanelDeviceInfoData {
    init(data: Data) throws {
        self = try newJSONDecoder().decode(FlatPanelDeviceInfoData.self, from: data)
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
        brightness: Int? = nil,
        connected: Bool? = nil,
        coverState: String? = nil,
        lightOn: Bool? = nil,
        maxBrightness: Int?? = nil,
        minBrightness: Int?? = nil,
        supportsOnOff: Bool?? = nil,
        supportsOpenClose: Bool?? = nil
    ) -> FlatPanelDeviceInfoData {
        return FlatPanelDeviceInfoData(
            brightness: brightness ?? self.brightness,
            connected: connected ?? self.connected,
            coverState: coverState ?? self.coverState,
            lightOn: lightOn ?? self.lightOn,
            maxBrightness: maxBrightness ?? self.maxBrightness,
            minBrightness: minBrightness ?? self.minBrightness,
            supportsOnOff: supportsOnOff ?? self.supportsOnOff,
            supportsOpenClose: supportsOpenClose ?? self.supportsOpenClose
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
// MARK: - SafetyDeviceInfoData
struct SafetyDeviceInfoData: Codable {
    let connected, isSafe: Bool

    enum CodingKeys: String, CodingKey {
        case connected
        case isSafe = "is_safe"
    }
}

// MARK: SafetyDeviceInfoData convenience initializers and mutators

extension SafetyDeviceInfoData {
    init(data: Data) throws {
        self = try newJSONDecoder().decode(SafetyDeviceInfoData.self, from: data)
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
        isSafe: Bool? = nil
    ) -> SafetyDeviceInfoData {
        return SafetyDeviceInfoData(
            connected: connected ?? self.connected,
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

/// Weather station sensor readings
// MARK: - WeatherDeviceInfoData
struct WeatherDeviceInfoData: Codable {
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

// MARK: WeatherDeviceInfoData convenience initializers and mutators

extension WeatherDeviceInfoData {
    init(data: Data) throws {
        self = try newJSONDecoder().decode(WeatherDeviceInfoData.self, from: data)
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
    ) -> WeatherDeviceInfoData {
        return WeatherDeviceInfoData(
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
