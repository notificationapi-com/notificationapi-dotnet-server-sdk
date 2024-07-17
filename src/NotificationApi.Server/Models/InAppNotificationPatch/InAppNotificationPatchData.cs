namespace NotificationApi.Server.Models
{
    /// <summary>
    /// Represents the data required to patch an in-app notification.
    /// </summary>
    public class InAppNotificationPatchData
    {
        /// <summary>
        /// Gets or sets the tracking IDs.
        /// </summary>
        public List<string> TrackingIds { get; set; }

        /// <summary>
        /// Gets or sets the opened timestamp.
        /// </summary>
        public string? Opened { get; set; }

        /// <summary>
        /// Gets or sets the clicked timestamp.
        /// </summary>
        public string? Clicked { get; set; }

        /// <summary>
        /// Gets or sets the archived timestamp.
        /// </summary>
        public string? Archived { get; set; }

        /// <summary>
        /// Gets or sets the first actioned timestamp.
        /// </summary>
        public string? Actioned1 { get; set; }

        /// <summary>
        /// Gets or sets the second actioned timestamp.
        /// </summary>
        public string? Actioned2 { get; set; }

        /// <summary>
        /// Gets or sets the reply details.
        /// </summary>
        public ReplyData? Reply { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InAppNotificationPatchData"/> class.
        /// </summary>
        public InAppNotificationPatchData()
        {
            TrackingIds = new List<string>();
        }
    }

    /// <summary>
    /// Represents the reply details for an in-app notification.
    /// </summary>
    public class ReplyData
    {
        /// <summary>
        /// Gets or sets the reply date.
        /// </summary>
        public required string Date { get; set; }

        /// <summary>
        /// Gets or sets the reply message.
        /// </summary>
        public required string Message { get; set; }
    }
}
