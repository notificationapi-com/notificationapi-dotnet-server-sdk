using System.Text.Json;
using System.Text.Json.Serialization;

namespace NotificationApi.Server.Utilities;

internal static class Configuration
{
    public static JsonSerializerOptions JsonSerializerOptions { get; } = new JsonSerializerOptions()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        Converters =
        {
            new JsonStringEnumConverter(),
            new DateTimeConverter(),
        }
    };
}