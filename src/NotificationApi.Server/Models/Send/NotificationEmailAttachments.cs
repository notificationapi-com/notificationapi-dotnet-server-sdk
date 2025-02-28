using System.Diagnostics.CodeAnalysis;

namespace NotificationApi.Server.Models;

/// <summary>
/// Represents an email attachment for notification emails.
/// </summary>
public class NotificationEmailAttachments
{
    /// <summary>
    /// Gets or sets the file name of the attachment.
    /// </summary>
    public required string FileName { get; set; }

    /// <summary>
    /// Gets or sets the URL of the attachment.
    /// </summary>
    public required string Url { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="NotificationEmailAttachments"/> class.
    /// </summary>
    /// <param name="fileName">The file name of the attachment.</param>
    /// <param name="url">The URL of the attachment.</param>
    [SetsRequiredMembers]
    [ExcludeFromCodeCoverage]
    public NotificationEmailAttachments(string fileName, string url)
    {
        FileName = fileName;
        Url = url;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NotificationEmailAttachments"/> class.
    /// </summary>
    public NotificationEmailAttachments()
    {
    }
}