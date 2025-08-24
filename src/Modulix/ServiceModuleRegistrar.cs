namespace Modulix;

/// <summary>
/// Represents a method that registers services in a service module.
/// </summary>
/// <param name="serviceProvider">The service provider used to resolve dependencies.</param>
/// <param name="services">The collection of services to register into.</param>
public delegate void ServiceModuleRegistrar(IServiceProvider serviceProvider, IServiceCollection services);
