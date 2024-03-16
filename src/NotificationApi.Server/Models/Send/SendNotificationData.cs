using System.Diagnostics.CodeAnalysis;

namespace NotificationApi.Server.Models;

/// <summary>
/// Represents the data for sending a notification.
/// </summary>
public class SendNotificationData
{
    /// <summary>
    /// Gets or sets the notification ID.
    /// </summary>
    public required string NotificationId { get; set; }

    /// <summary>
    /// Gets or sets the sub-notification ID.
    /// </summary>
    public string? SubNotificationId { get; set; }

    /// <summary>
    /// Gets or sets the template ID.
    /// </summary>
    public string? TemplateId { get; set; }

    /// <summary>
    /// Gets or sets the user for the notification.
    /// </summary>
    public required NotificationUser User { get; set; }

    /// <summary>
    /// Gets or sets the merge tags for the notification.
    /// </summary>
    public Dictionary<string, object>? MergeTags { get; set; }

    /// <summary>
    /// Gets or sets the replace tags for the notification.
    /// </summary>
    public Dictionary<string, string>? Replace { get; set; }

    /// <summary>
    /// Gets or sets the time when the notification should be scheduled.
    /// </summary>
    public DateTime? Schedule { get; set; }

    /// <summary>
    /// Gets or sets the options for the notification.
    /// </summary>
    public NotificationOptions? Options { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SendNotificationData"/> class.
    /// </summary>
    /// <param name="notificationId">The notification ID.</param>
    /// <param name="user">The user for the notification.</param>
    [SetsRequiredMembers]
    [ExcludeFromCodeCoverage]
    public SendNotificationData(string notificationId, NotificationUser user)
    {
        NotificationId = notificationId;
        User = user;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SendNotificationData"/> class.
    /// </summary>
    public SendNotificationData()
    {
    }
}