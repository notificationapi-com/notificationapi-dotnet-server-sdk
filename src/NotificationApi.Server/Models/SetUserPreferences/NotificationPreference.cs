using System.Diagnostics.CodeAnalysis;

namespace NotificationApi.Server.Models;

/// <summary>
/// Represents a notification preference for a user.
/// </summary>
public class NotificationPreference
{
    /// <summary>
    /// Gets or sets the notification ID.
    /// </summary>
    public required string NotificationId { get; set; }

    /// <summary>
    /// Gets or sets the notification channel.
    /// </summary>
    public required NotificationChannel Channel { get; set; }

    /// <summary>
    /// Gets or sets the state of the notification preference.
    /// </summary>
    public required bool State { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="NotificationPreference"/> class.
    /// </summary>
    /// <param name="notificationId">The notification ID.</param>
    /// <param name="channel">The notification channel.</param>
    /// <param name="state">The state of the notification preference.</param>
    [SetsRequiredMembers]
    [ExcludeFromCodeCoverage]
    public NotificationPreference(string notificationId, NotificationChannel channel, bool state)
    {
        NotificationId = notificationId;
        Channel = channel;
        State = state;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NotificationPreference"/> class.
    /// </summary>
    public NotificationPreference()
    {
    }
}