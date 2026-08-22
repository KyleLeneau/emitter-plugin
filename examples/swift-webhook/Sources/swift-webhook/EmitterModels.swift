// Vendored from schema/generated/swift/EmitterModels.swift — regenerate there with
// `./schema/build.sh generate` after a schema change, then re-copy the file here.
//
// This file was generated from JSON Schema using quicktype, do not modify it directly.
// To parse the JSON, add this file to your project and do:
//
//   let deviceConnectionData = try DeviceConnectionData(json)
//   let cameraDeviceInfoData = try CameraDeviceInfoData(json)
//   let cameraErrorData = try CameraErrorData(json)
//   let domeDeviceInfoData = try DomeDeviceInfoData(json)
//   let domeShutterData = try DomeShutterData(json)
//   let filterWheelDeviceInfoData = try FilterWheelDeviceInfoData(json)
//   let filterWheelChangeData = try FilterWheelChangeData(json)
//   let flatPanelDeviceInfoData = try FlatPanelDeviceInfoData(json)
//   let flatPanelLEDData = try FlatPanelLEDData(json)
//   let flatPanelStateData = try FlatPanelStateData(json)
//   let flatPanelValueData = try FlatPanelValueData(json)
//   let focuserDeviceInfoData = try FocuserDeviceInfoData(json)
//   let focuserChangeData = try FocuserChangeData(json)
//   let guiderDeviceInfoData = try GuiderDeviceInfoData(json)
//   let guiderDitherData = try GuiderDitherData(json)
//   let guiderStartData = try GuiderStartData(json)
//   let guiderStepData = try GuiderStepData(json)
//   let mountDeviceInfoData = try MountDeviceInfoData(json)
//   let mountFlipData = try MountFlipData(json)
//   let mountMovedData = try MountMovedData(json)
//   let rotatorDeviceInfoData = try RotatorDeviceInfoData(json)
//   let rotatorMovedData = try RotatorMovedData(json)
//   let safetyDeviceInfoData = try SafetyDeviceInfoData(json)
//   let safetyChangeData = try SafetyChangeData(json)
//   let switchDeviceInfoData = try SwitchDeviceInfoData(json)
//   let weatherDeviceInfoData = try WeatherDeviceInfoData(json)
//   let imageSavedData = try ImageSavedData(json)
//   let profileHorizonData = try ProfileHorizonData(json)
//   let profileListData = try ProfileListData(json)
//   let profileLocaleData = try ProfileLocaleData(json)
//   let profileLocationData = try ProfileLocationData(json)
//   let profileSelectedData = try ProfileSelectedData(json)
//   let sequenceEndData = try SequenceEndData(json)
//   let sequenceStartData = try SequenceStartData(json)
//   let targetSchedulerContainerStoppedData = try TargetSchedulerContainerStoppedData(json)
//   let targetSchedulerNewTargetStartData = try TargetSchedulerNewTargetStartData(json)
//   let targetSchedulerTargetCompleteData = try TargetSchedulerTargetCompleteData(json)
//   let targetSchedulerTargetStartData = try TargetSchedulerTargetStartData(json)
//   let targetSchedulerWaitStartData = try TargetSchedulerWaitStartData(json)

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

/// Periodic Camera event data
// MARK: - CameraDeviceInfoData
struct CameraDeviceInfoData: Codable {
    let battery, bayerOffsetX, bayerOffsetY, binX: Int?
    let binY: Int?
    let binningModes: [BinningMode]?
    let bitDepth: Int?
    let cameraState: CameraState?
    let canGetGain: Bool?
    let canSetGain, canSetOffset, canSetTemperature: Bool
    let canSetUSBLimit, canShowLiveView, canSubSample: Bool?
    let connected: Bool
    let coolerOn: Bool?
    let coolerPower: Double?
    let defaultGain, defaultOffset: Int?
    let dewHeaterOn: Bool?
    let electronsPerAdu: Double?
    let exposureEndTime: Date?
    let exposureMax, exposureMin: Double?
    let gain, gainMax, gainMin: Int?
    let gains: [Int]?
    let hasBattery, hasDewHeater: Bool?
    let hasShutter: Bool
    let isExposing, isSubSampleEnabled: Bool?
    let lastDownloadTime: Double?
    let liveViewEnabled: Bool?
    let normalReadoutMode, offset, offsetMax, offsetMin: Int?
    let pixelSize: Double?
    let readoutMode: Int?
    let readoutModes: [String]?
    let sensorType: SensorType?
    let snapReadoutMode, subSampleHeight, subSampleWidth, subSampleX: Int?
    let subSampleY: Int?
    let temeratureSetPoint, temperature: Double?
    let usbLimit, usbLimitMax, usbLimitMin, xSize: Int?
    let ySize: Int?

    enum CodingKeys: String, CodingKey {
        case battery
        case bayerOffsetX = "bayer_offset_x"
        case bayerOffsetY = "bayer_offset_y"
        case binX = "bin_x"
        case binY = "bin_y"
        case binningModes = "binning_modes"
        case bitDepth = "bit_depth"
        case cameraState = "camera_state"
        case canGetGain = "can_get_gain"
        case canSetGain = "can_set_gain"
        case canSetOffset = "can_set_offset"
        case canSetTemperature = "can_set_temperature"
        case canSetUSBLimit = "can_set_usb_limit"
        case canShowLiveView = "can_show_live_view"
        case canSubSample = "can_sub_sample"
        case connected
        case coolerOn = "cooler_on"
        case coolerPower = "cooler_power"
        case defaultGain = "default_gain"
        case defaultOffset = "default_offset"
        case dewHeaterOn = "dew_heater_on"
        case electronsPerAdu = "electrons_per_adu"
        case exposureEndTime = "exposure_end_time"
        case exposureMax = "exposure_max"
        case exposureMin = "exposure_min"
        case gain
        case gainMax = "gain_max"
        case gainMin = "gain_min"
        case gains
        case hasBattery = "has_battery"
        case hasDewHeater = "has_dew_heater"
        case hasShutter = "has_shutter"
        case isExposing = "is_exposing"
        case isSubSampleEnabled = "is_sub_sample_enabled"
        case lastDownloadTime = "last_download_time"
        case liveViewEnabled = "live_view_enabled"
        case normalReadoutMode = "normal_readout_mode"
        case offset
        case offsetMax = "offset_max"
        case offsetMin = "offset_min"
        case pixelSize = "pixel_size"
        case readoutMode = "readout_mode"
        case readoutModes = "readout_modes"
        case sensorType = "sensor_type"
        case snapReadoutMode = "snap_readout_mode"
        case subSampleHeight = "sub_sample_height"
        case subSampleWidth = "sub_sample_width"
        case subSampleX = "sub_sample_x"
        case subSampleY = "sub_sample_y"
        case temeratureSetPoint = "temerature_set_point"
        case temperature
        case usbLimit = "usb_limit"
        case usbLimitMax = "usb_limit_max"
        case usbLimitMin = "usb_limit_min"
        case xSize = "x_size"
        case ySize = "y_size"
    }
}

// MARK: CameraDeviceInfoData convenience initializers and mutators

