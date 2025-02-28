using System.Net;
using System.Text;
using System.Text.Json;
using NotificationApi.Server.Models;
using NotificationApi.Server.Utilities;
using RichardSzalay.MockHttp;

namespace NotificationApi.Server.Tests;

[TestClass]
public class NotificationApiServerTests
{
    [TestMethod]
    public async Task Send_ValidData_ReturnsHttpResponseMessage()
    {
        // Arrange
        const string clientId = "testClientId";
        const string clientSecret = "testClientSecret";
        const bool secureMode = false;

        MockHttpMessageHandler mockHttp = new MockHttpMessageHandler();

        _ = mockHttp.Expect(HttpMethod.Post, $"https://api.notificationapi.com/{clientId}/sender")
            .WithHeaders("Authorization", $"Basic {Convert.ToBase64String(Encoding.ASCII.GetBytes($"{clientId}:{clientSecret}"))}")
            .WithContent("""{"notificationId":"testNotificationId","subNotificationId":"testSubNotificationId","templateId":"testTemplateId","user":{"id":"testUserId","email":"test@email.com","number":"1234567890"},"mergeTags":{"testMergeTag":"testMergeTagValue"},"replace":{"testReplaceTag":"testReplaceTagValue"},"schedule":"2024-03-16T14:36:30Z","options":{"email":{"replyToAddresses":["test@email.com"],"ccAddresses":["test@email.com"],"bccAddresses":["test@email.com"],"attachments":[{"fileName":"testFilename","url":"https://test.com"}]},"apn":{"expiry":3600,"priority":10,"collapseId":"testCollapseId","threadId":"testThreadId","badge":1,"sound":"testSound","contentAvailable":true},"fcm":{"android":{"collapseKey":"testCollapseKey","priority":"testPriority","ttl":"3600s"}}}}""")
            .Respond(HttpStatusCode.OK);

        NotificationApiServer notificationApiServer = new NotificationApiServer(mockHttp.ToHttpClient(), clientId, clientSecret, secureMode);

        SendNotificationData sendNotificationData = new SendNotificationData()
        {
            NotificationId = "testNotificationId",
            SubNotificationId = "testSubNotificationId",
            Schedule = DateTime.FromFileTimeUtc(133550733900000000),
            TemplateId = "testTemplateId",
            User = new()
            {
                Id = "testUserId",
                Email = "test@email.com",
                TelephoneNumber = "1234567890"
            },
            MergeTags = new()
            {
                { "testMergeTag", "testMergeTagValue" }
            },
            Replace = new()
            {
               { "testReplaceTag", "testReplaceTagValue" }
            },
            Options = new()
            {
                Apn = new()
                {
                    Badge = 1,
                    Sound = "testSound",
                    ContentAvailable = true,
                    CollapseId = "testCollapseId",
                    Expiry = 3600,
                    Priority = 10,
                    ThreadId = "testThreadId"
                },
                Fcm = new()
                {
                    Android = new()
                    {
                        CollapseKey = "testCollapseKey",
                        Priority = "testPriority",
                        Ttl = "3600s",
                    }
                },
                Email = new()
                {
                    ReplyToAddresses = ["test@email.com"],
                    BccAddresses = ["test@email.com"],
                    CcAddresses = ["test@email.com"],
                    Attachments =
                    [
                        new()
                        {
                           FileName = "testFilename",
                           Url = "https://test.com"
                        }
                    ]
                }
            }
        };

        // Act
        HttpResponseMessage response = await notificationApiServer.Send(sendNotificationData);

        // Assert
        Assert.IsTrue(response.IsSuccessStatusCode, "Should be success status code");
    }

