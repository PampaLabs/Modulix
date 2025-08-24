namespace Modulix;

/// <summary>
/// Provides extension methods for registering service modules in an <see cref="IServiceCollection"/>.
/// </summary>
public static class RegistrationExtensions
{
    /// <summary>
    /// Adds a service module to the service collection with the specified configuration.
    /// </summary>
    /// <param name="services">The service collection to add the module to.</param>
    /// <param name="optionsAction">An action to configure the module options.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddModule(this IServiceCollection services, Action<ServiceModuleOptionsBuilder> optionsAction)
    {
        var serviceKey = new object();

        var optionsBuilder = new ServiceModuleOptionsBuilder();
        optionsAction.Invoke(optionsBuilder);

        var options = optionsBuilder.Options;

        services.AddKeyedScoped(serviceKey, (sp, _) =>
            CreateModuleServiceProvider(sp, options)
        );

        ExportServices(services, options.Exports, serviceKey);

        return services;
    }

    /// <summary>
    /// Creates a module-specific service provider using the provided options.
    /// </summary>
    /// <param name="serviceProvider">The root service provider.</param>
    /// <param name="options">The module options.</param>
    /// <returns>A service provider for the module.</returns>
    private static IServiceProvider CreateModuleServiceProvider(IServiceProvider serviceProvider, ServiceModuleOptions options)
    {
        var moduleServices = new ServiceCollection();

        ImportServices(moduleServices, options.Imports, serviceProvider);

        options.Registrar(serviceProvider, moduleServices);

        return moduleServices.BuildServiceProvider();
    }

    /// <summary>
    /// Imports the specified service bindings into a service collection.
    /// </summary>
    /// <param name="services">The service collection to import services into.</param>
    /// <param name="bindings">The service bindings to import.</param>
    /// <param name="serviceProvider">The service provider to resolve imported services from.</param>
    private static void ImportServices(IServiceCollection services, IServiceBindingCollection bindings, IServiceProvider serviceProvider)
    {
        foreach (var binding in bindings)
        {
            services.AddImported(binding.ServiceType, serviceProvider);
        }
    }

    /// <summary>
    /// Exports the specified service bindings from a service module.
    /// </summary>
    /// <param name="services">The service collection to add exported services to.</param>
    /// <param name="bindings">The service bindings to export.</param>
    /// <param name="moduleServiceKey">The key identifying the module's service provider.</param>
    private static void ExportServices(IServiceCollection services, IServiceBindingCollection bindings, object moduleServiceKey)
    {
        foreach (var binding in bindings)
        {
            services.AddExported(binding.ServiceType, moduleServiceKey);
        }
    }
}
