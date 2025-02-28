namespace NotificationApi.Server.Models;

/// <summary>
/// Represents the options for sending APN (Apple Push Notification) notifications.
/// </summary>
public class NotificationApnOptions
{
    /// <summary>
    /// Gets or sets the expiry time of the notification.
    /// </summary>
    public int? Expiry { get; set; }

    /// <summary>
    /// Gets or sets the priority of the notification.
    /// </summary>
    public int? Priority { get; set; }

    /// <summary>
    /// Gets or sets the collapse identifier of the notification.
    /// </summary>
    public string? CollapseId { get; set; }

    /// <summary>
    /// Gets or sets the thread identifier of the notification.
    /// </summary>
    public string? ThreadId { get; set; }

    /// <summary>
    /// Gets or sets the badge count of the notification.
    /// </summary>
    public int? Badge { get; set; }

    /// <summary>
    /// Gets or sets the sound of the notification.
    /// </summary>
    public string? Sound { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the notification content is available.
    /// </summary>
    public bool? ContentAvailable { get; set; }
}