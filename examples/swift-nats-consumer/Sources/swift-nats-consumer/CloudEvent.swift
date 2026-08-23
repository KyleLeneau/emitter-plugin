/// CloudEvents 1.0 envelope, hand-written from `schema/schemas/cloudevents.json`.
///
/// Emitter always publishes in structured content mode: a single JSON object with the
/// domain payload inlined in `data`, so `data_base64` is not modeled here.
///
/// Generic over the decoded shape of `data` so the same envelope can be reused for every
/// event `type` — see `WebhookHandler` for how `data`'s concrete type is chosen.
///
/// The generic parameter is named `Payload` (not `Data`) so it doesn't shadow `Foundation.Data`
/// in files that decode a `CloudEvent` from raw request bytes.
struct CloudEvent<Payload: Decodable>: Decodable {
    let id: String
    let source: String
    let specversion: String
    let type: String
    let datacontenttype: String?
    let dataschema: String?
    let subject: String?
    let time: String?
    let data: Payload?
}

/// Just enough of the envelope to read `type` and route to the matching `CloudEvent<T>`
/// before committing to a concrete `data` payload type.
struct CloudEventHeader: Decodable {
    let id: String
    let source: String
    let specversion: String
    let type: String
    let time: String?
}

/// A loosely-typed JSON value, used as the `data` payload for CloudEvent types this example
/// doesn't map to a generated model (see the `default` case in `WebhookHandler`). Lets the
/// fallback branch decode and log the raw payload instead of failing.
enum JSONValue: Decodable {
    case string(String)
    case number(Double)
    case bool(Bool)
    case object([String: JSONValue])
    case array([JSONValue])
    case null

    init(from decoder: Decoder) throws {
        let container = try decoder.singleValueContainer()
        if container.decodeNil() {
            self = .null
        } else if let value = try? container.decode(Bool.self) {
            self = .bool(value)
        } else if let value = try? container.decode(Double.self) {
            self = .number(value)
        } else if let value = try? container.decode(String.self) {
            self = .string(value)
        } else if let value = try? container.decode([String: JSONValue].self) {
            self = .object(value)
        } else if let value = try? container.decode([JSONValue].self) {
            self = .array(value)
        } else {
            throw DecodingError.dataCorruptedError(
                in: container,
                debugDescription: "Unsupported JSON value"
            )
        }
    }
}

extension JSONValue: CustomStringConvertible {
    var description: String {
        switch self {
        case .string(let value): return "\"\(value)\""
        case .number(let value): return "\(value)"
        case .bool(let value): return "\(value)"
        case .null: return "null"
        case .object(let value):
            let entries = value.map { "\"\($0.key)\": \($0.value)" }.sorted().joined(separator: ", ")
            return "{\(entries)}"
        case .array(let value):
            return "[\(value.map(\.description).joined(separator: ", "))]"
        }
    }
}
