namespace NotificationApi.Server.Models;

/// <summary>
/// Represents the available notification channels.
/// </summary>
public enum NotificationChannel
{
    /// <summary>
    /// Email notification channel.
    /// </summary>
    EMAIL,

    /// <summary>
    /// In-app web notification channel.
    /// </summary>
    INAPP_WEB,

    /// <summary>
    /// Web push notification channel.
    /// </summary>
    WEB_PUSH,

    /// <summary>
    /// SMS notification channel.
    /// </summary>
    SMS,

    /// <summary>
    /// Push notification channel.
    /// </summary>
    PUSH,

    /// <summary>
    /// Call notification channel.
    /// </summary>
    CALL
}