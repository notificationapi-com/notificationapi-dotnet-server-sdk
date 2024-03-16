using System.Text.Json.Serialization;

namespace NotificationApi.Server.Models;

/// <summary>
/// Represents the data for identifying a user.
/// </summary>
public class IdentifyUserData
{
    /// <summary>
    /// Gets or sets the email address of the user.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets the telephone number of the user.
    /// </summary>
    [JsonPropertyName("number")]
    public string? TelephoneNumber { get; set; }

    /// <summary>
    /// Gets or sets the list of push tokens for the user.
    /// </summary>
    public List<NotificationPushToken>? PushTokens { get; set; }

    /// <summary>
    /// Gets or sets the list of web push tokens for the user.
    /// </summary>
    public List<NotificationWebPushToken>? WebPushTokens { get; set; }
}