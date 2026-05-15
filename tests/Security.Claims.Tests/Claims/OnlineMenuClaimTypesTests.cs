using Security.Claims.Claims;
using Shouldly;

namespace Security.Claims.Tests.Claims;

/// <summary>
/// Unit tests for <see cref="OnlineMenuClaimTypes"/> constants.
/// Verifies that the wire-level claim names remain stable across releases —
/// changing any of these is a breaking change for token consumers.
/// </summary>
public class OnlineMenuClaimTypesTests
{
    [Fact]
    public void TenantId_Constant_EqualsTenantId()
    {
        OnlineMenuClaimTypes.TenantId.ShouldBe("tenantId");
    }

    [Fact]
    public void PreferredUsername_Constant_EqualsPreferredUsername()
    {
        OnlineMenuClaimTypes.PreferredUsername.ShouldBe("preferred_username");
    }

    [Fact]
    public void EmailVerified_Constant_EqualsEmailVerified()
    {
        OnlineMenuClaimTypes.EmailVerified.ShouldBe("email_verified");
    }

    [Fact]
    public void Name_Constant_EqualsName()
    {
        OnlineMenuClaimTypes.Name.ShouldBe("name");
    }

    [Fact]
    public void GivenName_Constant_EqualsGivenName()
    {
        OnlineMenuClaimTypes.GivenName.ShouldBe("given_name");
    }

    [Fact]
    public void FamilyName_Constant_EqualsFamilyName()
    {
        OnlineMenuClaimTypes.FamilyName.ShouldBe("family_name");
    }

    [Fact]
    public void Email_Constant_EqualsEmail()
    {
        OnlineMenuClaimTypes.Email.ShouldBe("email");
    }

    [Fact]
    public void IsSuperUser_Constant_EqualsIsSuperUser()
    {
        OnlineMenuClaimTypes.IsSuperUser.ShouldBe("IsSuperUser");
    }

    [Fact]
    public void Sub_Constant_EqualsSub()
    {
        OnlineMenuClaimTypes.Sub.ShouldBe("sub");
    }

    [Fact]
    public void Iss_Constant_EqualsIss()
    {
        OnlineMenuClaimTypes.Iss.ShouldBe("iss");
    }

    [Fact]
    public void Azp_Constant_EqualsAzp()
    {
        OnlineMenuClaimTypes.Azp.ShouldBe("azp");
    }
}
