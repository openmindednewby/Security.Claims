namespace Security.Claims.Claims;

/// <summary>
/// Defines standard claim types used throughout the OnlineMenu application.
/// These claims are typically provided by the identity provider (Keycloak) or added by the application.
/// </summary>
public static class OnlineMenuClaimTypes
{
  /// <summary>
  /// The tenant identifier claim. Used for multi-tenancy support.
  /// </summary>
  public const string TenantId = "tenantId";

  /// <summary>
  /// The user's preferred username from the identity provider.
  /// </summary>
  public const string PreferredUsername = "preferred_username";

  /// <summary>
  /// Indicates whether the user's email has been verified.
  /// </summary>
  public const string EmailVerified = "email_verified";

  /// <summary>
  /// The user's full name.
  /// </summary>
  public const string Name = "name";

  /// <summary>
  /// The user's given (first) name.
  /// </summary>
  public const string GivenName = "given_name";

  /// <summary>
  /// The user's family (last) name.
  /// </summary>
  public const string FamilyName = "family_name";

  /// <summary>
  /// The user's email address.
  /// </summary>
  public const string Email = "email";

  /// <summary>
  /// Custom claim indicating if the user has super user privileges.
  /// </summary>
  public const string IsSuperUser = "IsSuperUser";

  /// <summary>
  /// The subject identifier - unique identifier for the user from the identity provider.
  /// </summary>
  public const string Sub = "sub";

  /// <summary>
  /// The issuer of the token.
  /// </summary>
  public const string Iss = "iss";

  /// <summary>
  /// The authorized party - the party to which the ID token was issued.
  /// </summary>
  public const string Azp = "azp";
}
