using System.Reflection;
using Common.Shared.Abstractions;
using FluentAssertions;
using NetArchTest.Rules;

namespace ArchitectureTests.Domain;

public sealed class DomainTests : BaseTest
{
    [Fact]
    public void Entities_Should_BeSealed()
    {
        var result = Types.InAssemblies(DomainAssemblies)
            .That()
            .Inherit(typeof(BaseEntity))
            .Should()
            .BeSealed()
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Enums_Should_HavePluralNames()
    {
        var enumTypes = Types.InAssemblies(DomainAssemblies)
            .GetTypes()
            .Where(type => type.IsEnum);

        var result = enumTypes
            .Where(type => !type.Name.EndsWith("s", StringComparison.OrdinalIgnoreCase))
            .Any();

        result.Should().BeFalse();
    }
}
