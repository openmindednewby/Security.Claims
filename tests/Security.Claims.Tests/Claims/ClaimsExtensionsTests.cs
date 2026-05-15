using System.Reflection;
using System.Security.Claims;
using Shouldly;

namespace Security.Claims.Tests.Claims;

/// <summary>
/// Unit tests for the internal <c>ClaimsExtensions.FindFirstValue</c> helper.
/// Reached via reflection because the type is <c>internal</c> and the package
/// does not expose <c>InternalsVisibleTo</c>; we still want explicit branch
/// coverage on its defensive null-handling.
/// </summary>
public class ClaimsExtensionsTests
{
    private static readonly MethodInfo FindFirstValueMethod = ResolveFindFirstValue();

    [Fact]
    public void FindFirstValue_WhenPrincipalIsNull_ReturnsNull()
    {
        // Arrange — extension methods compile down to static methods that can
        // legitimately receive a null `this`.
        ClaimsPrincipal? principal = null;

        // Act
        var result = (string?)FindFirstValueMethod.Invoke(null, new object?[] { principal, "anyType" });

        // Assert
        result.ShouldBeNull();
    }

    [Fact]
    public void FindFirstValue_WhenClaimAbsent_ReturnsNull()
    {
        // Arrange
        var principal = new ClaimsPrincipal(new ClaimsIdentity());

        // Act
        var result = (string?)FindFirstValueMethod.Invoke(null, new object?[] { principal, "missing" });

        // Assert
        result.ShouldBeNull();
    }

    [Fact]
    public void FindFirstValue_WhenClaimPresent_ReturnsValue()
    {
        // Arrange
        const string claimType = "test-type";
        const string claimValue = "test-value";
        var principal = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(claimType, claimValue),
        }));

        // Act
        var result = (string?)FindFirstValueMethod.Invoke(null, new object?[] { principal, claimType });

        // Assert
        result.ShouldBe(claimValue);
    }

    private static MethodInfo ResolveFindFirstValue()
    {
        var assembly = typeof(global::Security.Claims.Claims.OnlineMenuClaimTypes).Assembly;
        var type = assembly.GetType("Security.Claims.Claims.ClaimsExtensions", throwOnError: true)!;
        var method = type.GetMethod(
            "FindFirstValue",
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
        return method ?? throw new MissingMethodException("Security.Claims.Claims.ClaimsExtensions", "FindFirstValue");
    }
}
