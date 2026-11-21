using System.Security.Claims;
using Security.Claims.Claims;
using Shouldly;

namespace Security.Claims.Tests.Claims;

/// <summary>
/// Unit tests for the platform-realm overloads of CanManageUsers and CanViewUsers.
/// </summary>
public class UserCapabilityPlatformRealmTests
{
    [Fact]
    public void CanManageUsers_WithTenantRealmIssuerAndSuperUserRole_ReturnsFalse()
    {
        var user = BuildPrincipal("https://auth.example.com/realms/tenant-acme", OnlineMenuRoles.SuperUser);

        var result = user.CanManageUsers("platform");

        result.ShouldBeFalse();
    }

    [Fact]
    public void CanManageUsers_WithPlatformRealmIssuerAndSuperUserRole_ReturnsTrue()
    {
        var user = BuildPrincipal("https://auth.example.com/realms/platform", OnlineMenuRoles.SuperUser);

        var result = user.CanManageUsers("platform");

        result.ShouldBeTrue();
    }

    [Fact]
    public void CanManageUsers_WithEmptyPlatformRealmAndSuperUserRole_ReturnsFalse()
    {
        var user = BuildPrincipal("https://auth.example.com/realms/platform", OnlineMenuRoles.SuperUser);

        var result = user.CanManageUsers(string.Empty);

        result.ShouldBeFalse();
    }

    [Fact]
    public void CanManageUsers_WithTenantRealmIssuerAndManageUsersRole_ReturnsTrue()
    {
        var user = BuildPrincipal("https://auth.example.com/realms/tenant-acme", OnlineMenuRoles.ManageUsers);

        var result = user.CanManageUsers("platform");

        result.ShouldBeTrue();
    }

    [Fact]
    public void CanViewUsers_WithTenantRealmIssuerAndSuperUserRole_ReturnsFalse()
    {
        var user = BuildPrincipal("https://auth.example.com/realms/tenant-acme", OnlineMenuRoles.SuperUser);

        var result = user.CanViewUsers("platform");

        result.ShouldBeFalse();
    }

    [Fact]
    public void CanViewUsers_WithPlatformRealmIssuerAndSuperUserRole_ReturnsTrue()
    {
        var user = BuildPrincipal("https://auth.example.com/realms/platform", OnlineMenuRoles.SuperUser);

        var result = user.CanViewUsers("platform");

        result.ShouldBeTrue();
    }

    [Fact]
    public void CanViewUsers_WithEmptyPlatformRealmAndSuperUserRole_ReturnsFalse()
    {
        var user = BuildPrincipal("https://auth.example.com/realms/platform", OnlineMenuRoles.SuperUser);

        var result = user.CanViewUsers(string.Empty);

        result.ShouldBeFalse();
    }

    [Fact]
    public void CanViewUsers_WithTenantRealmIssuerAndViewUsersRole_ReturnsTrue()
    {
        var user = BuildPrincipal("https://auth.example.com/realms/tenant-acme", OnlineMenuRoles.ViewUsers);

        var result = user.CanViewUsers("platform");

        result.ShouldBeTrue();
    }

    private static ClaimsPrincipal BuildPrincipal(string issuer, string role)
    {
        var claims = new[] { new Claim(OnlineMenuClaimTypes.Iss, issuer), new Claim(ClaimTypes.Role, role) };
        return new ClaimsPrincipal(new ClaimsIdentity(claims, authenticationType: "test"));
    }
}
