using System.Security.Claims;
using Security.Claims.Claims;
using Shouldly;

namespace Security.Claims.Tests.Claims;

/// <summary>
/// Unit tests for <see cref="RealmExtensions"/>.
/// </summary>
public class RealmExtensionsTests
{
    private const string OnlineMenuRealm = "onlinemenu";
    private const string QuestionerRealm = "questioner";
    private const string KeycloakBase = "https://kc.example.com";

    [Fact]
    public void GetIssuer_WhenIssuerClaimPresent_ReturnsValue()
    {
        // Arrange
        var issuer = $"{KeycloakBase}/realms/{OnlineMenuRealm}";
        var user = BuildPrincipal(OnlineMenuClaimTypes.Iss, issuer);

        // Act
        var result = user.GetIssuer();

        // Assert
        result.ShouldBe(issuer);
    }

    [Fact]
    public void GetIssuer_WhenIssuerClaimAbsent_ReturnsNull()
    {
        // Arrange
        var user = new ClaimsPrincipal(new ClaimsIdentity());

        // Act
        var result = user.GetIssuer();

        // Assert
        result.ShouldBeNull();
    }

    [Fact]
    public void GetRealm_WhenIssuerClaimAbsent_ReturnsNull()
    {
        // Arrange
        var user = new ClaimsPrincipal(new ClaimsIdentity());

        // Act
        var result = user.GetRealm();

        // Assert
        result.ShouldBeNull();
    }

    [Fact]
    public void GetRealm_WhenIssuerWellFormed_ReturnsRealmSegment()
    {
        // Arrange
        var user = BuildPrincipal(
            OnlineMenuClaimTypes.Iss,
            $"{KeycloakBase}/realms/{OnlineMenuRealm}");

        // Act
        var result = user.GetRealm();

        // Assert
        result.ShouldBe(OnlineMenuRealm);
    }

    [Fact]
    public void GetRealm_WhenIssuerMalformed_ReturnsNull()
    {
        // Arrange
        var user = BuildPrincipal(
            OnlineMenuClaimTypes.Iss,
            "not-a-valid-issuer-url");

        // Act
        var result = user.GetRealm();

        // Assert
        result.ShouldBeNull();
    }

