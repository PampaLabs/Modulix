namespace Modulix;

/// <summary>
/// Provides configuration options for a service module,
/// including registrar, imports, and exports.
/// </summary>
public class ServiceModuleOptions
{
    /// <summary>
    /// Gets or sets the delegate used to register services.
    /// </summary>
    public ServiceModuleRegistrar Registrar { get; set; }

    /// <summary>
    /// Gets or sets the collection of imported service bindings.
    /// </summary>
    public IServiceBindingCollection Imports { get; set; }

    /// <summary>
    /// Gets or sets the collection of exported service bindings.
    /// </summary>
    public IServiceBindingCollection Exports { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ServiceModuleOptions"/> class.
    /// </summary>
    public ServiceModuleOptions()
    {
        Registrar = (_, _) => { };
        Imports = new ServiceBindingCollection();
        Exports = new ServiceBindingCollection();
    }
}
