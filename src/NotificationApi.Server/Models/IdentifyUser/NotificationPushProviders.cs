namespace NotificationApi.Server.Models;

/// <summary>
/// Represents the notification push providers.
/// </summary>
public enum NotificationPushProviders
{
    /// <summary>
    /// Firebase Cloud Messaging.
    /// </summary>
    FCM,

    /// <summary>
    /// Apple Push Notification service.
    /// </summary>
    APM
}