using System.Security.Claims;

namespace OnlineMenu.Security.Claims;

/// <summary>
/// Extension methods for ClaimsPrincipal to easily access OnlineMenu-specific claims.
/// </summary>
public static class ClaimsPrincipalExtensions
{
  /// <summary>
  /// Gets the tenant ID from the user's claims.
  /// </summary>
  /// <param name="user">The claims principal representing the user.</param>
  /// <returns>The tenant ID if found and valid; otherwise, null.</returns>
  public static Guid? GetTenantId(this ClaimsPrincipal user)
  {
    var value = user.FindFirstValue(OnlineMenuClaimTypes.TenantId);
    return Guid.TryParse(value, out var id) ? id : null;
  }

  /// <summary>
  /// Gets the user's preferred username.
  /// </summary>
  /// <param name="user">The claims principal representing the user.</param>
  /// <returns>The preferred username if found; otherwise, null.</returns>
  public static string? GetPreferredUsername(this ClaimsPrincipal user) =>
    user.FindFirstValue(OnlineMenuClaimTypes.PreferredUsername);

  /// <summary>
  /// Gets the user's email address.
  /// </summary>
  /// <param name="user">The claims principal representing the user.</param>
  /// <returns>The email address if found; otherwise, null.</returns>
  public static string? GetEmail(this ClaimsPrincipal user) =>
    user.FindFirstValue(OnlineMenuClaimTypes.Email);

  /// <summary>
  /// Gets the user's full name.
  /// </summary>
  /// <param name="user">The claims principal representing the user.</param>
  /// <returns>The full name if found; otherwise, null.</returns>
  public static string? GetName(this ClaimsPrincipal user) =>
    user.FindFirstValue(OnlineMenuClaimTypes.Name);

  /// <summary>
  /// Gets the user's given (first) name.
  /// </summary>
  /// <param name="user">The claims principal representing the user.</param>
  /// <returns>The given name if found; otherwise, null.</returns>
  public static string? GetGivenName(this ClaimsPrincipal user) =>
    user.FindFirstValue(OnlineMenuClaimTypes.GivenName);

  /// <summary>
  /// Gets the user's family (last) name.
  /// </summary>
  /// <param name="user">The claims principal representing the user.</param>
  /// <returns>The family name if found; otherwise, null.</returns>
  public static string? GetFamilyName(this ClaimsPrincipal user) =>
    user.FindFirstValue(OnlineMenuClaimTypes.FamilyName);

  /// <summary>
  /// Gets the user's subject identifier (unique ID from identity provider).
  /// </summary>
  /// <param name="user">The claims principal representing the user.</param>
  /// <returns>The subject ID if found; otherwise, null.</returns>
  public static string? GetSubjectId(this ClaimsPrincipal user) =>
    user.FindFirstValue(OnlineMenuClaimTypes.Sub);

  /// <summary>
  /// Checks if the user's email has been verified.
  /// </summary>
  /// <param name="user">The claims principal representing the user.</param>
  /// <returns>True if the email is verified; otherwise, false.</returns>
  public static bool IsEmailVerified(this ClaimsPrincipal user) =>
    bool.TryParse(
      user.FindFirstValue(OnlineMenuClaimTypes.EmailVerified),
      out var result
    ) && result;

  /// <summary>
  /// Checks if the user has super user privileges.
  /// </summary>
  /// <param name="user">The claims principal representing the user.</param>
  /// <returns>True if the user is a super user; otherwise, false.</returns>
  public static bool IsSuperUser(this ClaimsPrincipal user) =>
    user.IsInRole(OnlineMenuRoles.SuperUser);

  /// <summary>
  /// Checks if the user has admin privileges.
  /// </summary>
  /// <param name="user">The claims principal representing the user.</param>
  /// <returns>True if the user is an admin; otherwise, false.</returns>
  public static bool IsAdmin(this ClaimsPrincipal user) =>
    user.IsInRole(OnlineMenuRoles.Admin);

  /// <summary>
  /// Checks if the user has manage users capability.
  /// </summary>
  /// <param name="user">The claims principal representing the user.</param>
  /// <returns>True if the user can manage users; otherwise, false.</returns>
  public static bool CanManageUsers(this ClaimsPrincipal user) =>
    user.IsInRole(OnlineMenuRoles.ManageUsers) || user.IsSuperUser();

  /// <summary>
  /// Checks if the user has view users capability.
  /// </summary>
  /// <param name="user">The claims principal representing the user.</param>
  /// <returns>True if the user can view users; otherwise, false.</returns>
  public static bool CanViewUsers(this ClaimsPrincipal user) =>
    user.IsInRole(OnlineMenuRoles.ViewUsers) || user.IsSuperUser();
}
