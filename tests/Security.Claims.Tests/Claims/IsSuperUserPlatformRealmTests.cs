using System.Security.Claims;
using Security.Claims.Claims;
using Shouldly;

namespace Security.Claims.Tests.Claims;

/// <summary>
/// Unit tests for the platform-realm overload of <see cref="ClaimsPrincipalExtensions.IsSuperUser(ClaimsPrincipal, string)"/>.
/// </summary>
public class IsSuperUserPlatformRealmTests
{
    [Fact]
    public void IsSuperUser_WithTenantRealmIssuerAndSuperUserRole_ReturnsFalse()
    {
        var user = BuildPrincipal("https://auth.example.com/realms/tenant-acme", OnlineMenuRoles.SuperUser);

        var result = user.IsSuperUser("platform");

        result.ShouldBeFalse();
    }

    [Fact]
    public void IsSuperUser_WithPlatformRealmIssuerAndSuperUserRole_ReturnsTrue()
    {
        var user = BuildPrincipal("https://auth.example.com/realms/platform", OnlineMenuRoles.SuperUser);

        var result = user.IsSuperUser("platform");

        result.ShouldBeTrue();
    }

    [Fact]
    public void IsSuperUser_WithEmptyPlatformRealm_ReturnsFalse()
    {
        var user = BuildPrincipal("https://auth.example.com/realms/platform", OnlineMenuRoles.SuperUser);

        var result = user.IsSuperUser(string.Empty);

        result.ShouldBeFalse();
    }

    [Fact]
    public void IsSuperUser_WithPlatformRealmIssuerWithoutSuperUserRole_ReturnsFalse()
    {
        var user = BuildPrincipal("https://auth.example.com/realms/platform", OnlineMenuRoles.User);

        var result = user.IsSuperUser("platform");

        result.ShouldBeFalse();
    }

    [Fact]
    public void IsSuperUser_WithoutIssuerAndSuperUserRole_ReturnsFalse()
    {
        var identity = new ClaimsIdentity([new Claim(ClaimTypes.Role, OnlineMenuRoles.SuperUser)], authenticationType: "test");
        var user = new ClaimsPrincipal(identity);

        var result = user.IsSuperUser("platform");

        result.ShouldBeFalse();
    }

    private static ClaimsPrincipal BuildPrincipal(string issuer, string role)
    {
        var claims = new[] { new Claim(OnlineMenuClaimTypes.Iss, issuer), new Claim(ClaimTypes.Role, role) };
        return new ClaimsPrincipal(new ClaimsIdentity(claims, authenticationType: "test"));
    }
}
