namespace NotificationApi.Server.Models;

/// <summary>
/// Represents the options for a notification email.
/// </summary>
public class NotificationEmailOptions
{
    /// <summary>
    /// Gets or sets the reply-to addresses for the email.
    /// </summary>
    public string[]? ReplyToAddresses { get; set; }

    /// <summary>
    /// Gets or sets the CC (carbon copy) addresses for the email.
    /// </summary>
    public string[]? CcAddresses { get; set; }

    /// <summary>
    /// Gets or sets the BCC (blind carbon copy) addresses for the email.
    /// </summary>
    public string[]? BccAddresses { get; set; }

    /// <summary>
    /// Gets or sets the From name of an email.
    /// </summary>
    public string? FromName { get; set; }

    /// <summary>
    /// Gets or sets the From Address of an email.
    /// </summary>
    public string? FromAddress { get; set; }

    /// <summary>
    /// Gets or sets the attachments for the email.
    /// </summary>
    public NotificationEmailAttachments[]? Attachments { get; set; }
}