    [TestMethod]
    public async Task Retract_ValidData_ReturnsHttpResponseMessage()
    {
        // Arrange
        const string clientId = "testClientId";
        const string clientSecret = "testClientSecret";
        const bool secureMode = false;

        MockHttpMessageHandler mockHttp = new MockHttpMessageHandler();

        _ = mockHttp.Expect(HttpMethod.Post, $"https://api.notificationapi.com/{clientId}/sender/retract")
            .WithHeaders("Authorization", $"Basic {Convert.ToBase64String(Encoding.ASCII.GetBytes($"{clientId}:{clientSecret}"))}")
            .WithContent("""{"userId":"testUserId","notificationId":"testNotificationId","secondaryId":"testSecondaryId"}""")
            .Respond(HttpStatusCode.OK);

        NotificationApiServer notificationApiServer = new NotificationApiServer(mockHttp.ToHttpClient(), clientId, clientSecret, secureMode);

        RetractNotificationData retractNotificationData = new RetractNotificationData()
        {
            NotificationId = "testNotificationId",
            UserId = "testUserId",
            SecondaryId = "testSecondaryId"
        };

        // Act
        HttpResponseMessage response = await notificationApiServer.Retract(retractNotificationData);

        // Assert
        Assert.IsTrue(response.IsSuccessStatusCode, "Should be success status code");
    }

    [TestMethod]
    public async Task Identify_ValidDataSecure_ReturnsHttpResponseMessage()
    {
        // Arrange
        const string userId = "testUserId";
        const string clientId = "testClientId";
        const string clientSecret = "testClientSecret";
        const bool secureMode = true;

        MockHttpMessageHandler mockHttp = new MockHttpMessageHandler();

        _ = mockHttp.Expect(HttpMethod.Post, $"https://api.notificationapi.com/testClientId/users/{userId}")
            .WithHeaders("Authorization", $"Basic {Convert.ToBase64String(Encoding.ASCII.GetBytes($"{clientId}:{userId}:{UserIdHasher.Hash(userId, clientSecret)}"))}")
            .WithContent("""{"email":"test@email.com","number":"1234567890","pushTokens":[{"type":"FCM","token":"testToken","device":{"app_id":"testAppId","ad_id":"testAdId","device_id":"testDeviceId","platform":"testPlatform","manufacturer":"testManufacturer","model":"testModel"}}],"webPushTokens":[{"sub":{"endpoint":"testEndpoint","keys":{"p256dh":"testP256dh","auth":"testAuth"}}}]}""")
            .Respond(HttpStatusCode.OK);

        NotificationApiServer notificationApiServer = new NotificationApiServer(mockHttp.ToHttpClient(), clientId, clientSecret, secureMode);

        IdentifyUserData identifyUserData = new IdentifyUserData()
        {
            Email = "test@email.com",
            TelephoneNumber = "1234567890",
            PushTokens =
            [
                new()
                {
                    Device = new()
                    {
                        DeviceId = "testDeviceId",
                        AdId = "testAdId",
                        AppId = "testAppId",
                        Manufacturer = "testManufacturer",
                        Model = "testModel",
                        Platform = "testPlatform"
                    },
                    Token = "testToken",
                    Type = NotificationPushProviders.FCM
                }
            ],
            WebPushTokens =
            [
                new()
                {
                    Sub = new()
                    {
                        Endpoint = "testEndpoint",
                        Keys = new()
                        {
                           Auth = "testAuth",
                           P256dh = "testP256dh"
                        }
                    }
                }
            ]
        };

        // Act
        HttpResponseMessage response = await notificationApiServer.Identify("testUserId", identifyUserData);

        // Assert
        Assert.IsTrue(response.IsSuccessStatusCode, "Should be success status code");
    }

