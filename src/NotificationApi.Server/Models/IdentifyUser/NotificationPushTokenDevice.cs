using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace NotificationApi.Server.Models;

/// <summary>
/// Represents a notification push token device.
/// </summary>
public class NotificationPushTokenDevice
{
    /// <summary>
    /// Gets or sets the app ID.
    /// </summary>
    [JsonPropertyName("app_id")]
    public string? AppId { get; set; }

    /// <summary>
    /// Gets or sets the ad ID.
    /// </summary>
    [JsonPropertyName("ad_id")]
    public string? AdId { get; set; }

    /// <summary>
    /// Gets or sets the device ID.
    /// </summary>
    [JsonPropertyName("device_id")]
    public required string DeviceId { get; set; }

    /// <summary>
    /// Gets or sets the platform.
    /// </summary>
    public string? Platform { get; set; }

    /// <summary>
    /// Gets or sets the manufacturer.
    /// </summary>
    public string? Manufacturer { get; set; }

    /// <summary>
    /// Gets or sets the model.
    /// </summary>
    public string? Model { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="NotificationPushTokenDevice"/> class.
    /// </summary>
    /// <param name="deviceId">The device ID.</param>
    [SetsRequiredMembers]
    [ExcludeFromCodeCoverage]
    public NotificationPushTokenDevice(string deviceId)
    {
        DeviceId = deviceId;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NotificationPushTokenDevice"/> class.
    /// </summary>
    public NotificationPushTokenDevice()
    {
    }
}