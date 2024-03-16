using System.Diagnostics.CodeAnalysis;

namespace NotificationApi.Server.Models;

/// <summary>
/// Represents a subscription for web push notifications.
/// </summary>
public class NotificationWebPushTokenSub
{
    /// <summary>
    /// Gets or sets the endpoint of the web push notification.
    /// </summary>
    public required string Endpoint { get; set; }

    /// <summary>
    /// Gets or sets the keys for the web push notification.
    /// </summary>
    public required NotificationWebPushTokenKeys Keys { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="NotificationWebPushTokenSub"/> class.
    /// </summary>
    /// <param name="endpoint">The endpoint of the web push notification.</param>
    /// <param name="keys">The keys for the web push notification.</param>
    [SetsRequiredMembers]
    [ExcludeFromCodeCoverage]
    public NotificationWebPushTokenSub(string endpoint, NotificationWebPushTokenKeys keys)
    {
        Endpoint = endpoint;
        Keys = keys;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NotificationWebPushTokenSub"/> class.
    /// </summary>
    public NotificationWebPushTokenSub()
    {
    }
}