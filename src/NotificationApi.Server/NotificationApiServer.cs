using NotificationApi.Server.Models;
using NotificationApi.Server.Utilities;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Json;
using System.Text;

namespace NotificationApi.Server;

/// <summary>
/// Represents a server for the Notification API.
/// </summary>
public class NotificationApiServer
{
    /// <summary>
    /// The default base URL for the US region.
    /// </summary>
    public const string US_BASE_URL = "https://api.notificationapi.com";

    /// <summary>
    /// The base URL for the EU region.
    /// </summary>
    public const string EU_BASE_URL = "https://api.eu.notificationapi.com";

    /// <summary>
    /// The base URL for the CA region.
    /// </summary>
    public const string CA_BASE_URL = "https://api.ca.notificationapi.com";

    private readonly string clientId;
    private readonly string clientSecret;
    private readonly bool secureMode;
    private readonly HttpClient httpClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="NotificationApiServer"/> class.
    /// </summary>
    /// <param name="clientId">The client ID.</param>
    /// <param name="clientSecret">The client secret.</param>
    /// <param name="secureMode">Indicates whether secure mode is enabled.</param>
    /// <param name="baseAddress">The base address of the API. Use the predefined constants US_BASE_URL, EU_BASE_URL, or CA_BASE_URL.</param>
    [ExcludeFromCodeCoverage]
    public NotificationApiServer(string clientId, string clientSecret, bool secureMode, string baseAddress = US_BASE_URL) : this(new HttpClient(), clientId, clientSecret, secureMode, baseAddress)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NotificationApiServer"/> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client.</param>
    /// <param name="clientId">The client ID.</param>
    /// <param name="clientSecret">The client secret.</param>
    /// <param name="secureMode">Indicates whether secure mode is enabled.</param>
    /// <param name="baseAddress">The base address of the API. Use the predefined constants US_BASE_URL, EU_BASE_URL, or CA_BASE_URL.</param>
    public NotificationApiServer(HttpClient httpClient, string clientId, string clientSecret, bool secureMode, string baseAddress = US_BASE_URL)
    {
        string authToken = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{clientId}:{clientSecret}"));

        httpClient.BaseAddress = new Uri(new Uri(baseAddress), $"{clientId}/");
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {authToken}");

        this.clientId = clientId;
        this.clientSecret = clientSecret;
        this.secureMode = secureMode;
        this.httpClient = httpClient;
    }

    /// <summary>
    /// Sends a notification.
    /// </summary>
    /// <param name="sendNotificationData">The data for sending the notification.</param>
    /// <returns>The HTTP response message.</returns>
    public async Task<HttpResponseMessage> Send(SendNotificationData sendNotificationData)
    {
        return await httpClient.PostAsJsonAsync("sender", sendNotificationData, Configuration.JsonSerializerOptions);
    }

    /// <summary>
    /// Retracts a notification.
    /// </summary>
    /// <param name="retractNotificationData">The data for retracting the notification.</param>
    /// <returns>The HTTP response message.</returns>
    public async Task<HttpResponseMessage> Retract(RetractNotificationData retractNotificationData)
    {
        return await httpClient.PostAsJsonAsync("sender/retract", retractNotificationData, Configuration.JsonSerializerOptions);
    }

    /// <summary>
    /// Identifies a user.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="identifyUserData">The data for identifying the user.</param>
    /// <returns>The HTTP response message.</returns>
    public async Task<HttpResponseMessage> Identify(string userId, IdentifyUserData identifyUserData)
    {
        string authToken;

        if (secureMode)
        {
            string hashedUserId = UserIdHasher.Hash(userId, clientSecret);
            authToken = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{clientId}:{userId}:{hashedUserId}"));
        }
        else
        {
            authToken = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{clientId}:{userId}"));
        }

        HttpRequestMessage request = new(HttpMethod.Post, $"users/{userId}")
        {
            Content = JsonContent.Create(identifyUserData, options: Configuration.JsonSerializerOptions),
        };

        request.Headers.Add("Authorization", $"Basic {authToken}");

        string json = System.Text.Json.JsonSerializer.Serialize(identifyUserData, Configuration.JsonSerializerOptions);
        Trace.WriteLine(json);

        return await httpClient.SendAsync(request);
    }

    /// <summary>
    /// Sets user preferences.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="setUserPreferencesData">The data for setting user preferences.</param>
    /// <returns>The HTTP response message.</returns>
    public async Task<HttpResponseMessage> SetUserPreferences(string userId, SetUserPreferencesData setUserPreferencesData)
    {
        return await httpClient.PostAsJsonAsync($"user_preferences/{userId}", setUserPreferencesData.Preferences, Configuration.JsonSerializerOptions);
    }

    /// <summary>
    /// Updates an in-app notification.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="updateData">The data for updating the in-app notification.</param>
    /// <returns>The HTTP response message.</returns>
    public async Task<HttpResponseMessage> UpdateInAppNotification(string userId, InAppNotificationPatchData updateData)
    {
        string authToken;

        if (secureMode)
        {
            string hashedUserId = UserIdHasher.Hash(userId, clientSecret);
            authToken = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{clientId}:{userId}:{hashedUserId}"));
        }
        else
        {
            authToken = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{clientId}:{userId}"));
        }

        HttpRequestMessage request = new(HttpMethod.Patch, $"users/{userId}/notifications/INAPP_WEB")
        {
            Content = JsonContent.Create(updateData, options: Configuration.JsonSerializerOptions),
        };

        request.Headers.Add("Authorization", $"Basic {authToken}");

        string json = System.Text.Json.JsonSerializer.Serialize(updateData, Configuration.JsonSerializerOptions);
        Trace.WriteLine(json);

        return await httpClient.SendAsync(request);
    }

    public async Task<HttpResponseMessage> UpdateSchedule(string trackingId, UpdateScheduleData updateScheduleData)
    {
        string json = System.Text.Json.JsonSerializer.Serialize(updateScheduleData, Configuration.JsonSerializerOptions);
        Trace.WriteLine(json);

        return await httpClient.PutAsJsonAsync($"schedule/{trackingId}", updateScheduleData, Configuration.JsonSerializerOptions);
    }

    public async Task<HttpResponseMessage> DeleteSchedule(string trackingId)
    {
        return await httpClient.DeleteAsync($"schedule/{trackingId}");
    }

    /// <summary>
    ///  create a query on logs.
    /// </summary>
    /// <param name="queryLogsData">The data for query logs.</param>
    /// <returns>The HTTP response message.</returns>
    public async Task<HttpResponseMessage> QueryLogs(QueryLogsData queryLogsData)
    {
        return await httpClient.PostAsJsonAsync("logs/query", queryLogsData, Configuration.JsonSerializerOptions);
    }
    // TODO: Create CreateSubNotification method
    // TODO: Create DeleteSubNotification method
}