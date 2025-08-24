namespace Modulix;

/// <summary>
/// Provides extension methods for <see cref="IServiceBindingCollection"/>.
/// </summary>
public static class ServiceBindingCollectionExtensions
{
    /// <summary>
    /// Adds a service binding for the specified service type to the collection.
    /// </summary>
    /// <typeparam name="TService">The type of the service to bind.</typeparam>
    /// <param name="bindings">The collection of service bindings.</param>
    /// <returns>The updated collection of service bindings.</returns>
    public static IServiceBindingCollection AddBinding<TService>(this IServiceBindingCollection bindings)
        where TService : class
    {
        var binding = new ServiceBinding(typeof(TService));
        bindings.Add(binding);
        return bindings;
    }
}
