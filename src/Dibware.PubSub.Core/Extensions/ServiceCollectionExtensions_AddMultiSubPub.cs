namespace Dibware.PubSub.Core.Extensions;

using Dibware.PubSub.Core.Contracts;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Provides extension methods for adding the MultiSubPub to an IServiceCollection.
/// </summary>
public static class ServiceCollectionExtensions_AddMultiSubPub
{
    /// <summary>
    /// Adds the <see cref="MultiSubPub"/> to the service collection using configuration built by the specified action.
    /// </summary>
    /// <param name="services">
    /// The service collection to which the <see cref="MultiSubPub"/> will be added.
    /// </param>
    /// <returns>
    /// The updated service collection with the <see cref="MultiSubPub"/> added.
    /// </returns>
    public static IServiceCollection AddMultiSubPub(this IServiceCollection services)
    {
        services.AddScoped<ISubscribeAndPublish, MultiSubPub>();

        return services;
    }
}