    [Fact]
    public void IsFromRealm_WhenExpectedRealmIsNull_ReturnsFalse()
    {
        // Arrange
        var user = BuildPrincipal(
            OnlineMenuClaimTypes.Iss,
            $"{KeycloakBase}/realms/{OnlineMenuRealm}");

        // Act
        var result = user.IsFromRealm(null!);

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void IsFromRealm_WhenExpectedRealmIsEmpty_ReturnsFalse()
    {
        // Arrange
        var user = BuildPrincipal(
            OnlineMenuClaimTypes.Iss,
            $"{KeycloakBase}/realms/{OnlineMenuRealm}");

        // Act
        var result = user.IsFromRealm(string.Empty);

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void IsFromRealm_WhenExpectedRealmIsWhitespace_ReturnsFalse()
    {
        // Arrange
        var user = BuildPrincipal(
            OnlineMenuClaimTypes.Iss,
            $"{KeycloakBase}/realms/{OnlineMenuRealm}");

        // Act
        var result = user.IsFromRealm("   ");

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void IsFromRealm_WhenIssuerAbsent_ReturnsFalse()
    {
        // Arrange
        var user = new ClaimsPrincipal(new ClaimsIdentity());

        // Act
        var result = user.IsFromRealm(OnlineMenuRealm);

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void IsFromRealm_WhenRealmsMatchExactly_ReturnsTrue()
    {
        // Arrange
        var user = BuildPrincipal(
            OnlineMenuClaimTypes.Iss,
            $"{KeycloakBase}/realms/{OnlineMenuRealm}");

        // Act
        var result = user.IsFromRealm(OnlineMenuRealm);

        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public void IsFromRealm_WhenRealmsMatchCaseInsensitively_ReturnsTrue()
    {
        // Arrange
        var user = BuildPrincipal(
            OnlineMenuClaimTypes.Iss,
            $"{KeycloakBase}/realms/{OnlineMenuRealm}");

        // Act
        var result = user.IsFromRealm("ONLINEMENU");

        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public void IsFromRealm_WhenRealmsMismatch_ReturnsFalse()
    {
        // Arrange
        var user = BuildPrincipal(
            OnlineMenuClaimTypes.Iss,
            $"{KeycloakBase}/realms/{QuestionerRealm}");

        // Act
        var result = user.IsFromRealm(OnlineMenuRealm);

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void ExtractRealmFromIssuer_WhenStandardKeycloakUrl_ReturnsRealmName()
    {
        // Act
        var result = RealmExtensions.ExtractRealmFromIssuer(
            $"{KeycloakBase}/realms/{OnlineMenuRealm}");

        // Assert
        result.ShouldBe(OnlineMenuRealm);
    }

    [Fact]
    public void ExtractRealmFromIssuer_WhenTrailingSlash_TrimsAndReturnsRealmName()
    {
        // Act
        var result = RealmExtensions.ExtractRealmFromIssuer(
            $"{KeycloakBase}/realms/{OnlineMenuRealm}/");

        // Assert
        result.ShouldBe(OnlineMenuRealm);
    }

    [Fact]
    public void ExtractRealmFromIssuer_WhenQueryStringPresent_ReturnsRealmName()
    {
        // Act
        var result = RealmExtensions.ExtractRealmFromIssuer(
            $"{KeycloakBase}/realms/{OnlineMenuRealm}?foo=bar");

        // Assert
        result.ShouldBe(OnlineMenuRealm);
    }

    [Fact]
    public void ExtractRealmFromIssuer_WhenFragmentPresent_ReturnsRealmName()
    {
        // Act
        var result = RealmExtensions.ExtractRealmFromIssuer(
            $"{KeycloakBase}/realms/{OnlineMenuRealm}#section");

        // Assert
        result.ShouldBe(OnlineMenuRealm);
    }

    [Fact]
    public void ExtractRealmFromIssuer_WhenIssuerNull_ReturnsNull()
    {
        // Act
        var result = RealmExtensions.ExtractRealmFromIssuer(null);

        // Assert
        result.ShouldBeNull();
    }

    [Fact]
    public void ExtractRealmFromIssuer_WhenIssuerEmpty_ReturnsNull()
    {
        // Act
        var result = RealmExtensions.ExtractRealmFromIssuer(string.Empty);

        // Assert
        result.ShouldBeNull();
    }

    [Fact]
    public void ExtractRealmFromIssuer_WhenIssuerWhitespace_ReturnsNull()
    {
        // Act
        var result = RealmExtensions.ExtractRealmFromIssuer("   ");

        // Assert
        result.ShouldBeNull();
    }

    [Fact]
    public void ExtractRealmFromIssuer_WhenNoRealmsSegment_ReturnsNull()
    {
        // Act
        var result = RealmExtensions.ExtractRealmFromIssuer($"{KeycloakBase}/auth");

        // Assert
        result.ShouldBeNull();
    }

    [Fact]
    public void ExtractRealmFromIssuer_WhenRealmsSegmentEndsString_ReturnsNull()
    {
        // Arrange — `.../realms/` with no realm name following it.
        var issuer = $"{KeycloakBase}/realms/";

        // Act
        var result = RealmExtensions.ExtractRealmFromIssuer(issuer);

        // Assert
        result.ShouldBeNull();
    }

    [Fact]
    public void ExtractRealmFromIssuer_WhenRealmsSegmentFollowedByWhitespace_ReturnsNull()
    {
        // Act
        var result = RealmExtensions.ExtractRealmFromIssuer($"{KeycloakBase}/realms/   ");

        // Assert
        result.ShouldBeNull();
    }

    [Fact]
    public void ExtractRealmFromIssuer_WhenRealmsSegmentFollowedBySlashOnly_ReturnsNull()
    {
        // Arrange — `.../realms//` with empty realm name between slashes.
        var issuer = $"{KeycloakBase}/realms//other";

        // Act
        var result = RealmExtensions.ExtractRealmFromIssuer(issuer);

        // Assert
        result.ShouldBeNull();
    }

    [Fact]
    public void ExtractRealmFromIssuer_WhenSegmentMatchedCaseInsensitively_ReturnsRealmName()
    {
        // Act — Keycloak path is case-insensitive in some deployments.
        var result = RealmExtensions.ExtractRealmFromIssuer(
            $"{KeycloakBase}/REALMS/{OnlineMenuRealm}");

        // Assert
        result.ShouldBe(OnlineMenuRealm);
    }

    private static ClaimsPrincipal BuildPrincipal(string claimType, string claimValue)
    {
        var identity = new ClaimsIdentity(new[] { new Claim(claimType, claimValue) });
        return new ClaimsPrincipal(identity);
    }
}
