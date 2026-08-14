namespace Dibware.PubSub.Core.Extensions;

using System;

/// <summary>
/// Provides extension methods for the <see cref="Type"/> class.
/// </summary>
public static class TypeExtensions
{
    /// <summary>
    /// Determines whether the specified type is awaitable (i.e., a Task, ValueTask, or IAsyncEnumerable).
    /// </summary>
    /// <param name="type">
    /// The type to check for awaitability.
    /// </param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified type is awaitable; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool IsAwaitable(Type type)
    {
        if (type.IsTask() || type.IsValueTask())
            return true;

        if (type.IsGenericType)
        {
            var genericDefinition = type.GetGenericTypeDefinition();

            return genericDefinition.IsTask() ||
                   genericDefinition.IsValueTask() ||
                   genericDefinition == typeof(IAsyncEnumerable<>);
        }

        return false;
    }

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

    /// <summary>
    /// Determines whether the specified interface is a generic type and matches the given generic type definition.
    /// </summary>
    /// <param name="interface">
    /// The interface type to check.
    /// </param>
    /// <param name="genericTypeDefinitionToMatch">
    /// The generic type definition to match.
    /// </param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified interface is a generic type and matches the given generic type definition; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool IsGenericInterfaceWhichMatchesType(this Type @interface, Type genericTypeDefinitionToMatch) => @interface.IsGenericType && genericTypeDefinitionToMatch == @interface.GetGenericTypeDefinition();

    /// <summary>
    /// Determines whether the specified type is a <see cref="Task"/>.
    /// </summary>
    /// <param name="type">
    /// The type to check for being a <see cref="Task"/>.
    /// </param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified type is a <see cref="Task"/>; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool IsTask(this Type type)
    {
        return type == typeof(Task);
    }

    /// <summary>
    /// Determines whether the specified type is a <see cref="ValueTask"/>.
    /// </summary>
    /// <param name="type">
    /// The type to check for being a <see cref="ValueTask"/>.
    /// </param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified type is a <see cref="ValueTask"/>; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool IsValueTask(this Type type)
    {
        return type == typeof(ValueTask);
    }
}
