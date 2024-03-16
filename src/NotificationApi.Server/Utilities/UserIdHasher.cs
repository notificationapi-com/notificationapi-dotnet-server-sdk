using System.Security.Cryptography;
using System.Text;

namespace NotificationApi.Server.Utilities;

/// <summary>
/// Provides methods for hashing user IDs.
/// </summary>
public static class UserIdHasher
{
    /// <summary>
    /// Hashes the specified user ID using the provided client secret.
    /// </summary>
    /// <param name="userId">The user ID to hash.</param>
    /// <param name="clientSecret">The client secret used for hashing.</param>
    /// <returns>The hashed user ID.</returns>
    public static string Hash(string userId, string clientSecret)
    {
        using HMACSHA256 hmac = new HMACSHA256(Encoding.ASCII.GetBytes(clientSecret));
        return Convert.ToBase64String(hmac.ComputeHash(Encoding.ASCII.GetBytes(userId)));
    }
}