using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace NotificationApi.Server.Models;

/// <summary>
/// Represents a notification user.
/// </summary>
public class NotificationUser
{
    /// <summary>
    /// Gets or sets the ID of the user.
    /// </summary>
    public required string Id { get; set; }

    /// <summary>
    /// Gets or sets the email of the user.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets the telephone number of the user.
    /// </summary>
    [JsonPropertyName("number")]
    public string? TelephoneNumber { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="NotificationUser"/> class.
    /// </summary>
    public NotificationUser()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NotificationUser"/> class with the specified ID.
    /// </summary>
    /// <param name="id">The ID of the user.</param>
    [SetsRequiredMembers]
    [ExcludeFromCodeCoverage]
    public NotificationUser(string id)
    {
        Id = id;
    }
}