extension CameraDeviceInfoData {
    init(data: Data) throws {
        self = try newJSONDecoder().decode(CameraDeviceInfoData.self, from: data)
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
        battery: Int?? = nil,
        bayerOffsetX: Int?? = nil,
        bayerOffsetY: Int?? = nil,
        binX: Int?? = nil,
        binY: Int?? = nil,
        binningModes: [BinningMode]?? = nil,
        bitDepth: Int?? = nil,
        cameraState: CameraState?? = nil,
        canGetGain: Bool?? = nil,
        canSetGain: Bool? = nil,
        canSetOffset: Bool? = nil,
        canSetTemperature: Bool? = nil,
        canSetUSBLimit: Bool?? = nil,
        canShowLiveView: Bool?? = nil,
        canSubSample: Bool?? = nil,
        connected: Bool? = nil,
        coolerOn: Bool?? = nil,
        coolerPower: Double?? = nil,
        defaultGain: Int?? = nil,
        defaultOffset: Int?? = nil,
        dewHeaterOn: Bool?? = nil,
        electronsPerAdu: Double?? = nil,
        exposureEndTime: Date?? = nil,
        exposureMax: Double?? = nil,
        exposureMin: Double?? = nil,
        gain: Int?? = nil,
        gainMax: Int?? = nil,
        gainMin: Int?? = nil,
        gains: [Int]?? = nil,
        hasBattery: Bool?? = nil,
        hasDewHeater: Bool?? = nil,
        hasShutter: Bool? = nil,
        isExposing: Bool?? = nil,
        isSubSampleEnabled: Bool?? = nil,
        lastDownloadTime: Double?? = nil,
        liveViewEnabled: Bool?? = nil,
        normalReadoutMode: Int?? = nil,
        offset: Int?? = nil,
        offsetMax: Int?? = nil,
        offsetMin: Int?? = nil,
        pixelSize: Double?? = nil,
        readoutMode: Int?? = nil,
        readoutModes: [String]?? = nil,
        sensorType: SensorType?? = nil,
        snapReadoutMode: Int?? = nil,
        subSampleHeight: Int?? = nil,
        subSampleWidth: Int?? = nil,
        subSampleX: Int?? = nil,
        subSampleY: Int?? = nil,
        temeratureSetPoint: Double?? = nil,
        temperature: Double?? = nil,
        usbLimit: Int?? = nil,
        usbLimitMax: Int?? = nil,
        usbLimitMin: Int?? = nil,
        xSize: Int?? = nil,
        ySize: Int?? = nil
    ) -> CameraDeviceInfoData {
        return CameraDeviceInfoData(
            battery: battery ?? self.battery,
            bayerOffsetX: bayerOffsetX ?? self.bayerOffsetX,
            bayerOffsetY: bayerOffsetY ?? self.bayerOffsetY,
            binX: binX ?? self.binX,
            binY: binY ?? self.binY,
            binningModes: binningModes ?? self.binningModes,
            bitDepth: bitDepth ?? self.bitDepth,
            cameraState: cameraState ?? self.cameraState,
            canGetGain: canGetGain ?? self.canGetGain,
            canSetGain: canSetGain ?? self.canSetGain,
            canSetOffset: canSetOffset ?? self.canSetOffset,
            canSetTemperature: canSetTemperature ?? self.canSetTemperature,
            canSetUSBLimit: canSetUSBLimit ?? self.canSetUSBLimit,
            canShowLiveView: canShowLiveView ?? self.canShowLiveView,
            canSubSample: canSubSample ?? self.canSubSample,
            connected: connected ?? self.connected,
            coolerOn: coolerOn ?? self.coolerOn,
            coolerPower: coolerPower ?? self.coolerPower,
            defaultGain: defaultGain ?? self.defaultGain,
            defaultOffset: defaultOffset ?? self.defaultOffset,
            dewHeaterOn: dewHeaterOn ?? self.dewHeaterOn,
            electronsPerAdu: electronsPerAdu ?? self.electronsPerAdu,
            exposureEndTime: exposureEndTime ?? self.exposureEndTime,
            exposureMax: exposureMax ?? self.exposureMax,
            exposureMin: exposureMin ?? self.exposureMin,
            gain: gain ?? self.gain,
            gainMax: gainMax ?? self.gainMax,
            gainMin: gainMin ?? self.gainMin,
            gains: gains ?? self.gains,
            hasBattery: hasBattery ?? self.hasBattery,
            hasDewHeater: hasDewHeater ?? self.hasDewHeater,
            hasShutter: hasShutter ?? self.hasShutter,
            isExposing: isExposing ?? self.isExposing,
            isSubSampleEnabled: isSubSampleEnabled ?? self.isSubSampleEnabled,
            lastDownloadTime: lastDownloadTime ?? self.lastDownloadTime,
            liveViewEnabled: liveViewEnabled ?? self.liveViewEnabled,
            normalReadoutMode: normalReadoutMode ?? self.normalReadoutMode,
            offset: offset ?? self.offset,
            offsetMax: offsetMax ?? self.offsetMax,
            offsetMin: offsetMin ?? self.offsetMin,
            pixelSize: pixelSize ?? self.pixelSize,
            readoutMode: readoutMode ?? self.readoutMode,
            readoutModes: readoutModes ?? self.readoutModes,
            sensorType: sensorType ?? self.sensorType,
            snapReadoutMode: snapReadoutMode ?? self.snapReadoutMode,
            subSampleHeight: subSampleHeight ?? self.subSampleHeight,
            subSampleWidth: subSampleWidth ?? self.subSampleWidth,
            subSampleX: subSampleX ?? self.subSampleX,
            subSampleY: subSampleY ?? self.subSampleY,
            temeratureSetPoint: temeratureSetPoint ?? self.temeratureSetPoint,
            temperature: temperature ?? self.temperature,
            usbLimit: usbLimit ?? self.usbLimit,
            usbLimitMax: usbLimitMax ?? self.usbLimitMax,
            usbLimitMin: usbLimitMin ?? self.usbLimitMin,
            xSize: xSize ?? self.xSize,
            ySize: ySize ?? self.ySize
        )
    }

    func jsonData() throws -> Data {
        return try newJSONEncoder().encode(self)
    }

    func jsonString(encoding: String.Encoding = .utf8) throws -> String? {
        return String(data: try self.jsonData(), encoding: encoding)
    }
}

// MARK: - BinningMode
struct BinningMode: Codable {
    let x, y: Int
}

// MARK: BinningMode convenience initializers and mutators

extension BinningMode {
    init(data: Data) throws {
        self = try newJSONDecoder().decode(BinningMode.self, from: data)
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
        x: Int? = nil,
        y: Int? = nil
    ) -> BinningMode {
        return BinningMode(
            x: x ?? self.x,
            y: y ?? self.y
        )
    }

    func jsonData() throws -> Data {
        return try newJSONEncoder().encode(self)
    }

    func jsonString(encoding: String.Encoding = .utf8) throws -> String? {
        return String(data: try self.jsonData(), encoding: encoding)
    }
}

enum CameraState: String, Codable {
    case download = "Download"
    case error = "Error"
    case exposing = "Exposing"
    case idle = "Idle"
    case loadingFile = "LoadingFile"
    case none = "None"
    case reading = "Reading"
    case waiting = "Waiting"
}

enum SensorType: String, Codable {
    case bggr = "BGGR"
    case bgrg = "BGRG"
    case cmyg = "CMYG"
    case cmyg2 = "CMYG2"
    case color = "Color"
    case gbgr = "GBGR"
    case gbrg = "GBRG"
    case grbg = "GRBG"
    case grgb = "GRGB"
    case lrgb = "LRGB"
    case monochrome = "Monochrome"
    case rgbg = "RGBG"
    case rggb = "RGGB"
}

/// Data on the camera encounters and error
// MARK: - CameraErrorData
struct CameraErrorData: Codable {
    let error: Error
}

// MARK: CameraErrorData convenience initializers and mutators

extension CameraErrorData {
    init(data: Data) throws {
        self = try newJSONDecoder().decode(CameraErrorData.self, from: data)
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
        error: Error? = nil
    ) -> CameraErrorData {
        return CameraErrorData(
            error: error ?? self.error
        )
    }

    func jsonData() throws -> Data {
        return try newJSONEncoder().encode(self)
    }

    func jsonString(encoding: String.Encoding = .utf8) throws -> String? {
        return String(data: try self.jsonData(), encoding: encoding)
    }
}

enum Error: String, Codable {
    case downloadTimeout = "DownloadTimeout"
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

/// Event that fires before or after a meridian flip
// MARK: - DomeShutterData
struct DomeShutterData: Codable {
    let position: Position
}

// MARK: DomeShutterData convenience initializers and mutators

extension DomeShutterData {
    init(data: Data) throws {
        self = try newJSONDecoder().decode(DomeShutterData.self, from: data)
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
        position: Position? = nil
    ) -> DomeShutterData {
        return DomeShutterData(
            position: position ?? self.position
        )
    }

    func jsonData() throws -> Data {
        return try newJSONEncoder().encode(self)
    }

    func jsonString(encoding: String.Encoding = .utf8) throws -> String? {
        return String(data: try self.jsonData(), encoding: encoding)
    }
}

enum Position: String, Codable {
    case closed = "closed"
    case homed = "homed"
    case opened = "opened"
    case parked = "parked"
    case slewed = "slewed"
    case synced = "synced"
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

/// Filter Wheel selected filter change event data
// MARK: - FilterWheelChangeData
struct FilterWheelChangeData: Codable {
    let fromFilter, toFilter: FilterInfo

