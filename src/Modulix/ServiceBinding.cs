namespace Modulix;

/// <summary>
/// Represents a binding for a service type.
/// </summary>
public class ServiceBinding
{
    /// <summary>
    /// Gets the type of the service being bound.
    /// </summary>
    public Type ServiceType { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ServiceBinding"/> class
    /// with the specified service type.
    /// </summary>
    /// <param name="serviceType">The type of the service to bind.</param>
    public ServiceBinding(Type serviceType)
    {
        ServiceType = serviceType;
    }
}
