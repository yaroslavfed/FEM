using System.Diagnostics.CodeAnalysis;
using FEM.Common.InjectionBuilder;
using Splat;

namespace FEM.Common.Extensions;

public static class LocatorExtensions
{
    public static InjectionBuilder<TService> WithBuilder<
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
        TService
    >(this IReadonlyDependencyResolver @this) =>
        new(@this);
}