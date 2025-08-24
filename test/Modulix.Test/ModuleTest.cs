using Modulix.Test.Fixture;

using Xunit;

namespace Modulix.Test;

public class ModuleTest
{
    [Fact]
    public void BuildProvider_WithModule_ResolvesOnlyBoundService()
    {
        var services = new ServiceCollection();

        services.AddScoped<GlobalService>();

        services.AddModule(options =>
        {
            options
                .ConfigureServices((sp, services) =>
                {
                    services.AddScoped<ExposedService>();
                    services.AddScoped<HiddenService>();
                    services.AddScoped<ChildService>();
                })
                .Imports(imports =>
                {
                    imports.AddBinding<GlobalService>();
                })
                .Exports(exports =>
                {
                    exports.AddBinding<ExposedService>();
                });
        });

        using var serviceProvider = services.BuildServiceProvider();

        var exposedService = serviceProvider.GetService<ExposedService>();
        var hiddenService = serviceProvider.GetService<HiddenService>();
        var childService = serviceProvider.GetService<ChildService>();

        exposedService.Should().NotBeNull();
        hiddenService.Should().BeNull();
        childService.Should().BeNull();
    }

    [Fact]
    public void GetService_SameScope_ReturnsSameInstance()
    {
        var services = new ServiceCollection();

        services.AddScoped<GlobalService>();

        services.AddModule(options =>
        {
            options
                .ConfigureServices((sp, services) =>
                {
                    services.AddScoped<ExposedService>();
                    services.AddScoped<HiddenService>();
                    services.AddScoped<ChildService>();
                })
                .Imports(imports =>
                {
                    imports.AddBinding<GlobalService>();
                })
                .Exports(exports =>
                {
                    exports.AddBinding<ExposedService>();
                });
        });

        using var serviceProvider = services.BuildServiceProvider();

        using var scope = serviceProvider.CreateScope();

        var scopedInstance1 = scope.ServiceProvider.GetService<ExposedService>();
        var scopedInstance2 = scope.ServiceProvider.GetService<ExposedService>();

        scopedInstance1.Should().NotBeNull();
        scopedInstance2.Should().NotBeNull();

        scopedInstance1.Should().Be(scopedInstance2);
    }

    [Fact]
    public void GetService_DifferentScopes_ReturnsDifferentInstances()
    {
        var services = new ServiceCollection();

        services.AddScoped<GlobalService>();

        services.AddModule(options =>
        {
            options
                .ConfigureServices((sp, services) =>
                {
                    services.AddScoped<ExposedService>();
                    services.AddScoped<HiddenService>();
                    services.AddScoped<ChildService>();
                })
                .Imports(imports =>
                {
                    imports.AddBinding<GlobalService>();
                })
                .Exports(exports =>
                {
                    exports.AddBinding<ExposedService>();
                });
        });

        using var serviceProvider = services.BuildServiceProvider();

        var rootInstance = serviceProvider.GetService<ExposedService>();

        using var scope = serviceProvider.CreateScope();

        var scopedInstance = scope.ServiceProvider.GetService<ExposedService>();

        rootInstance.Should().NotBeNull();
        scopedInstance.Should().NotBeNull();

        rootInstance.Should().NotBe(scopedInstance);
    }
}
