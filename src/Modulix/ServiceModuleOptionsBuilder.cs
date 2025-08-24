namespace Modulix;

/// <summary>
/// Provides a builder for configuring <see cref="ServiceModuleOptions"/>.
/// </summary>
public class ServiceModuleOptionsBuilder
{
    /// <summary>
    /// Gets the current <see cref="ServiceModuleOptions"/> being configured.
    /// </summary>
    public ServiceModuleOptions Options { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ServiceModuleOptionsBuilder"/> class
    /// with default options.
    /// </summary>
    public ServiceModuleOptionsBuilder()
    {
        Options = new();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ServiceModuleOptionsBuilder"/> class
    /// using the specified options.
    /// </summary>
    /// <param name="options">The options to configure.</param>
    public ServiceModuleOptionsBuilder(ServiceModuleOptions options)
    {
        Options = options;
    }

    /// <summary>
    /// Configures services using a delegate that receives only <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="configureServices">The action to configure services.</param>
    /// <returns>The current builder instance.</returns>
    public ServiceModuleOptionsBuilder ConfigureServices(Action<IServiceCollection> configureServices)
    {
        Options.Registrar = (_, services) =>
            configureServices(services);

        return this;
    }

    /// <summary>
    /// Configures services using a delegate that receives <see cref="IServiceProvider"/> and <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="configureServices">The action to configure services.</param>
    /// <returns>The current builder instance.</returns>
    public ServiceModuleOptionsBuilder ConfigureServices(Action<IServiceProvider, IServiceCollection> configureServices)
    {
        Options.Registrar = (sp, services) =>
            configureServices(sp, services);

        return this;
    }

    /// <summary>
    /// Configures services using a type that implements <see cref="IServiceConfiguration"/>.
    /// </summary>
    /// <typeparam name="TServiceConfiguration">The type that implements service configuration.</typeparam>
    /// <returns>The current builder instance.</returns>
    public ServiceModuleOptionsBuilder ConfigureServices<TServiceConfiguration>()
        where TServiceConfiguration : IServiceConfiguration
    {
        Options.Registrar = (sp, services) =>
            ActivatorUtilities.CreateInstance<TServiceConfiguration>(sp).ConfigureServices(services);

        return this;
    }

    /// <summary>
    /// Configures imported service bindings using an action.
    /// </summary>
    /// <param name="configureBindings">The action to configure imports.</param>
    /// <returns>The current builder instance.</returns>
    public ServiceModuleOptionsBuilder Imports(Action<IServiceBindingCollection> configureBindings)
    {
        var bindings = new ServiceBindingCollection();
        configureBindings(bindings);

        Options.Imports = bindings;

        return this;
    }

    /// <summary>
    /// Sets imported service bindings directly.
    /// </summary>
    /// <param name="bindings">The collection of imported bindings.</param>
    /// <returns>The current builder instance.</returns>
    public ServiceModuleOptionsBuilder Imports(IServiceBindingCollection bindings)
    {
        Options.Imports = bindings;

        return this;
    }

    /// <summary>
    /// Configures exported service bindings using an action.
    /// </summary>
    /// <param name="configureBindings">The action to configure exports.</param>
    /// <returns>The current builder instance.</returns>
    public ServiceModuleOptionsBuilder Exports(Action<IServiceBindingCollection> configureBindings)
    {
        var bindings = new ServiceBindingCollection();
        configureBindings(bindings);

        Options.Exports = bindings;

        return this;
    }

    /// <summary>
    /// Sets exported service bindings directly.
    /// </summary>
    /// <param name="bindings">The collection of exported bindings.</param>
    /// <returns>The current builder instance.</returns>
    public ServiceModuleOptionsBuilder Exports(IServiceBindingCollection bindings)
    {
        Options.Exports = bindings;

        return this;
    }
}
