using System.Security.Claims;
using Security.Claims.Claims;
using Shouldly;

namespace Security.Claims.Tests.Claims;

/// <summary>
/// Unit tests for <see cref="ClaimsPrincipalExtensions"/>.
/// </summary>
public class ClaimsPrincipalExtensionsTests
{
    [Fact]
    public void GetTenantId_WhenClaimIsValidGuid_ReturnsParsedGuid()
    {
        // Arrange
        var tenantId = Guid.NewGuid();
        var user = BuildPrincipal((OnlineMenuClaimTypes.TenantId, tenantId.ToString()));

        // Act
        var result = user.GetTenantId();

        // Assert
        result.ShouldBe(tenantId);
    }

    [Fact]
    public void GetTenantId_WhenClaimIsNotAGuid_ReturnsNull()
    {
        // Arrange
        var user = BuildPrincipal((OnlineMenuClaimTypes.TenantId, "not-a-guid"));

        // Act
        var result = user.GetTenantId();

        // Assert
        result.ShouldBeNull();
    }

    [Fact]
    public void GetTenantId_WhenClaimAbsent_ReturnsNull()
    {
        // Arrange
        var user = new ClaimsPrincipal(new ClaimsIdentity());

        // Act
        var result = user.GetTenantId();

        // Assert
        result.ShouldBeNull();
    }

    [Fact]
    public void GetPreferredUsername_WhenClaimPresent_ReturnsValue()
    {
        // Arrange
        const string username = "alice";
        var user = BuildPrincipal((OnlineMenuClaimTypes.PreferredUsername, username));

        // Act
        var result = user.GetPreferredUsername();

        // Assert
        result.ShouldBe(username);
    }

    [Fact]
    public void GetPreferredUsername_WhenClaimAbsent_ReturnsNull()
    {
        // Arrange
        var user = new ClaimsPrincipal(new ClaimsIdentity());

        // Act
        var result = user.GetPreferredUsername();

        // Assert
        result.ShouldBeNull();
    }

    [Fact]
    public void GetEmail_WhenClaimPresent_ReturnsValue()
    {
        // Arrange
        const string email = "alice@example.com";
        var user = BuildPrincipal((OnlineMenuClaimTypes.Email, email));

        // Act
        var result = user.GetEmail();

        // Assert
        result.ShouldBe(email);
    }

    [Fact]
    public void GetEmail_WhenClaimAbsent_ReturnsNull()
    {
        // Arrange
        var user = new ClaimsPrincipal(new ClaimsIdentity());

        // Act
        var result = user.GetEmail();

        // Assert
        result.ShouldBeNull();
    }

    [Fact]
    public void GetName_WhenClaimPresent_ReturnsValue()
    {
        // Arrange
        const string fullName = "Alice Liddell";
        var user = BuildPrincipal((OnlineMenuClaimTypes.Name, fullName));

        // Act
        var result = user.GetName();

        // Assert
        result.ShouldBe(fullName);
    }

    [Fact]
    public void GetName_WhenClaimAbsent_ReturnsNull()
    {
        // Arrange
        var user = new ClaimsPrincipal(new ClaimsIdentity());

        // Act
        var result = user.GetName();

        // Assert
        result.ShouldBeNull();
    }

    [Fact]
    public void GetGivenName_WhenClaimPresent_ReturnsValue()
    {
        // Arrange
        const string givenName = "Alice";
        var user = BuildPrincipal((OnlineMenuClaimTypes.GivenName, givenName));

        // Act
        var result = user.GetGivenName();

        // Assert
        result.ShouldBe(givenName);
    }

    [Fact]
    public void GetGivenName_WhenClaimAbsent_ReturnsNull()
    {
        // Arrange
        var user = new ClaimsPrincipal(new ClaimsIdentity());

        // Act
        var result = user.GetGivenName();

        // Assert
        result.ShouldBeNull();
    }

    [Fact]
    public void GetFamilyName_WhenClaimPresent_ReturnsValue()
    {
        // Arrange
        const string familyName = "Liddell";
        var user = BuildPrincipal((OnlineMenuClaimTypes.FamilyName, familyName));

        // Act
        var result = user.GetFamilyName();

        // Assert
        result.ShouldBe(familyName);
    }

    [Fact]
    public void GetFamilyName_WhenClaimAbsent_ReturnsNull()
    {
        // Arrange
        var user = new ClaimsPrincipal(new ClaimsIdentity());

        // Act
        var result = user.GetFamilyName();

        // Assert
        result.ShouldBeNull();
    }