    [TestMethod]
    public async Task Identify_ValidData_ReturnsHttpResponseMessage()
    {
        // Arrange
        const string userId = "testUserId";
        const string clientId = "testClientId";
        const string clientSecret = "testClientSecret";
        const bool secureMode = false;

        MockHttpMessageHandler mockHttp = new MockHttpMessageHandler();

        _ = mockHttp.Expect(HttpMethod.Post, $"https://api.notificationapi.com/testClientId/users/{userId}")
            .WithHeaders("Authorization", $"Basic {Convert.ToBase64String(Encoding.ASCII.GetBytes($"{clientId}:{userId}"))}")
            .WithContent("""{"email":"test@email.com","number":"1234567890","pushTokens":[{"type":"FCM","token":"testToken","device":{"app_id":"testAppId","ad_id":"testAdId","device_id":"testDeviceId","platform":"testPlatform","manufacturer":"testManufacturer","model":"testModel"}}],"webPushTokens":[{"sub":{"endpoint":"testEndpoint","keys":{"p256dh":"testP256dh","auth":"testAuth"}}}]}""")
            .Respond(HttpStatusCode.OK);

        NotificationApiServer notificationApiServer = new NotificationApiServer(mockHttp.ToHttpClient(), clientId, clientSecret, secureMode);

        IdentifyUserData identifyUserData = new IdentifyUserData()
        {
            Email = "test@email.com",
            TelephoneNumber = "1234567890",
            PushTokens =
            [
                new()
                {
                    Device = new()
                    {
                        DeviceId = "testDeviceId",
                        AdId = "testAdId",
                        AppId = "testAppId",
                        Manufacturer = "testManufacturer",
                        Model = "testModel",
                        Platform = "testPlatform"
                    },
                    Token = "testToken",
                    Type = NotificationPushProviders.FCM
                }
            ],
            WebPushTokens =
            [
                new()
                {
                    Sub = new()
                    {
                        Endpoint = "testEndpoint",
                        Keys = new()
                        {
                           Auth = "testAuth",
                           P256dh = "testP256dh"
                        }
                    }
                }
            ]
        };

        // Act
        HttpResponseMessage response = await notificationApiServer.Identify("testUserId", identifyUserData);

        // Assert
        Assert.IsTrue(response.IsSuccessStatusCode, "Should be success status code");
    }

    [TestMethod]
    public async Task SetUserPreferences_ValidData_ReturnsHttpResponseMessage()
    {
        // Arrange
        const string clientId = "testClientId";
        const string clientSecret = "testClientSecret";
        const bool secureMode = false;

        MockHttpMessageHandler mockHttp = new MockHttpMessageHandler();

        _ = mockHttp.Expect(HttpMethod.Post, $"https://api.notificationapi.com/{clientId}/user_preferences/testUserId")
            .WithHeaders("Authorization", $"Basic {Convert.ToBase64String(Encoding.ASCII.GetBytes($"{clientId}:{clientSecret}"))}")
            .WithContent("""[{"notificationId":"testNotificationId","channel":"EMAIL","state":true},{"notificationId":"testNotificationId","channel":"SMS","state":false}]""")
            .Respond(HttpStatusCode.OK);

        NotificationApiServer notificationApiServer = new NotificationApiServer(mockHttp.ToHttpClient(), clientId, clientSecret, secureMode);

        SetUserPreferencesData setUserPreferencesData = new SetUserPreferencesData()
        {
            Preferences =
            [
                new NotificationPreference()
                {
                    Channel = NotificationChannel.EMAIL,
                    State = true,
                    NotificationId = "testNotificationId"
                },
                new NotificationPreference()
                {
                    Channel = NotificationChannel.SMS,
                    State = false,
                    NotificationId = "testNotificationId"
                }
            ]
        };

        // Act
        HttpResponseMessage response = await notificationApiServer.SetUserPreferences("testUserId", setUserPreferencesData);

        // Assert
        Assert.IsTrue(response.IsSuccessStatusCode, "Should be success status code");
    }

    [TestMethod]
    public async Task UpdateSchedule_ValidData_ReturnsHttpResponseMessage()
    {
        // Arrange
        const string clientId = "testClientId";
        const string clientSecret = "testClientSecret";
        const bool secureMode = false;

        MockHttpMessageHandler mockHttp = new MockHttpMessageHandler();

        _ = mockHttp.Expect(HttpMethod.Put, $"https://api.notificationapi.com/{clientId}/schedule/testTrackingId")
            .WithHeaders("Authorization", $"Basic {Convert.ToBase64String(Encoding.ASCII.GetBytes($"{clientId}:{clientSecret}"))}")
            .WithContent("""{"schedule":"2024-03-16T14:36:30Z"}""")
            .Respond(HttpStatusCode.OK);

        NotificationApiServer notificationApiServer = new NotificationApiServer(mockHttp.ToHttpClient(), clientId, clientSecret, secureMode);

        UpdateScheduleData updateScheduleData = new UpdateScheduleData()
        {
            Schedule = DateTime.FromFileTimeUtc(133550733900000000)
        };

        // Act
        HttpResponseMessage response = await notificationApiServer.UpdateSchedule("testTrackingId", updateScheduleData);

        // Assert
        Assert.IsTrue(response.IsSuccessStatusCode, "Should be success status code");
    }

