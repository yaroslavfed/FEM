using System.Diagnostics.CodeAnalysis;
using FEM.Core.InjectionBuilder;
using Splat;

namespace FEM.Core.Extensions;

public static class LocatorExtensions
{
    public static InjectionBuilder<TService> WithBuilder<
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
        TService
    >(this IReadonlyDependencyResolver @this)
    {
        return new InjectionBuilder<TService>(@this);
    }
}