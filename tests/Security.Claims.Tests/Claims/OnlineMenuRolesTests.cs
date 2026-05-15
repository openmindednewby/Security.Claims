using Security.Claims.Claims;
using Shouldly;

namespace Security.Claims.Tests.Claims;

/// <summary>
/// Unit tests for <see cref="OnlineMenuRoles"/> constants.
/// Verifies the wire-level role names — these must stay in sync with the
/// Keycloak realm role definitions.
/// </summary>
public class OnlineMenuRolesTests
{
    [Fact]
    public void SuperUser_Constant_EqualsSuperUser()
    {
        OnlineMenuRoles.SuperUser.ShouldBe("superUser");
    }

    [Fact]
    public void Admin_Constant_EqualsAdmin()
    {
        OnlineMenuRoles.Admin.ShouldBe("admin");
    }

    [Fact]
    public void User_Constant_EqualsUser()
    {
        OnlineMenuRoles.User.ShouldBe("user");
    }

    [Fact]
    public void UmaAuthorization_Constant_EqualsUmaAuthorization()
    {
        OnlineMenuRoles.UmaAuthorization.ShouldBe("uma_authorization");
    }

    [Fact]
    public void OfflineAccess_Constant_EqualsOfflineAccess()
    {
        OnlineMenuRoles.OfflineAccess.ShouldBe("offline_access");
    }

    [Fact]
    public void DefaultRoles_Constant_EqualsDefaultRolesOnlinemenu()
    {
        OnlineMenuRoles.DefaultRoles.ShouldBe("default-roles-onlinemenu");
    }

    [Fact]
    public void ManageUsers_Constant_EqualsManageUsers()
    {
        OnlineMenuRoles.ManageUsers.ShouldBe("manage-users");
    }

    [Fact]
    public void ViewUsers_Constant_EqualsViewUsers()
    {
        OnlineMenuRoles.ViewUsers.ShouldBe("view-users");
    }

    [Fact]
    public void QueryUsers_Constant_EqualsQueryUsers()
    {
        OnlineMenuRoles.QueryUsers.ShouldBe("query-users");
    }

    [Fact]
    public void QueryGroups_Constant_EqualsQueryGroups()
    {
        OnlineMenuRoles.QueryGroups.ShouldBe("query-groups");
    }
}
