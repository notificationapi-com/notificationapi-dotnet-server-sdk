namespace NotificationApi.Server.Models;

/// <summary>
/// Represents the options for sending a notification.
/// </summary>
public class NotificationOptions
{
    /// <summary>
    /// Gets or sets the email notification options.
    /// </summary>
    public NotificationEmailOptions? Email { get; set; }

    /// <summary>
    /// Gets or sets the Apple Push Notification (APN) options.
    /// </summary>
    public NotificationApnOptions? Apn { get; set; }

    /// <summary>
    /// Gets or sets the Firebase Cloud Messaging (FCM) options.
    /// </summary>
    public NotificationFcmOptions? Fcm { get; set; }
}