    [TestMethod]
    public async Task DeleteSchedule_ValidData_ReturnsHttpResponseMessage()
    {
        // Arrange
        const string clientId = "testClientId";
        const string clientSecret = "testClientSecret";
        const bool secureMode = false;

        MockHttpMessageHandler mockHttp = new MockHttpMessageHandler();

        _ = mockHttp.Expect(HttpMethod.Delete, $"https://api.notificationapi.com/{clientId}/schedule/testTrackingId")
            .WithHeaders("Authorization", $"Basic {Convert.ToBase64String(Encoding.ASCII.GetBytes($"{clientId}:{clientSecret}"))}")
            .Respond(HttpStatusCode.OK);

        NotificationApiServer notificationApiServer = new NotificationApiServer(mockHttp.ToHttpClient(), clientId, clientSecret, secureMode);

        // Act
        HttpResponseMessage response = await notificationApiServer.DeleteSchedule("testTrackingId");

        // Assert
        Assert.IsTrue(response.IsSuccessStatusCode, "Should be success status code");
    }


    [TestMethod]
    public async Task QueryLogs_ValidData_ReturnsHttpResponseMessage()
    {
        // Arrange
        const string clientId = "testClientId";
        const string clientSecret = "testClientSecret";
        const bool secureMode = false;

        var dateRangeFilter = new DateRangeFilter
        {
            StartTime = 1719600830559,
            EndTime = 1719600840559
        };

        var queryLogsData = new QueryLogsData
        {
            DateRangeFilter = dateRangeFilter,
            NotificationFilter = new List<string> { "order_tracking" },
            ChannelFilter = new List<NotificationChannel> { NotificationChannel.EMAIL },
            UserFilter = new List<string> { "abcd-1234" },
            StatusFilter = new List<string> { "SUCCESS" },
            TrackingIds = new List<string> { "172cf2f4-18cd-4f1f-b2ac-e50c7d71891c" },
            RequestFilter = new List<string> { @"request.mergeTags.item=""Krabby Patty Burger""" },
            EnvIdFilter = new List<string> { "6ok6imq9unr2budgiebjdaa6oi" }
        };

        MockHttpMessageHandler mockHttp = new MockHttpMessageHandler();

        // Use the same serialization settings that the SDK uses
        string expectedJson = JsonSerializer.Serialize(queryLogsData, Configuration.JsonSerializerOptions);

        _ = mockHttp.Expect(HttpMethod.Post, $"https://api.notificationapi.com/{clientId}/logs/query")
            .WithHeaders("Authorization", $"Basic {Convert.ToBase64String(Encoding.ASCII.GetBytes($"{clientId}:{clientSecret}"))}")
            .WithContent(expectedJson)
            .Respond(HttpStatusCode.OK);

        NotificationApiServer notificationApiServer = new NotificationApiServer(mockHttp.ToHttpClient(), clientId, clientSecret, secureMode);

        // Act
        HttpResponseMessage response = await notificationApiServer.QueryLogs(queryLogsData);

        // Assert
        Assert.IsTrue(response.IsSuccessStatusCode, "Should be success status code");
    }

    [TestMethod]
    public async Task UpdateInAppNotification_ValidData_ReturnsHttpResponseMessage()
    {
        // Arrange
        const string userId = "testUserId";
        const string clientId = "testClientId";
        const string clientSecret = "testClientSecret";
        const bool secureMode = true;

        MockHttpMessageHandler mockHttp = new MockHttpMessageHandler();

        _ = mockHttp.Expect(HttpMethod.Patch, $"https://api.notificationapi.com/{clientId}/users/{userId}/notifications/INAPP_WEB")
            .WithHeaders("Authorization", $"Basic {Convert.ToBase64String(Encoding.ASCII.GetBytes($"{clientId}:{userId}:{UserIdHasher.Hash(userId, clientSecret)}"))}")
            .WithContent("""{"trackingIds":["testTrackingId"],"opened":"2024-03-16T14:36:30Z","clicked":"2024-03-16T15:00:00Z"}""")
            .Respond(HttpStatusCode.OK);

        NotificationApiServer notificationApiServer = new NotificationApiServer(mockHttp.ToHttpClient(), clientId, clientSecret, secureMode);

        InAppNotificationPatchData inAppNotificationPatchData = new InAppNotificationPatchData()
        {
            TrackingIds = new List<string> { "testTrackingId" },
            Opened = "2024-03-16T14:36:30Z",
            Clicked = "2024-03-16T15:00:00Z"
        };

        // Act
        HttpResponseMessage response = await notificationApiServer.UpdateInAppNotification(userId, inAppNotificationPatchData);

        // Assert
        Assert.IsTrue(response.IsSuccessStatusCode, "Should be success status code");
    }

