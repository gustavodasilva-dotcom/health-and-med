using FluentAssertions;
using NetArchTest.Rules;

namespace ArchitectureTests;

public sealed class LayerTests : BaseTest
{
    [Fact]
    public void Domain_Should_NotHaveDependencyOnApplication()
    {
        var result = Types.InAssemblies(DomainAssemblies)
            .Should()
            .NotHaveDependencyOn("Application")
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Domain_Should_NotHaveDependencyOnPersistence()
    {
        var result = Types.InAssemblies(DomainAssemblies)
            .Should()
            .NotHaveDependencyOn("Persistence")
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Domain_Should_NotHaveDependencyOnEndpoints()
    {
        var result = Types.InAssemblies(DomainAssemblies)
            .Should()
            .NotHaveDependencyOn("Endpoints")
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Domain_Should_NotHaveDependencyOnCrossCutting()
    {
        var result = Types.InAssemblies(DomainAssemblies)
            .Should()
            .NotHaveDependencyOn("CrossCutting")
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }
}
