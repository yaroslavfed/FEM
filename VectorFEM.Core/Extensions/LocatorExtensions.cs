using System.Diagnostics.CodeAnalysis;
using Splat;
using VectorFEM.Core.InjectionBuilder;

namespace VectorFEM.Core.Extensions;

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