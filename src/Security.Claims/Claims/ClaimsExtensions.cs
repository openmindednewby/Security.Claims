using System.Security.Claims;

namespace Security.Claims.Claims;

/// <summary>
/// Internal extension methods for finding claim values.
/// </summary>
internal static class ClaimsExtensions
{
  /// <summary>
  /// Finds the first claim value with the specified claim type.
  /// </summary>
  /// <param name="principal">The claims principal.</param>
  /// <param name="claimType">The type of claim to find.</param>
  /// <returns>The claim value if found; otherwise, null.</returns>
  public static string? FindFirstValue(this ClaimsPrincipal principal, string claimType)
  {
    return principal?.FindFirst(claimType)?.Value;
  }
}
