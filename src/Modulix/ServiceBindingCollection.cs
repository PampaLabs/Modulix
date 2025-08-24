namespace Modulix;

/// <summary>
/// A default implementation of <see cref="IServiceBindingCollection"/> 
/// that stores <see cref="ServiceBinding"/> objects in a list.
/// </summary>
public class ServiceBindingCollection : List<ServiceBinding>, IServiceBindingCollection
{
}
