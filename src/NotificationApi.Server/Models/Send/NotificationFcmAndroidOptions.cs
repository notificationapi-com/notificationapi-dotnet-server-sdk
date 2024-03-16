namespace NotificationApi.Server.Models;

/// <summary>
/// Represents the options for sending FCM notifications to Android devices.
/// </summary>
public class NotificationFcmAndroidOptions
{
    /// <summary>
    /// Gets or sets the collapse key for the notification.
    /// </summary>
    public string? CollapseKey { get; set; }

    /// <summary>
    /// Gets or sets the priority of the notification.
    /// </summary>
    public string? Priority { get; set; }

    /// <summary>
    /// Gets or sets the time to live (TTL) for the notification.
    /// </summary>
    public string? Ttl { get; set; }
}