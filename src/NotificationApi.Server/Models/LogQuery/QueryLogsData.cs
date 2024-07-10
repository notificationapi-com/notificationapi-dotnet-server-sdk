namespace NotificationApi.Server.Models
{
    /// <summary>
    /// Represents the data required to query logs.
    /// </summary>
    public class QueryLogsData
    {
        /// <summary>
        /// Gets or sets the date range filter.
        /// </summary>
        public DateRangeFilter? DateRangeFilter { get; set; }

        /// <summary>
        /// Gets or sets the notification filter.
        /// </summary>
        public List<string>? NotificationFilter { get; set; }

        /// <summary>
        /// Gets or sets the channel filter.
        /// </summary>
        public List<Channels>? ChannelFilter { get; set; }

        /// <summary>
        /// Gets or sets the user filter.
        /// </summary>
        public List<string>? UserFilter { get; set; }

        /// <summary>
        /// Gets or sets the status filter.
        /// </summary>
        public List<string>? StatusFilter { get; set; }

        /// <summary>
        /// Gets or sets the tracking IDs.
        /// </summary>
        public List<string>? TrackingIds { get; set; }

        /// <summary>
        /// Gets or sets the request filter.
        /// </summary>
        public List<string>? RequestFilter { get; set; }

        /// <summary>
        /// Gets or sets the environment ID filter.
        /// </summary>
        public List<string>? EnvIdFilter { get; set; }

        /// <summary>
        /// Gets or sets the custom filter.
        /// </summary>
        public string? CustomFilter { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryLogsData"/> class.
        /// </summary>
        public QueryLogsData()
        {
        }
    }

    /// <summary>
    /// Represents a date range filter.
    /// </summary>
    public class DateRangeFilter
    {
        /// <summary>
        /// Gets or sets the start time of the date range filter as a Unix timestamp.
        /// </summary>
        public long? StartTime { get; set; }

        /// <summary>
        /// Gets or sets the end time of the date range filter as a Unix timestamp.
        /// </summary>
        public long? EndTime { get; set; }
    }

    /// <summary>
    /// Represents the different channels available for filtering logs.
    /// </summary>
    public enum Channels
    {
        EMAIL,
        INAPP_WEB,
        SMS,
        CALL,
        PUSH,
        WEB_PUSH,
    }
}
