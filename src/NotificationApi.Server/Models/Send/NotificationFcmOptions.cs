namespace NotificationApi.Server.Models;

/// <summary>
/// Represents the options for sending FCM notifications.
/// </summary>
public class NotificationFcmOptions
{
    /// <summary>
    /// Gets or sets the Android-specific options for FCM notifications.
    /// </summary>
    public NotificationFcmAndroidOptions? Android { get; set; }
}