    [TestMethod]
    public async Task Send_WithEURegion_UsesCorrectBaseUrl()
    {
        // Arrange
        const string clientId = "testClientId";
        const string clientSecret = "testClientSecret";
        const bool secureMode = false;

        MockHttpMessageHandler mockHttp = new MockHttpMessageHandler();

        _ = mockHttp.Expect(HttpMethod.Post, $"https://api.eu.notificationapi.com/{clientId}/sender")
            .WithHeaders("Authorization", $"Basic {Convert.ToBase64String(Encoding.ASCII.GetBytes($"{clientId}:{clientSecret}"))}")
            .Respond(HttpStatusCode.OK);

        NotificationApiServer notificationApiServer = new NotificationApiServer(mockHttp.ToHttpClient(), clientId, clientSecret, secureMode, NotificationApiServer.EU_BASE_URL);

        SendNotificationData sendNotificationData = new SendNotificationData()
        {
            NotificationId = "testNotificationId",
            User = new()
            {
                Id = "testUserId"
            }
        };

        // Act
        HttpResponseMessage response = await notificationApiServer.Send(sendNotificationData);

        // Assert
        Assert.IsTrue(response.IsSuccessStatusCode, "Should be success status code");
    }

    [TestMethod]
    public async Task Send_WithCARegion_UsesCorrectBaseUrl()
    {
        // Arrange
        const string clientId = "testClientId";
        const string clientSecret = "testClientSecret";
        const bool secureMode = false;

        MockHttpMessageHandler mockHttp = new MockHttpMessageHandler();

        _ = mockHttp.Expect(HttpMethod.Post, $"https://api.ca.notificationapi.com/{clientId}/sender")
            .WithHeaders("Authorization", $"Basic {Convert.ToBase64String(Encoding.ASCII.GetBytes($"{clientId}:{clientSecret}"))}")
            .Respond(HttpStatusCode.OK);

        NotificationApiServer notificationApiServer = new NotificationApiServer(mockHttp.ToHttpClient(), clientId, clientSecret, secureMode, NotificationApiServer.CA_BASE_URL);

        SendNotificationData sendNotificationData = new SendNotificationData()
        {
            NotificationId = "testNotificationId",
            User = new()
            {
                Id = "testUserId"
            }
        };

        // Act
        HttpResponseMessage response = await notificationApiServer.Send(sendNotificationData);

        // Assert
        Assert.IsTrue(response.IsSuccessStatusCode, "Should be success status code");
    }

    [TestMethod]
    public async Task Send_WithDefaultRegion_UsesUSBaseUrl()
    {
        // Arrange
        const string clientId = "testClientId";
        const string clientSecret = "testClientSecret";
        const bool secureMode = false;

        MockHttpMessageHandler mockHttp = new MockHttpMessageHandler();

        // Explicitly check that it's using the US base URL when no region is specified
        _ = mockHttp.Expect(HttpMethod.Post, $"https://api.notificationapi.com/{clientId}/sender")
            .WithHeaders("Authorization", $"Basic {Convert.ToBase64String(Encoding.ASCII.GetBytes($"{clientId}:{clientSecret}"))}")
            .Respond(HttpStatusCode.OK);

        // Don't specify a baseAddress parameter, so it should use the default
        NotificationApiServer notificationApiServer = new NotificationApiServer(mockHttp.ToHttpClient(), clientId, clientSecret, secureMode);

        SendNotificationData sendNotificationData = new SendNotificationData()
        {
            NotificationId = "testNotificationId",
            User = new()
            {
                Id = "testUserId"
            }
        };

        // Act
        HttpResponseMessage response = await notificationApiServer.Send(sendNotificationData);

        // Assert
        Assert.IsTrue(response.IsSuccessStatusCode, "Should be success status code");
    }
}
