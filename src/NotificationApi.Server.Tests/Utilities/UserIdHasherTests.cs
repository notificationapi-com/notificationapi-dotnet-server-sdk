using NotificationApi.Server.Utilities;

namespace NotificationApi.Server.Tests.Utilities;

[TestClass]
public class UserIdHasherTests
{
    [TestMethod]
    public void Hash_ValidUserId_ReturnsHashedUserId()
    {
        // Arrange
        string userId = "testUserId";
        string expectedHashedUserId = "1eQrhzxYitw6K76dI5JT8CBhq9XcxDlU6GK2vaILuBk=";

        // Act
        string actualHashedUserId = UserIdHasher.Hash(userId, "123");

        // Assert
        Assert.AreEqual(expectedHashedUserId, actualHashedUserId);
    }
}