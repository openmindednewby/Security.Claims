namespace OnlineMenu.Security.Claims;

/// <summary>
/// Defines standard roles used throughout the OnlineMenu application.
/// These roles are typically managed by the identity provider (Keycloak).
/// </summary>
public static class OnlineMenuRoles
{
  /// <summary>
  /// Super user role with full system access and user management capabilities.
  /// </summary>
  public const string SuperUser = "superUser";

  /// <summary>
  /// Administrator role with elevated privileges.
  /// </summary>
  public const string Admin = "admin";

  /// <summary>
  /// Standard user role with basic access privileges.
  /// </summary>
  public const string User = "user";

  /// <summary>
  /// UMA (User-Managed Access) authorization role from Keycloak.
  /// </summary>
  public const string UmaAuthorization = "uma_authorization";

  /// <summary>
  /// Offline access role - allows refresh tokens to work when user is offline.
  /// </summary>
  public const string OfflineAccess = "offline_access";

  /// <summary>
  /// Default roles assigned to all users in the OnlineMenu realm.
  /// </summary>
  public const string DefaultRoles = "default-roles-onlinemenu";

  /// <summary>
  /// Realm management - manage users capability.
  /// </summary>
  public const string ManageUsers = "manage-users";

  /// <summary>
  /// Realm management - view users capability.
  /// </summary>
  public const string ViewUsers = "view-users";

  /// <summary>
  /// Realm management - query users capability.
  /// </summary>
  public const string QueryUsers = "query-users";

  /// <summary>
  /// Realm management - query groups capability.
  /// </summary>
  public const string QueryGroups = "query-groups";
}