    [Fact]
    public void GetSubjectId_WhenClaimPresent_ReturnsValue()
    {
        // Arrange
        const string subjectId = "abc-123-sub";
        var user = BuildPrincipal((OnlineMenuClaimTypes.Sub, subjectId));

        // Act
        var result = user.GetSubjectId();

        // Assert
        result.ShouldBe(subjectId);
    }

    [Fact]
    public void GetSubjectId_WhenClaimAbsent_ReturnsNull()
    {
        // Arrange
        var user = new ClaimsPrincipal(new ClaimsIdentity());

        // Act
        var result = user.GetSubjectId();

        // Assert
        result.ShouldBeNull();
    }

    [Fact]
    public void IsEmailVerified_WhenClaimIsTrueString_ReturnsTrue()
    {
        // Arrange
        var user = BuildPrincipal((OnlineMenuClaimTypes.EmailVerified, "true"));

        // Act
        var result = user.IsEmailVerified();

        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public void IsEmailVerified_WhenClaimIsFalseString_ReturnsFalse()
    {
        // Arrange
        var user = BuildPrincipal((OnlineMenuClaimTypes.EmailVerified, "false"));

        // Act
        var result = user.IsEmailVerified();

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void IsEmailVerified_WhenClaimNotParseable_ReturnsFalse()
    {
        // Arrange
        var user = BuildPrincipal((OnlineMenuClaimTypes.EmailVerified, "yes"));

        // Act
        var result = user.IsEmailVerified();

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void IsEmailVerified_WhenClaimAbsent_ReturnsFalse()
    {
        // Arrange
        var user = new ClaimsPrincipal(new ClaimsIdentity());

        // Act
        var result = user.IsEmailVerified();

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void IsSuperUser_WhenInSuperUserRole_ReturnsTrue()
    {
        // Arrange
        var user = BuildPrincipalWithRoles(OnlineMenuRoles.SuperUser);

        // Act
        var result = user.IsSuperUser();

        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public void IsSuperUser_WhenNotInSuperUserRole_ReturnsFalse()
    {
        // Arrange
        var user = BuildPrincipalWithRoles(OnlineMenuRoles.User);

        // Act
        var result = user.IsSuperUser();

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void IsAdmin_WhenInAdminRole_ReturnsTrue()
    {
        // Arrange
        var user = BuildPrincipalWithRoles(OnlineMenuRoles.Admin);

        // Act
        var result = user.IsAdmin();

        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public void IsAdmin_WhenNotInAdminRole_ReturnsFalse()
    {
        // Arrange
        var user = BuildPrincipalWithRoles(OnlineMenuRoles.User);

        // Act
        var result = user.IsAdmin();

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void CanManageUsers_WhenInManageUsersRole_ReturnsTrue()
    {
        // Arrange
        var user = BuildPrincipalWithRoles(OnlineMenuRoles.ManageUsers);

        // Act
        var result = user.CanManageUsers();

        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public void CanManageUsers_WhenSuperUser_ReturnsTrue()
    {
        // Arrange — super users always have manage capability.
        var user = BuildPrincipalWithRoles(OnlineMenuRoles.SuperUser);

        // Act
        var result = user.CanManageUsers();

        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public void CanManageUsers_WhenNeitherRolePresent_ReturnsFalse()
    {
        // Arrange
        var user = BuildPrincipalWithRoles(OnlineMenuRoles.User);

        // Act
        var result = user.CanManageUsers();

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void CanViewUsers_WhenInViewUsersRole_ReturnsTrue()
    {
        // Arrange
        var user = BuildPrincipalWithRoles(OnlineMenuRoles.ViewUsers);

        // Act
        var result = user.CanViewUsers();

        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public void CanViewUsers_WhenSuperUser_ReturnsTrue()
    {
        // Arrange
        var user = BuildPrincipalWithRoles(OnlineMenuRoles.SuperUser);

        // Act
        var result = user.CanViewUsers();

        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public void CanViewUsers_WhenNeitherRolePresent_ReturnsFalse()
    {
        // Arrange
        var user = BuildPrincipalWithRoles(OnlineMenuRoles.User);

        // Act
        var result = user.CanViewUsers();

        // Assert
        result.ShouldBeFalse();
    }

    private static ClaimsPrincipal BuildPrincipal(params (string Type, string Value)[] claims)
    {
        var identity = new ClaimsIdentity(claims.Select(c => new Claim(c.Type, c.Value)));
        return new ClaimsPrincipal(identity);
    }

    private static ClaimsPrincipal BuildPrincipalWithRoles(params string[] roles)
    {
        var claims = roles.Select(r => new Claim(ClaimTypes.Role, r));
        var identity = new ClaimsIdentity(claims, authenticationType: "test");
        return new ClaimsPrincipal(identity);
    }
}
