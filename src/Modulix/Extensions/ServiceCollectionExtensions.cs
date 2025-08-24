namespace Modulix;

/// <summary>
/// Provides extension methods for <see cref="IServiceCollection"/> to add imported and exported services.
/// </summary>
internal static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds a service to the collection that resolves from a specified <see cref="IServiceProvider"/>.
    /// </summary>
    /// <param name="services">The service collection to add the service to.</param>
    /// <param name="serviceType">The type of the service to add.</param>
    /// <param name="serviceProvider">The service provider to resolve the service from.</param>
    public static void AddImported(this IServiceCollection services, Type serviceType, IServiceProvider serviceProvider)
    {
        services.AddTransient(serviceType, _ =>
            serviceProvider.GetRequiredService(serviceType)
        );
    }

    /// <summary>
    /// Adds a service to the collection that resolves from a module-specific service provider using a key.
    /// </summary>
    /// <param name="services">The service collection to add the service to.</param>
    /// <param name="serviceType">The type of the service to add.</param>
    /// <param name="moduleServiceKey">The key identifying the module's service provider.</param>
    public static void AddExported(this IServiceCollection services, Type serviceType, object moduleServiceKey)
    {
        services.AddTransient(serviceType, sp =>
            sp.GetRequiredKeyedService<IServiceProvider>(moduleServiceKey).GetRequiredService(serviceType)
        );
    }
}
