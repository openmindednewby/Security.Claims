using System.Security.Claims;

namespace Security.Claims.Claims;

/// <summary>
/// Realm-aware extensions for <see cref="ClaimsPrincipal"/>.
/// Used to enforce hard isolation between products that share a Keycloak install
/// but use different realms (e.g. Questioner realm vs OnlineMenu realm).
/// </summary>
public static class RealmExtensions
{
    private const string KeycloakRealmPathSegment = "/realms/";

    /// <summary>
    /// Returns the raw issuer (<c>iss</c>) claim value from the user's token.
    /// </summary>
    /// <param name="user">The claims principal representing the user.</param>
    /// <returns>The issuer URL if present; otherwise, null.</returns>
    public static string? GetIssuer(this ClaimsPrincipal user) =>
        user.FindFirstValue(OnlineMenuClaimTypes.Iss);

    /// <summary>
    /// Extracts the Keycloak realm name from the issuer claim. The issuer is
    /// expected to follow the Keycloak convention <c>{baseUrl}/realms/{realm}</c>.
    /// Returns null if the issuer is missing or doesn't match the convention.
    /// </summary>
    /// <param name="user">The claims principal representing the user.</param>
    /// <returns>The realm name if extractable; otherwise, null.</returns>
    public static string? GetRealm(this ClaimsPrincipal user)
    {
        var issuer = user.GetIssuer();
        return ExtractRealmFromIssuer(issuer);
    }

    /// <summary>
    /// Verifies that the user's token was issued by the expected realm. Use
    /// this in services that should reject cross-realm tokens — e.g. the
    /// OnlineMenu API rejecting Questioner-realm tokens.
    /// </summary>
    /// <param name="user">The claims principal representing the user.</param>
    /// <param name="expectedRealm">The realm name the service expects (case-insensitive).</param>
    /// <returns>True only if the token's realm matches; otherwise, false.</returns>
    public static bool IsFromRealm(this ClaimsPrincipal user, string expectedRealm)
    {
        if (string.IsNullOrWhiteSpace(expectedRealm))
        {
            return false;
        }

        var realm = user.GetRealm();
        return !string.IsNullOrEmpty(realm)
            && string.Equals(realm, expectedRealm, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Parses a Keycloak issuer URL of the form
    /// <c>{base}/realms/{realm}</c> and returns the <c>{realm}</c> segment.
    /// Returns null for any input that doesn't match the convention.
    /// Public so it can be reused by middleware and authorization handlers
    /// that operate on raw issuer strings instead of <see cref="ClaimsPrincipal"/>.
    /// </summary>
    public static string? ExtractRealmFromIssuer(string? issuer)
    {
        if (string.IsNullOrWhiteSpace(issuer))
        {
            return null;
        }

        var index = issuer.IndexOf(KeycloakRealmPathSegment, StringComparison.OrdinalIgnoreCase);
        if (index < 0)
        {
            return null;
        }

        var start = index + KeycloakRealmPathSegment.Length;
        if (start >= issuer.Length)
        {
            return null;
        }

        var remainder = issuer[start..];

        var separators = new[] { '/', '?', '#' };
        var end = remainder.IndexOfAny(separators);
        var realm = end < 0 ? remainder : remainder[..end];

        return string.IsNullOrWhiteSpace(realm) ? null : realm;
    }
}
