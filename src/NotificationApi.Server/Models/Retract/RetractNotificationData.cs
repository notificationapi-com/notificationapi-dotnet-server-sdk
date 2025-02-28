using System.Diagnostics.CodeAnalysis;

namespace NotificationApi.Server.Models;

/// <summary>
/// Represents the data required to retract a notification.
/// </summary>
public class RetractNotificationData
{
    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public required string UserId { get; set; }

    /// <summary>
    /// Gets or sets the notification ID.
    /// </summary>
    public required string NotificationId { get; set; }

    /// <summary>
    /// Gets or sets the secondary ID.
    /// </summary>
    public string? SecondaryId { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="RetractNotificationData"/> class.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <param name="notificationId">The notification ID.</param>
    [SetsRequiredMembers]
    [ExcludeFromCodeCoverage]
    public RetractNotificationData(string userId, string notificationId)
    {
        UserId = userId;
        NotificationId = notificationId;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RetractNotificationData"/> class.
    /// </summary>
    public RetractNotificationData()
    {
    }
}