namespace Modulix;

/// <summary>
/// Defines a contract for configuring application services.
/// </summary>
public interface IServiceConfiguration
{
    /// <summary>
    /// Configures services and adds them to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The collection of services to configure.</param>
    void ConfigureServices(IServiceCollection services);
}
