namespace Dibware.PubSub.Core.Extensions;

using System;

/// <summary>
/// Provides extension methods for the <see cref="Type"/> class.
/// </summary>
public static class TypeExtensions
{
    /// <summary>
    /// Gets a value indicating whether the specified type is a concrete class (i.e., a non-abstract class).
    /// </summary>
    /// <param name="type">
    /// The <see cref="Type"/> to check.
    /// </param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified type is a concrete class; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool IsConcreteClass(this Type type) => type.IsClass && !type.IsAbstract;
}
