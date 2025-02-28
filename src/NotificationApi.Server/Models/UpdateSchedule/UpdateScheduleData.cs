using System.Diagnostics.CodeAnalysis;

namespace NotificationApi.Server.Models;

public class UpdateScheduleData
{
    public required DateTime Schedule { get; set; }

    [SetsRequiredMembers]
    [ExcludeFromCodeCoverage]
    public UpdateScheduleData(DateTime schedule)
    {
        Schedule = schedule;
    }

    public UpdateScheduleData()
    {
    }
}