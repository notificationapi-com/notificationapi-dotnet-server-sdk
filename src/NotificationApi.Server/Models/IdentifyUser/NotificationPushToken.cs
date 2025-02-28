using System.Diagnostics.CodeAnalysis;

namespace NotificationApi.Server.Models;

/// <summary>
/// Represents a notification push token.
/// </summary>
public class NotificationPushToken
{
    /// <summary>
    /// Gets or sets the type of the notification push provider.
    /// </summary>
    public required NotificationPushProviders Type { get; set; }

    /// <summary>
    /// Gets or sets the token value.
    /// </summary>
    public required string Token { get; set; }

    /// <summary>
    /// Gets or sets the device associated with the token.
    /// </summary>
    public required NotificationPushTokenDevice Device { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="NotificationPushToken"/> class.
    /// </summary>
    /// <param name="type">The type of the notification push provider.</param>
    /// <param name="token">The token value.</param>
    /// <param name="device">The device associated with the token.</param>
    [SetsRequiredMembers]
    [ExcludeFromCodeCoverage]
    public NotificationPushToken(NotificationPushProviders type, string token, NotificationPushTokenDevice device)
    {
        Type = type;
        Token = token;
        Device = device;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NotificationPushToken"/> class.
    /// </summary>
    public NotificationPushToken()
    {
    }
}