    enum CodingKeys: String, CodingKey {
        case fromFilter = "from_filter"
        case toFilter = "to_filter"
    }
}

// MARK: FilterWheelChangeData convenience initializers and mutators

extension FilterWheelChangeData {
    init(data: Data) throws {
        self = try newJSONDecoder().decode(FilterWheelChangeData.self, from: data)
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
        fromFilter: FilterInfo? = nil,
        toFilter: FilterInfo? = nil
    ) -> FilterWheelChangeData {
        return FilterWheelChangeData(
            fromFilter: fromFilter ?? self.fromFilter,
            toFilter: toFilter ?? self.toFilter
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

/// Event data for when the led toggles
// MARK: - FlatPanelLEDData
struct FlatPanelLEDData: Codable {
    let lightOn: Bool

    enum CodingKeys: String, CodingKey {
        case lightOn = "light_on"
    }
}

// MARK: FlatPanelLEDData convenience initializers and mutators

extension FlatPanelLEDData {
    init(data: Data) throws {
        self = try newJSONDecoder().decode(FlatPanelLEDData.self, from: data)
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
        lightOn: Bool? = nil
    ) -> FlatPanelLEDData {
        return FlatPanelLEDData(
            lightOn: lightOn ?? self.lightOn
        )
    }

    func jsonData() throws -> Data {
        return try newJSONEncoder().encode(self)
    }

    func jsonString(encoding: String.Encoding = .utf8) throws -> String? {
        return String(data: try self.jsonData(), encoding: encoding)
    }
}

/// Event data for when the panel opens or closes
// MARK: - FlatPanelStateData
struct FlatPanelStateData: Codable {
    let state: State
}

// MARK: FlatPanelStateData convenience initializers and mutators

extension FlatPanelStateData {
    init(data: Data) throws {
        self = try newJSONDecoder().decode(FlatPanelStateData.self, from: data)
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
        state: State? = nil
    ) -> FlatPanelStateData {
        return FlatPanelStateData(
            state: state ?? self.state
        )
    }

    func jsonData() throws -> Data {
        return try newJSONEncoder().encode(self)
    }

    func jsonString(encoding: String.Encoding = .utf8) throws -> String? {
        return String(data: try self.jsonData(), encoding: encoding)
    }
}

enum State: String, Codable {
    case closed = "Closed"
    case opened = "Opened"
}

/// Event data for when the brightness changes
// MARK: - FlatPanelValueData
struct FlatPanelValueData: Codable {
    let from, to: Double
}

// MARK: FlatPanelValueData convenience initializers and mutators

extension FlatPanelValueData {
    init(data: Data) throws {
        self = try newJSONDecoder().decode(FlatPanelValueData.self, from: data)
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
        from: Double? = nil,
        to: Double? = nil
    ) -> FlatPanelValueData {
        return FlatPanelValueData(
            from: from ?? self.from,
            to: to ?? self.to
        )
    }

    func jsonData() throws -> Data {
        return try newJSONEncoder().encode(self)
    }

    func jsonString(encoding: String.Encoding = .utf8) throws -> String? {
        return String(data: try self.jsonData(), encoding: encoding)
    }
}

/// Focuser device state
// MARK: - FocuserDeviceInfoData
struct FocuserDeviceInfoData: Codable {
    let connected, isMoving, isSettling: Bool
    let position: Int?
    let stepSize: Double?
    let tempComp, tempCompAvailable: Bool?
    let temperature: Double?

    enum CodingKeys: String, CodingKey {
        case connected
        case isMoving = "is_moving"
        case isSettling = "is_settling"
        case position
        case stepSize = "step_size"
        case tempComp = "temp_comp"
        case tempCompAvailable = "temp_comp_available"
        case temperature
    }
}

// MARK: FocuserDeviceInfoData convenience initializers and mutators

extension FocuserDeviceInfoData {
    init(data: Data) throws {
        self = try newJSONDecoder().decode(FocuserDeviceInfoData.self, from: data)
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
        isSettling: Bool? = nil,
        position: Int?? = nil,
        stepSize: Double?? = nil,
        tempComp: Bool?? = nil,
        tempCompAvailable: Bool?? = nil,
        temperature: Double?? = nil
    ) -> FocuserDeviceInfoData {
        return FocuserDeviceInfoData(
            connected: connected ?? self.connected,
            isMoving: isMoving ?? self.isMoving,
            isSettling: isSettling ?? self.isSettling,
            position: position ?? self.position,
            stepSize: stepSize ?? self.stepSize,
            tempComp: tempComp ?? self.tempComp,
            tempCompAvailable: tempCompAvailable ?? self.tempCompAvailable,
            temperature: temperature ?? self.temperature
        )
    }

    func jsonData() throws -> Data {
        return try newJSONEncoder().encode(self)
    }

    func jsonString(encoding: String.Encoding = .utf8) throws -> String? {
        return String(data: try self.jsonData(), encoding: encoding)
    }
}

/// Event data when the user or auto focus runs
// MARK: - FocuserChangeData
struct FocuserChangeData: Codable {
    let filter: String?
    let focusEvent: FocusEvent
    let postion, temperature: Double?
    let timestamp: Date?

    enum CodingKeys: String, CodingKey {
        case filter
        case focusEvent = "focus_event"
        case postion, temperature, timestamp
    }
}

// MARK: FocuserChangeData convenience initializers and mutators

extension FocuserChangeData {
    init(data: Data) throws {
        self = try newJSONDecoder().decode(FocuserChangeData.self, from: data)
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
        filter: String?? = nil,
        focusEvent: FocusEvent? = nil,
        postion: Double?? = nil,
        temperature: Double?? = nil,
        timestamp: Date?? = nil
    ) -> FocuserChangeData {
        return FocuserChangeData(
            filter: filter ?? self.filter,
            focusEvent: focusEvent ?? self.focusEvent,
            postion: postion ?? self.postion,
            temperature: temperature ?? self.temperature,
            timestamp: timestamp ?? self.timestamp
        )
    }

    func jsonData() throws -> Data {
        return try newJSONEncoder().encode(self)
    }

    func jsonString(encoding: String.Encoding = .utf8) throws -> String? {
        return String(data: try self.jsonData(), encoding: encoding)
    }
}

enum FocusEvent: String, Codable {
    case autoFocusEnd = "AutoFocusEnd"
    case autoFocusStart = "AutoFocusStart"
    case manualFocus = "ManualFocus"
}

/// Periodic Guider event data
// MARK: - GuiderDeviceInfoData
struct GuiderDeviceInfoData: Codable {
    let canClearCalibration, canGetLockPostion, canSetShiftRate, connected: Bool
    let pixelScale: Double?
    let rmsError: RMSError?

    enum CodingKeys: String, CodingKey {
        case canClearCalibration = "can_clear_calibration"
        case canGetLockPostion = "can_get_lock_postion"
        case canSetShiftRate = "can_set_shift_rate"
        case connected
        case pixelScale = "pixel_scale"
        case rmsError = "rms_error"
    }
}

// MARK: GuiderDeviceInfoData convenience initializers and mutators

extension GuiderDeviceInfoData {
    init(data: Data) throws {
        self = try newJSONDecoder().decode(GuiderDeviceInfoData.self, from: data)
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
        canClearCalibration: Bool? = nil,
        canGetLockPostion: Bool? = nil,
        canSetShiftRate: Bool? = nil,
        connected: Bool? = nil,
        pixelScale: Double?? = nil,
        rmsError: RMSError?? = nil
    ) -> GuiderDeviceInfoData {
        return GuiderDeviceInfoData(
            canClearCalibration: canClearCalibration ?? self.canClearCalibration,
            canGetLockPostion: canGetLockPostion ?? self.canGetLockPostion,
            canSetShiftRate: canSetShiftRate ?? self.canSetShiftRate,
            connected: connected ?? self.connected,
            pixelScale: pixelScale ?? self.pixelScale,
            rmsError: rmsError ?? self.rmsError
        )
    }

    func jsonData() throws -> Data {
        return try newJSONEncoder().encode(self)
    }

    func jsonString(encoding: String.Encoding = .utf8) throws -> String? {
        return String(data: try self.jsonData(), encoding: encoding)
    }
}

// MARK: - RMSError
struct RMSError: Codable {
    let dec, peakDEC, peakRa, ra: RMSUnit
    let total: RMSUnit

    enum CodingKeys: String, CodingKey {
        case dec
        case peakDEC = "peak_dec"
        case peakRa = "peak_ra"
        case ra, total
    }
}

// MARK: RMSError convenience initializers and mutators

extension RMSError {
    init(data: Data) throws {
        self = try newJSONDecoder().decode(RMSError.self, from: data)
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
        dec: RMSUnit? = nil,
        peakDEC: RMSUnit? = nil,
        peakRa: RMSUnit? = nil,
        ra: RMSUnit? = nil,
        total: RMSUnit? = nil
    ) -> RMSError {
        return RMSError(
            dec: dec ?? self.dec,
            peakDEC: peakDEC ?? self.peakDEC,
            peakRa: peakRa ?? self.peakRa,
            ra: ra ?? self.ra,
            total: total ?? self.total
        )
    }

    func jsonData() throws -> Data {
        return try newJSONEncoder().encode(self)
    }

    func jsonString(encoding: String.Encoding = .utf8) throws -> String? {
        return String(data: try self.jsonData(), encoding: encoding)
    }
}

// MARK: - RMSUnit
struct RMSUnit: Codable {
    let arcSeconds, pixel: Double

    enum CodingKeys: String, CodingKey {
        case arcSeconds = "arc_seconds"
        case pixel
    }
}

// MARK: RMSUnit convenience initializers and mutators

extension RMSUnit {
    init(data: Data) throws {
        self = try newJSONDecoder().decode(RMSUnit.self, from: data)
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
        arcSeconds: Double? = nil,
        pixel: Double? = nil
    ) -> RMSUnit {
        return RMSUnit(
            arcSeconds: arcSeconds ?? self.arcSeconds,
            pixel: pixel ?? self.pixel
        )
    }

    func jsonData() throws -> Data {
        return try newJSONEncoder().encode(self)
    }

    func jsonString(encoding: String.Encoding = .utf8) throws -> String? {
        return String(data: try self.jsonData(), encoding: encoding)
    }
}

/// Event after the guider has dithered
// MARK: - GuiderDitherData
struct GuiderDitherData: Codable {
    let stage: GuiderDitherDataStage
}

// MARK: GuiderDitherData convenience initializers and mutators

extension GuiderDitherData {
    init(data: Data) throws {
        self = try newJSONDecoder().decode(GuiderDitherData.self, from: data)
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
        stage: GuiderDitherDataStage? = nil
    ) -> GuiderDitherData {
        return GuiderDitherData(
            stage: stage ?? self.stage
        )
    }

    func jsonData() throws -> Data {
        return try newJSONEncoder().encode(self)
    }

    func jsonString(encoding: String.Encoding = .utf8) throws -> String? {
        return String(data: try self.jsonData(), encoding: encoding)
    }
}

enum GuiderDitherDataStage: String, Codable {
    case after = "After"
}

/// Event after the guider has started or stopped
// MARK: - GuiderStartData
struct GuiderStartData: Codable {
    let stage: GuiderStartDataStage
}

// MARK: GuiderStartData convenience initializers and mutators

extension GuiderStartData {
    init(data: Data) throws {
        self = try newJSONDecoder().decode(GuiderStartData.self, from: data)
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
        stage: GuiderStartDataStage? = nil
    ) -> GuiderStartData {
        return GuiderStartData(
            stage: stage ?? self.stage
        )
    }

    func jsonData() throws -> Data {
        return try newJSONEncoder().encode(self)
    }

    func jsonString(encoding: String.Encoding = .utf8) throws -> String? {
        return String(data: try self.jsonData(), encoding: encoding)
    }
}

enum GuiderStartDataStage: String, Codable {
    case started = "Started"
    case stopped = "Stopped"
}

/// Event after the guider has started or stopped
// MARK: - GuiderStepData
struct GuiderStepData: Codable {
    let decDistianceRaw, decDuration, frame, raDistanceRaw: Double
    let raDuration, time: Double

    enum CodingKeys: String, CodingKey {
        case decDistianceRaw = "dec_distiance_raw"
        case decDuration = "dec_duration"
        case frame
        case raDistanceRaw = "ra_distance_raw"
        case raDuration = "ra_duration"
        case time
    }
}

// MARK: GuiderStepData convenience initializers and mutators

extension GuiderStepData {
    init(data: Data) throws {
        self = try newJSONDecoder().decode(GuiderStepData.self, from: data)
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
        decDistianceRaw: Double? = nil,
        decDuration: Double? = nil,
        frame: Double? = nil,
        raDistanceRaw: Double? = nil,
        raDuration: Double? = nil,
        time: Double? = nil
    ) -> GuiderStepData {
        return GuiderStepData(
            decDistianceRaw: decDistianceRaw ?? self.decDistianceRaw,
            decDuration: decDuration ?? self.decDuration,
            frame: frame ?? self.frame,
            raDistanceRaw: raDistanceRaw ?? self.raDistanceRaw,
            raDuration: raDuration ?? self.raDuration,
            time: time ?? self.time
        )
    }

    func jsonData() throws -> Data {
        return try newJSONEncoder().encode(self)
    }

    func jsonString(encoding: String.Encoding = .utf8) throws -> String? {
        return String(data: try self.jsonData(), encoding: encoding)
    }
}

/// Periodic Mount event data
// MARK: - MountDeviceInfoData
struct MountDeviceInfoData: Codable {
    let alignmentMode: AlignmentMode?
    let altitude: Double?
    let atHome, atPark: Bool?
    let azimuth: Double?
    let canFindHome, canMovePrimaryAxis, canMoveSecondaryAxis, canPark: Bool?
    let canPulseGuide, canSetDeclinationRate, canSetPark, canSetPierSide: Bool?
    let canSetRightAscensionRate, canSetTracking, canSlew, canSlewAltAz: Bool?
    let connected: Bool
    let declination: Double?
    let equatorialSystem: Epoch?
    let guideRateDECArcSECPerSEC, guideRateRaArcSECPerSEC: Double?
    let hasUnknownEpoch, isPulseGuiding: Bool?
    let primaryAxisRates: [[Double]]?
    let rightAscension: Double?
    let secondaryAxisRates: [[Double]]?
    let sideOfPier: PierSide?
    let siderealTime, siteElevation, siteLatitude, siteLongitude: Double?
    let slewing: Bool?
    let targetCoordinates: Coordinates?
    let targetSideOfPier: PierSide?
    let timeToMeridianFlip: Double?
    let trackingEnabled: Bool?
    let trackingModes: [TrackingMode]?
    let trackingRate: TrackingRate?
    let utcDate: Date?

    enum CodingKeys: String, CodingKey {
        case alignmentMode = "alignment_mode"
        case altitude
        case atHome = "at_home"
        case atPark = "at_park"
        case azimuth
        case canFindHome = "can_find_home"
        case canMovePrimaryAxis = "can_move_primary_axis"
        case canMoveSecondaryAxis = "can_move_secondary_axis"
        case canPark = "can_park"
        case canPulseGuide = "can_pulse_guide"
        case canSetDeclinationRate = "can_set_declination_rate"
        case canSetPark = "can_set_park"
        case canSetPierSide = "can_set_pier_side"
        case canSetRightAscensionRate = "can_set_right_ascension_rate"
        case canSetTracking = "can_set_tracking"
        case canSlew = "can_slew"
        case canSlewAltAz = "can_slew_alt_az"
        case connected, declination
        case equatorialSystem = "equatorial_system"
        case guideRateDECArcSECPerSEC = "guide_rate_dec_arc_sec_per_sec"
        case guideRateRaArcSECPerSEC = "guide_rate_ra_arc_sec_per_sec"
        case hasUnknownEpoch = "has_unknown_epoch"
        case isPulseGuiding = "is_pulse_guiding"
        case primaryAxisRates = "primary_axis_rates"
        case rightAscension = "right_ascension"
        case secondaryAxisRates = "secondary_axis_rates"
        case sideOfPier = "side_of_pier"
        case siderealTime = "sidereal_time"
        case siteElevation = "site_elevation"
        case siteLatitude = "site_latitude"
        case siteLongitude = "site_longitude"
        case slewing
        case targetCoordinates = "target_coordinates"
        case targetSideOfPier = "target_side_of_pier"
        case timeToMeridianFlip = "time_to_meridian_flip"
        case trackingEnabled = "tracking_enabled"
        case trackingModes = "tracking_modes"
        case trackingRate = "tracking_rate"
        case utcDate = "utc_date"
    }
}

// MARK: MountDeviceInfoData convenience initializers and mutators

extension MountDeviceInfoData {
    init(data: Data) throws {
        self = try newJSONDecoder().decode(MountDeviceInfoData.self, from: data)
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
        alignmentMode: AlignmentMode?? = nil,
        altitude: Double?? = nil,
        atHome: Bool?? = nil,
        atPark: Bool?? = nil,
        azimuth: Double?? = nil,
        canFindHome: Bool?? = nil,
        canMovePrimaryAxis: Bool?? = nil,
        canMoveSecondaryAxis: Bool?? = nil,
        canPark: Bool?? = nil,
        canPulseGuide: Bool?? = nil,
        canSetDeclinationRate: Bool?? = nil,
        canSetPark: Bool?? = nil,
        canSetPierSide: Bool?? = nil,
        canSetRightAscensionRate: Bool?? = nil,
        canSetTracking: Bool?? = nil,
        canSlew: Bool?? = nil,
        canSlewAltAz: Bool?? = nil,
        connected: Bool? = nil,
        declination: Double?? = nil,
        equatorialSystem: Epoch?? = nil,
        guideRateDECArcSECPerSEC: Double?? = nil,
        guideRateRaArcSECPerSEC: Double?? = nil,
        hasUnknownEpoch: Bool?? = nil,
        isPulseGuiding: Bool?? = nil,
        primaryAxisRates: [[Double]]?? = nil,
        rightAscension: Double?? = nil,
        secondaryAxisRates: [[Double]]?? = nil,
        sideOfPier: PierSide?? = nil,
        siderealTime: Double?? = nil,
        siteElevation: Double?? = nil,
        siteLatitude: Double?? = nil,
        siteLongitude: Double?? = nil,
        slewing: Bool?? = nil,
        targetCoordinates: Coordinates?? = nil,
        targetSideOfPier: PierSide?? = nil,
        timeToMeridianFlip: Double?? = nil,
        trackingEnabled: Bool?? = nil,
        trackingModes: [TrackingMode]?? = nil,
        trackingRate: TrackingRate?? = nil,
        utcDate: Date?? = nil
    ) -> MountDeviceInfoData {
        return MountDeviceInfoData(
            alignmentMode: alignmentMode ?? self.alignmentMode,
            altitude: altitude ?? self.altitude,
            atHome: atHome ?? self.atHome,
            atPark: atPark ?? self.atPark,
            azimuth: azimuth ?? self.azimuth,
            canFindHome: canFindHome ?? self.canFindHome,
            canMovePrimaryAxis: canMovePrimaryAxis ?? self.canMovePrimaryAxis,
            canMoveSecondaryAxis: canMoveSecondaryAxis ?? self.canMoveSecondaryAxis,
            canPark: canPark ?? self.canPark,
            canPulseGuide: canPulseGuide ?? self.canPulseGuide,
            canSetDeclinationRate: canSetDeclinationRate ?? self.canSetDeclinationRate,
            canSetPark: canSetPark ?? self.canSetPark,
            canSetPierSide: canSetPierSide ?? self.canSetPierSide,
            canSetRightAscensionRate: canSetRightAscensionRate ?? self.canSetRightAscensionRate,
            canSetTracking: canSetTracking ?? self.canSetTracking,
            canSlew: canSlew ?? self.canSlew,
            canSlewAltAz: canSlewAltAz ?? self.canSlewAltAz,
            connected: connected ?? self.connected,
            declination: declination ?? self.declination,
            equatorialSystem: equatorialSystem ?? self.equatorialSystem,
            guideRateDECArcSECPerSEC: guideRateDECArcSECPerSEC ?? self.guideRateDECArcSECPerSEC,
            guideRateRaArcSECPerSEC: guideRateRaArcSECPerSEC ?? self.guideRateRaArcSECPerSEC,
            hasUnknownEpoch: hasUnknownEpoch ?? self.hasUnknownEpoch,
            isPulseGuiding: isPulseGuiding ?? self.isPulseGuiding,
            primaryAxisRates: primaryAxisRates ?? self.primaryAxisRates,
            rightAscension: rightAscension ?? self.rightAscension,
            secondaryAxisRates: secondaryAxisRates ?? self.secondaryAxisRates,
            sideOfPier: sideOfPier ?? self.sideOfPier,
            siderealTime: siderealTime ?? self.siderealTime,
            siteElevation: siteElevation ?? self.siteElevation,
            siteLatitude: siteLatitude ?? self.siteLatitude,
            siteLongitude: siteLongitude ?? self.siteLongitude,
            slewing: slewing ?? self.slewing,
            targetCoordinates: targetCoordinates ?? self.targetCoordinates,
            targetSideOfPier: targetSideOfPier ?? self.targetSideOfPier,
            timeToMeridianFlip: timeToMeridianFlip ?? self.timeToMeridianFlip,
            trackingEnabled: trackingEnabled ?? self.trackingEnabled,
            trackingModes: trackingModes ?? self.trackingModes,
            trackingRate: trackingRate ?? self.trackingRate,
            utcDate: utcDate ?? self.utcDate
        )
    }

    func jsonData() throws -> Data {
        return try newJSONEncoder().encode(self)
    }

    func jsonString(encoding: String.Encoding = .utf8) throws -> String? {
        return String(data: try self.jsonData(), encoding: encoding)
    }
}

enum AlignmentMode: String, Codable {
    case altAz = "AltAz"
    case germanPolar = "GermanPolar"
    case polar = "Polar"
}

enum Epoch: String, Codable {
    case b1950 = "B1950"
    case j2000 = "J2000"
    case j2050 = "J2050"
    case jnow = "JNOW"
}

enum PierSide: String, Codable {
    case east = "East"
    case unknown = "Unknown"
    case west = "West"
}

// MARK: - Coordinates
struct Coordinates: Codable {
    let decDegrees: Double?
    let epoch: Epoch?
    let raDegrees: Double?

    enum CodingKeys: String, CodingKey {
        case decDegrees = "dec_degrees"
        case epoch
        case raDegrees = "ra_degrees"
    }
}

// MARK: Coordinates convenience initializers and mutators

extension Coordinates {
    init(data: Data) throws {
        self = try newJSONDecoder().decode(Coordinates.self, from: data)
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
        decDegrees: Double?? = nil,
        epoch: Epoch?? = nil,
        raDegrees: Double?? = nil
    ) -> Coordinates {
        return Coordinates(
            decDegrees: decDegrees ?? self.decDegrees,
            epoch: epoch ?? self.epoch,
            raDegrees: raDegrees ?? self.raDegrees
        )
    }

    func jsonData() throws -> Data {
        return try newJSONEncoder().encode(self)
    }

    func jsonString(encoding: String.Encoding = .utf8) throws -> String? {
        return String(data: try self.jsonData(), encoding: encoding)
    }
}

enum TrackingMode: String, Codable {
    case custom = "Custom"
    case king = "King"
    case lunar = "Lunar"
    case sidereal = "Sidereal"
    case solar = "Solar"
    case stopped = "Stopped"
}

// MARK: - TrackingRate
struct TrackingRate: Codable {
    let customDECRate, customRaRate: Double?
    let trackingMode: TrackingMode?

    enum CodingKeys: String, CodingKey {
        case customDECRate = "custom_dec_rate"
        case customRaRate = "custom_ra_rate"
        case trackingMode = "tracking_mode"
    }
}

// MARK: TrackingRate convenience initializers and mutators

extension TrackingRate {
    init(data: Data) throws {
        self = try newJSONDecoder().decode(TrackingRate.self, from: data)
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
        customDECRate: Double?? = nil,
        customRaRate: Double?? = nil,
        trackingMode: TrackingMode?? = nil
    ) -> TrackingRate {
        return TrackingRate(
            customDECRate: customDECRate ?? self.customDECRate,
            customRaRate: customRaRate ?? self.customRaRate,
            trackingMode: trackingMode ?? self.trackingMode
        )
    }

    func jsonData() throws -> Data {
        return try newJSONEncoder().encode(self)
    }

    func jsonString(encoding: String.Encoding = .utf8) throws -> String? {
        return String(data: try self.jsonData(), encoding: encoding)
    }
}

/// Event that fires before or after a meridian flip
// MARK: - MountFlipData
struct MountFlipData: Codable {
    let flipEvent: FlipEvent
    let success: Bool?
    let targetCoordinates: Coordinates?

    enum CodingKeys: String, CodingKey {
        case flipEvent = "flip_event"
        case success
        case targetCoordinates = "target_coordinates"
    }
}

// MARK: MountFlipData convenience initializers and mutators

extension MountFlipData {
    init(data: Data) throws {
        self = try newJSONDecoder().decode(MountFlipData.self, from: data)
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
        flipEvent: FlipEvent? = nil,
        success: Bool?? = nil,
        targetCoordinates: Coordinates?? = nil
    ) -> MountFlipData {
        return MountFlipData(
            flipEvent: flipEvent ?? self.flipEvent,
            success: success ?? self.success,
            targetCoordinates: targetCoordinates ?? self.targetCoordinates
        )
    }

    func jsonData() throws -> Data {
        return try newJSONEncoder().encode(self)
    }

    func jsonString(encoding: String.Encoding = .utf8) throws -> String? {
        return String(data: try self.jsonData(), encoding: encoding)
    }
}

enum FlipEvent: String, Codable {
    case after = "after"
    case before = "before"
}

/// Event after the mount has moved
// MARK: - MountMovedData
struct MountMovedData: Codable {
    let fromCoordinates: Coordinates?
    let moveType: MoveType
    let toCoordinates: Coordinates?

    enum CodingKeys: String, CodingKey {
        case fromCoordinates = "from_coordinates"
        case moveType = "move_type"
        case toCoordinates = "to_coordinates"
    }
}

// MARK: MountMovedData convenience initializers and mutators

extension MountMovedData {
    init(data: Data) throws {
        self = try newJSONDecoder().decode(MountMovedData.self, from: data)
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
        fromCoordinates: Coordinates?? = nil,
        moveType: MoveType? = nil,
        toCoordinates: Coordinates?? = nil
    ) -> MountMovedData {
        return MountMovedData(
            fromCoordinates: fromCoordinates ?? self.fromCoordinates,
            moveType: moveType ?? self.moveType,
            toCoordinates: toCoordinates ?? self.toCoordinates
        )
    }

    func jsonData() throws -> Data {
        return try newJSONEncoder().encode(self)
    }

    func jsonString(encoding: String.Encoding = .utf8) throws -> String? {
        return String(data: try self.jsonData(), encoding: encoding)
    }
}

enum MoveType: String, Codable {
    case homed = "homed"
    case parked = "parked"
    case slewed = "slewed"
    case unparked = "unparked"
}

/// Periodic Rotator event data
// MARK: - RotatorDeviceInfoData
struct RotatorDeviceInfoData: Codable {
    let canReverse, connected, isMoving: Bool
    let mechanicalPosition, position: Double?
    let reverse: Bool
    let stepSize: Double?
    let synced: Bool

    enum CodingKeys: String, CodingKey {
        case canReverse = "can_reverse"
        case connected
        case isMoving = "is_moving"
        case mechanicalPosition = "mechanical_position"
        case position, reverse
        case stepSize = "step_size"
        case synced
    }
}

// MARK: RotatorDeviceInfoData convenience initializers and mutators

extension RotatorDeviceInfoData {
    init(data: Data) throws {
        self = try newJSONDecoder().decode(RotatorDeviceInfoData.self, from: data)
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
        canReverse: Bool? = nil,
        connected: Bool? = nil,
        isMoving: Bool? = nil,
        mechanicalPosition: Double?? = nil,
        position: Double?? = nil,
        reverse: Bool? = nil,
        stepSize: Double?? = nil,
        synced: Bool? = nil
    ) -> RotatorDeviceInfoData {
        return RotatorDeviceInfoData(
            canReverse: canReverse ?? self.canReverse,
            connected: connected ?? self.connected,
            isMoving: isMoving ?? self.isMoving,
            mechanicalPosition: mechanicalPosition ?? self.mechanicalPosition,
            position: position ?? self.position,
            reverse: reverse ?? self.reverse,
            stepSize: stepSize ?? self.stepSize,
            synced: synced ?? self.synced
        )
    }

    func jsonData() throws -> Data {
        return try newJSONEncoder().encode(self)
    }

    func jsonString(encoding: String.Encoding = .utf8) throws -> String? {
        return String(data: try self.jsonData(), encoding: encoding)
    }
}

/// Event after the rotator has moved
// MARK: - RotatorMovedData
struct RotatorMovedData: Codable {
    let event: Event
    let from, to: Double
}

// MARK: RotatorMovedData convenience initializers and mutators

extension RotatorMovedData {
    init(data: Data) throws {
        self = try newJSONDecoder().decode(RotatorMovedData.self, from: data)
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
        event: Event? = nil,
        from: Double? = nil,
        to: Double? = nil
    ) -> RotatorMovedData {
        return RotatorMovedData(
            event: event ?? self.event,
            from: from ?? self.from,
            to: to ?? self.to
        )
    }

    func jsonData() throws -> Data {
        return try newJSONEncoder().encode(self)
    }

    func jsonString(encoding: String.Encoding = .utf8) throws -> String? {
        return String(data: try self.jsonData(), encoding: encoding)
    }
}

enum Event: String, Codable {
    case moved = "moved"
    case movedMechanical = "moved_mechanical"
    case synced = "synced"
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

/// Periodic Switch event data
// MARK: - SwitchDeviceInfoData
struct SwitchDeviceInfoData: Codable {
    let connected: Bool
    let readableSwitches: [ReadableSwitch]?
    let writeableSwitches: [WriteableSwitch]?

    enum CodingKeys: String, CodingKey {
        case connected
        case readableSwitches = "readable_switches"
        case writeableSwitches = "writeable_switches"
    }
}

// MARK: SwitchDeviceInfoData convenience initializers and mutators

extension SwitchDeviceInfoData {
    init(data: Data) throws {
        self = try newJSONDecoder().decode(SwitchDeviceInfoData.self, from: data)
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
        readableSwitches: [ReadableSwitch]?? = nil,
        writeableSwitches: [WriteableSwitch]?? = nil
    ) -> SwitchDeviceInfoData {
        return SwitchDeviceInfoData(
            connected: connected ?? self.connected,
            readableSwitches: readableSwitches ?? self.readableSwitches,
            writeableSwitches: writeableSwitches ?? self.writeableSwitches
        )
    }

    func jsonData() throws -> Data {
        return try newJSONEncoder().encode(self)
    }

    func jsonString(encoding: String.Encoding = .utf8) throws -> String? {
        return String(data: try self.jsonData(), encoding: encoding)
    }
}

// MARK: - ReadableSwitch
struct ReadableSwitch: Codable {
    let description: String?
    let id: Int
    let name: String?
    let value: Double?
}

// MARK: ReadableSwitch convenience initializers and mutators

extension ReadableSwitch {
    init(data: Data) throws {
        self = try newJSONDecoder().decode(ReadableSwitch.self, from: data)
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
        description: String?? = nil,
        id: Int? = nil,
        name: String?? = nil,
        value: Double?? = nil
    ) -> ReadableSwitch {
        return ReadableSwitch(
            description: description ?? self.description,
            id: id ?? self.id,
            name: name ?? self.name,
            value: value ?? self.value
        )
    }

    func jsonData() throws -> Data {
        return try newJSONEncoder().encode(self)
    }

    func jsonString(encoding: String.Encoding = .utf8) throws -> String? {
        return String(data: try self.jsonData(), encoding: encoding)
    }
}

// MARK: - WriteableSwitch
struct WriteableSwitch: Codable {
    let description: String?
    let id: Int
    let maximum, minimum: Double
    let name: String?
    let stepSize, targetValue: Double
    let value: Double?

    enum CodingKeys: String, CodingKey {
        case description, id, maximum, minimum, name
        case stepSize = "step_size"
        case targetValue = "target_value"
        case value
    }
}

// MARK: WriteableSwitch convenience initializers and mutators

extension WriteableSwitch {
    init(data: Data) throws {
        self = try newJSONDecoder().decode(WriteableSwitch.self, from: data)
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
        description: String?? = nil,
        id: Int? = nil,
        maximum: Double? = nil,
        minimum: Double? = nil,
        name: String?? = nil,
        stepSize: Double? = nil,
        targetValue: Double? = nil,
        value: Double?? = nil
    ) -> WriteableSwitch {
        return WriteableSwitch(
            description: description ?? self.description,
            id: id ?? self.id,
            maximum: maximum ?? self.maximum,
            minimum: minimum ?? self.minimum,
            name: name ?? self.name,
            stepSize: stepSize ?? self.stepSize,
            targetValue: targetValue ?? self.targetValue,
            value: value ?? self.value
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

/// Data when the a new image is taken/saved
// MARK: - ImageSavedData
struct ImageSavedData: Codable {
    let exposureDuration: Double?
    let filePath: String
    let fileType: String?
    let filter: String?
    let imageStatistics: ImageStatistics?
    let imageType: String?
    let isBayered: Bool?
    let pixelHeight, pixelWidth: Int?
    let starStatistics: StarStatistics?

    enum CodingKeys: String, CodingKey {
        case exposureDuration = "exposure_duration"
        case filePath = "file_path"
        case fileType = "file_type"
        case filter
        case imageStatistics = "image_statistics"
        case imageType = "image_type"
        case isBayered = "is_bayered"
        case pixelHeight = "pixel_height"
        case pixelWidth = "pixel_width"
        case starStatistics = "star_statistics"
    }
}

// MARK: ImageSavedData convenience initializers and mutators

extension ImageSavedData {
    init(data: Data) throws {
        self = try newJSONDecoder().decode(ImageSavedData.self, from: data)
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
        exposureDuration: Double?? = nil,
        filePath: String? = nil,
        fileType: String?? = nil,
        filter: String?? = nil,
        imageStatistics: ImageStatistics?? = nil,
        imageType: String?? = nil,
        isBayered: Bool?? = nil,
        pixelHeight: Int?? = nil,
        pixelWidth: Int?? = nil,
        starStatistics: StarStatistics?? = nil
    ) -> ImageSavedData {
        return ImageSavedData(
            exposureDuration: exposureDuration ?? self.exposureDuration,
            filePath: filePath ?? self.filePath,
            fileType: fileType ?? self.fileType,
            filter: filter ?? self.filter,
            imageStatistics: imageStatistics ?? self.imageStatistics,
            imageType: imageType ?? self.imageType,
            isBayered: isBayered ?? self.isBayered,
            pixelHeight: pixelHeight ?? self.pixelHeight,
            pixelWidth: pixelWidth ?? self.pixelWidth,
            starStatistics: starStatistics ?? self.starStatistics
        )
    }

    func jsonData() throws -> Data {
        return try newJSONEncoder().encode(self)
    }

    func jsonString(encoding: String.Encoding = .utf8) throws -> String? {
        return String(data: try self.jsonData(), encoding: encoding)
    }
}

// MARK: - ImageStatistics
struct ImageStatistics: Codable {
    let bitDepth, max: Int?
    let maxOccurrences, mean, median, medianAbsDev: Double?
    let min: Int?
    let minOccurrences, stdev: Double?

    enum CodingKeys: String, CodingKey {
        case bitDepth = "bit_depth"
        case max
        case maxOccurrences = "max_occurrences"
        case mean, median
        case medianAbsDev = "median_abs_dev"
        case min
        case minOccurrences = "min_occurrences"
        case stdev
    }
}

// MARK: ImageStatistics convenience initializers and mutators

extension ImageStatistics {
    init(data: Data) throws {
        self = try newJSONDecoder().decode(ImageStatistics.self, from: data)
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
        bitDepth: Int?? = nil,
        max: Int?? = nil,
        maxOccurrences: Double?? = nil,
        mean: Double?? = nil,
        median: Double?? = nil,
        medianAbsDev: Double?? = nil,
        min: Int?? = nil,
        minOccurrences: Double?? = nil,
        stdev: Double?? = nil
    ) -> ImageStatistics {
        return ImageStatistics(
            bitDepth: bitDepth ?? self.bitDepth,
            max: max ?? self.max,
            maxOccurrences: maxOccurrences ?? self.maxOccurrences,
            mean: mean ?? self.mean,
            median: median ?? self.median,
            medianAbsDev: medianAbsDev ?? self.medianAbsDev,
            min: min ?? self.min,
            minOccurrences: minOccurrences ?? self.minOccurrences,
            stdev: stdev ?? self.stdev
        )
    }

    func jsonData() throws -> Data {
        return try newJSONEncoder().encode(self)
    }

    func jsonString(encoding: String.Encoding = .utf8) throws -> String? {
        return String(data: try self.jsonData(), encoding: encoding)
    }
}

// MARK: - StarStatistics
struct StarStatistics: Codable {
    let detectedStars: Int?
    let hfr, hfrStdDev: Double?

    enum CodingKeys: String, CodingKey {
        case detectedStars = "detected_stars"
        case hfr
        case hfrStdDev = "hfr_std_dev"
    }
}

// MARK: StarStatistics convenience initializers and mutators

extension StarStatistics {
    init(data: Data) throws {
        self = try newJSONDecoder().decode(StarStatistics.self, from: data)
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
        detectedStars: Int?? = nil,
        hfr: Double?? = nil,
        hfrStdDev: Double?? = nil
    ) -> StarStatistics {
        return StarStatistics(
            detectedStars: detectedStars ?? self.detectedStars,
            hfr: hfr ?? self.hfr,
            hfrStdDev: hfrStdDev ?? self.hfrStdDev
        )
    }

    func jsonData() throws -> Data {
        return try newJSONEncoder().encode(self)
    }

    func jsonString(encoding: String.Encoding = .utf8) throws -> String? {
        return String(data: try self.jsonData(), encoding: encoding)
    }
}

/// Data when the profile horizon changes
// MARK: - ProfileHorizonData
struct ProfileHorizonData: Codable {
    let filePath: String?

    enum CodingKeys: String, CodingKey {
        case filePath = "file_path"
    }
}

// MARK: ProfileHorizonData convenience initializers and mutators

extension ProfileHorizonData {
    init(data: Data) throws {
        self = try newJSONDecoder().decode(ProfileHorizonData.self, from: data)
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
        filePath: String?? = nil
    ) -> ProfileHorizonData {
        return ProfileHorizonData(
            filePath: filePath ?? self.filePath
        )
    }

    func jsonData() throws -> Data {
        return try newJSONEncoder().encode(self)
    }

    func jsonString(encoding: String.Encoding = .utf8) throws -> String? {
        return String(data: try self.jsonData(), encoding: encoding)
    }
}

/// Data when the profile list changes
// MARK: - ProfileListData
struct ProfileListData: Codable {
    let action: Action
}

// MARK: ProfileListData convenience initializers and mutators

extension ProfileListData {
    init(data: Data) throws {
        self = try newJSONDecoder().decode(ProfileListData.self, from: data)
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
        action: Action? = nil
    ) -> ProfileListData {
        return ProfileListData(
            action: action ?? self.action
        )
    }

    func jsonData() throws -> Data {
        return try newJSONEncoder().encode(self)
    }

    func jsonString(encoding: String.Encoding = .utf8) throws -> String? {
        return String(data: try self.jsonData(), encoding: encoding)
    }
}

enum Action: String, Codable {
    case add = "Add"
    case remove = "Remove"
    case replace = "Replace"
    case reset = "Reset"
}

/// Data when the profile locale changes
// MARK: - ProfileLocaleData
struct ProfileLocaleData: Codable {
    let name: String?
}

// MARK: ProfileLocaleData convenience initializers and mutators

extension ProfileLocaleData {
    init(data: Data) throws {
        self = try newJSONDecoder().decode(ProfileLocaleData.self, from: data)
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
        name: String?? = nil
    ) -> ProfileLocaleData {
        return ProfileLocaleData(
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

/// Data when the profile location changes
// MARK: - ProfileLocationData
struct ProfileLocationData: Codable {
    let elevation, latitude, longitude: Double?
}

// MARK: ProfileLocationData convenience initializers and mutators

extension ProfileLocationData {
    init(data: Data) throws {
        self = try newJSONDecoder().decode(ProfileLocationData.self, from: data)
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
        elevation: Double?? = nil,
        latitude: Double?? = nil,
        longitude: Double?? = nil
    ) -> ProfileLocationData {
        return ProfileLocationData(
            elevation: elevation ?? self.elevation,
            latitude: latitude ?? self.latitude,
            longitude: longitude ?? self.longitude
        )
    }

    func jsonData() throws -> Data {
        return try newJSONEncoder().encode(self)
    }

    func jsonString(encoding: String.Encoding = .utf8) throws -> String? {
        return String(data: try self.jsonData(), encoding: encoding)
    }
}

/// Data on the currently selected profile
// MARK: - ProfileSelectedData
struct ProfileSelectedData: Codable {
    let description: String?
    let id, name: String
}

// MARK: ProfileSelectedData convenience initializers and mutators

extension ProfileSelectedData {
    init(data: Data) throws {
        self = try newJSONDecoder().decode(ProfileSelectedData.self, from: data)
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
        description: String?? = nil,
        id: String? = nil,
        name: String? = nil
    ) -> ProfileSelectedData {
        return ProfileSelectedData(
            description: description ?? self.description,
            id: id ?? self.id,
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

/// Data when a sequence ends
// MARK: - SequenceEndData
struct SequenceEndData: Codable {
    let name: String?
}

// MARK: SequenceEndData convenience initializers and mutators

extension SequenceEndData {
    init(data: Data) throws {
        self = try newJSONDecoder().decode(SequenceEndData.self, from: data)
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
        name: String?? = nil
    ) -> SequenceEndData {
        return SequenceEndData(
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

/// Data when a sequence starts
// MARK: - SequenceStartData
struct SequenceStartData: Codable {
    let name: String?
}

// MARK: SequenceStartData convenience initializers and mutators

extension SequenceStartData {
    init(data: Data) throws {
        self = try newJSONDecoder().decode(SequenceStartData.self, from: data)
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
        name: String?? = nil
    ) -> SequenceStartData {
        return SequenceStartData(
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

/// Data when the TS Container instruction ends
// MARK: - TargetSchedulerContainerStoppedData
struct TargetSchedulerContainerStoppedData: Codable {
    let stoppedAt: Date?

    enum CodingKeys: String, CodingKey {
        case stoppedAt = "stopped_at"
    }
}

// MARK: TargetSchedulerContainerStoppedData convenience initializers and mutators

extension TargetSchedulerContainerStoppedData {
    init(data: Data) throws {
        self = try newJSONDecoder().decode(TargetSchedulerContainerStoppedData.self, from: data)
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
        stoppedAt: Date?? = nil
    ) -> TargetSchedulerContainerStoppedData {
        return TargetSchedulerContainerStoppedData(
            stoppedAt: stoppedAt ?? self.stoppedAt
        )
    }

    func jsonData() throws -> Data {
        return try newJSONEncoder().encode(self)
    }

    func jsonString(encoding: String.Encoding = .utf8) throws -> String? {
        return String(data: try self.jsonData(), encoding: encoding)
    }
}

/// Data when the TS planner returns a target plan and the target is ‘new’
// MARK: - TargetSchedulerNewTargetStartData
struct TargetSchedulerNewTargetStartData: Codable {
    let binning: String?
    let coordinates: Coordinates?
    let exposure: Double?
    let filter, gain, offset, projectName: String?
    let rotation: Double?
    let targetEndTime: Date?
    let targetName: String?

    enum CodingKeys: String, CodingKey {
        case binning, coordinates, exposure, filter, gain, offset
        case projectName = "project_name"
        case rotation
        case targetEndTime = "target_end_time"
        case targetName = "target_name"
    }
}

// MARK: TargetSchedulerNewTargetStartData convenience initializers and mutators

extension TargetSchedulerNewTargetStartData {
    init(data: Data) throws {
        self = try newJSONDecoder().decode(TargetSchedulerNewTargetStartData.self, from: data)
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
        binning: String?? = nil,
        coordinates: Coordinates?? = nil,
        exposure: Double?? = nil,
        filter: String?? = nil,
        gain: String?? = nil,
        offset: String?? = nil,
        projectName: String?? = nil,
        rotation: Double?? = nil,
        targetEndTime: Date?? = nil,
        targetName: String?? = nil
    ) -> TargetSchedulerNewTargetStartData {
        return TargetSchedulerNewTargetStartData(
            binning: binning ?? self.binning,
            coordinates: coordinates ?? self.coordinates,
            exposure: exposure ?? self.exposure,
            filter: filter ?? self.filter,
            gain: gain ?? self.gain,
            offset: offset ?? self.offset,
            projectName: projectName ?? self.projectName,
            rotation: rotation ?? self.rotation,
            targetEndTime: targetEndTime ?? self.targetEndTime,
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

/// Data when the TS planner completes a target plan (all exposure plans 100% complete)
// MARK: - TargetSchedulerTargetCompleteData
struct TargetSchedulerTargetCompleteData: Codable {
    let coordinates: Coordinates?
    let projectName: String?
    let rotation: Double?
    let targetName: String?

    enum CodingKeys: String, CodingKey {
        case coordinates
        case projectName = "project_name"
        case rotation
        case targetName = "target_name"
    }
}

// MARK: TargetSchedulerTargetCompleteData convenience initializers and mutators

extension TargetSchedulerTargetCompleteData {
    init(data: Data) throws {
        self = try newJSONDecoder().decode(TargetSchedulerTargetCompleteData.self, from: data)
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
        coordinates: Coordinates?? = nil,
        projectName: String?? = nil,
        rotation: Double?? = nil,
        targetName: String?? = nil
    ) -> TargetSchedulerTargetCompleteData {
        return TargetSchedulerTargetCompleteData(
            coordinates: coordinates ?? self.coordinates,
            projectName: projectName ?? self.projectName,
            rotation: rotation ?? self.rotation,
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

/// Data when the TS planner returns a target plan. Sent regardless of whether the target is
/// new or not.
// MARK: - TargetSchedulerTargetStartData
struct TargetSchedulerTargetStartData: Codable {
    let binning: String?
    let coordinates: Coordinates?
    let exposure: Double?
    let filter, gain, offset, projectName: String?
    let rotation: Double?
    let targetEndTime: Date?
    let targetName: String?

    enum CodingKeys: String, CodingKey {
        case binning, coordinates, exposure, filter, gain, offset
        case projectName = "project_name"
        case rotation
        case targetEndTime = "target_end_time"
        case targetName = "target_name"
    }
}

// MARK: TargetSchedulerTargetStartData convenience initializers and mutators

extension TargetSchedulerTargetStartData {
    init(data: Data) throws {
        self = try newJSONDecoder().decode(TargetSchedulerTargetStartData.self, from: data)
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
        binning: String?? = nil,
        coordinates: Coordinates?? = nil,
        exposure: Double?? = nil,
        filter: String?? = nil,
        gain: String?? = nil,
        offset: String?? = nil,
        projectName: String?? = nil,
        rotation: Double?? = nil,
        targetEndTime: Date?? = nil,
        targetName: String?? = nil
    ) -> TargetSchedulerTargetStartData {
        return TargetSchedulerTargetStartData(
            binning: binning ?? self.binning,
            coordinates: coordinates ?? self.coordinates,
            exposure: exposure ?? self.exposure,
            filter: filter ?? self.filter,
            gain: gain ?? self.gain,
            offset: offset ?? self.offset,
            projectName: projectName ?? self.projectName,
            rotation: rotation ?? self.rotation,
            targetEndTime: targetEndTime ?? self.targetEndTime,
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

/// Data when a the target scheduler enter a wait period
// MARK: - TargetSchedulerWaitStartData
struct TargetSchedulerWaitStartData: Codable {
    let coordinates: Coordinates?
    let projectName: String?
    let rotation: Double?
    let secsTillNextTarget: Int?
    let targetName: String?
    let waitEndTime: Date?

    enum CodingKeys: String, CodingKey {
        case coordinates
        case projectName = "project_name"
        case rotation
        case secsTillNextTarget = "secs_till_next_target"
        case targetName = "target_name"
        case waitEndTime = "wait_end_time"
    }
}

// MARK: TargetSchedulerWaitStartData convenience initializers and mutators

extension TargetSchedulerWaitStartData {
    init(data: Data) throws {
        self = try newJSONDecoder().decode(TargetSchedulerWaitStartData.self, from: data)
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
        coordinates: Coordinates?? = nil,
        projectName: String?? = nil,
        rotation: Double?? = nil,
        secsTillNextTarget: Int?? = nil,
        targetName: String?? = nil,
        waitEndTime: Date?? = nil
    ) -> TargetSchedulerWaitStartData {
        return TargetSchedulerWaitStartData(
            coordinates: coordinates ?? self.coordinates,
            projectName: projectName ?? self.projectName,
            rotation: rotation ?? self.rotation,
            secsTillNextTarget: secsTillNextTarget ?? self.secsTillNextTarget,
            targetName: targetName ?? self.targetName,
            waitEndTime: waitEndTime ?? self.waitEndTime
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
