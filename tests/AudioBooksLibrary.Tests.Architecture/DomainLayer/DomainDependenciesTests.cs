using FluentAssertions;
using NetArchTest.Rules;
using Xunit;

namespace AudioBooksLibrary.Tests.Architecture.DomainLayer;

public class DomainDependenciesTests
{
    [Fact]
    public void DomainLayer_ShouldNotHaveDependencyOn_ApplicationLayer()
    {
        Types.InAssembly(Core.AssemblyReference.Assembly)
            .Should()
            .NotHaveDependencyOn(Application.AssemblyReference.Assembly.GetName().Name)
            .GetResult().IsSuccessful.Should().BeTrue();
    }
    
    [Fact]
    public void DomainLayer_ShouldNotHaveDependencyOn_PersistenceLayer()
    {
        Types.InAssembly(Core.AssemblyReference.Assembly)
            .Should()
            .NotHaveDependencyOn(Infrastructure.AssemblyReference.Assembly.GetName().Name)
            .GetResult().IsSuccessful.Should().BeTrue();
    }
    
    [Fact]
    public void DomainLayer_ShouldNotHaveDependencyOn_ApiLayer()
    {
        Types.InAssembly(Core.AssemblyReference.Assembly)
            .Should()
            .NotHaveDependencyOn(Api.AssemblyReference.Assembly.GetName().Name)
            .GetResult().IsSuccessful.Should().BeTrue();
    }
}