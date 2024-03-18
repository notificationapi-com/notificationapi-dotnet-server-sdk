using System.Diagnostics.CodeAnalysis;

namespace NotificationApi.Server.Models;

/// <summary>
/// Represents the keys required for a web push notification token.
/// </summary>
public class NotificationWebPushTokenKeys
{
    /// <summary>
    /// Gets or sets the P256dh key.
    /// </summary>
    public required string P256dh { get; set; }

    /// <summary>
    /// Gets or sets the Auth key.
    /// </summary>
    public required string Auth { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="NotificationWebPushTokenKeys"/> class.
    /// </summary>
    /// <param name="p256dh">The P256dh key.</param>
    /// <param name="auth">The Auth key.</param>
    [SetsRequiredMembers]
    [ExcludeFromCodeCoverage]
    public NotificationWebPushTokenKeys(string p256dh, string auth)
    {
        P256dh = p256dh;
        Auth = auth;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NotificationWebPushTokenKeys"/> class.
    /// </summary>
    public NotificationWebPushTokenKeys()
    {
    }
}