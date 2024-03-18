using NotificationApi.Server.Utilities;

using System.Text.Json;

namespace NotificationApi.Server.Tests.Utilities;

[TestClass]
public class DateTimeConverterTests
{
    [TestMethod]
    public void Read_ValidDateTime_ReturnsDateTimeInUniversalTime()
    {
        // Act
        DateTime actualDateTime = JsonSerializer.Deserialize<DateTime>("\"2024-03-16T00:15:34Z\"", Configuration.JsonSerializerOptions);

        // Assert
        Assert.AreEqual(new DateTime(2024, 3, 16, 0, 15, 34, DateTimeKind.Utc), actualDateTime);
    }

    [TestMethod]
    public void Write_ValidDateTime_WritesDateTimeInUniversalTime()
    {
        // Act
        string actualJson = JsonSerializer.Serialize(new DateTime(2024, 3, 16, 12, 5, 54, DateTimeKind.Utc), Configuration.JsonSerializerOptions);

        // Assert
        Assert.AreEqual("\"2024-03-16T12:05:54Z\"", actualJson);
    }
}