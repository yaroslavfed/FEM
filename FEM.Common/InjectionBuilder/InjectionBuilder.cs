using System.Diagnostics.CodeAnalysis;
using Splat;

namespace FEM.Common.InjectionBuilder;

public class InjectionBuilder<
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
    TService
>
{
    private readonly IReadonlyDependencyResolver         _dependencyResolver;
    private readonly IList<(Type Type, object Instance)> _autocompleteMap = new List<(Type Type, object Instance)>();

    internal InjectionBuilder(IReadonlyDependencyResolver dependencyResolver)
    {
        _dependencyResolver = dependencyResolver;
    }

    public InjectionBuilder<TService> WithAutocomplete<TDependency>(TDependency instance)
    {
        _autocompleteMap.Add((typeof(TDependency), instance)!);

        return this;
    }

    public TService BuildService()
    {
        var serviceType = typeof(TService);
        var dependenciesQuantity = _autocompleteMap.Count;

        var constructor = serviceType
                          .GetConstructors()
                          .FirstOrDefault(c => c.GetParameters().Length >= dependenciesQuantity);

        if (constructor is null)
            throw new ArgumentException("No matching constructor");

        var inputDependenciesTypeList = _autocompleteMap.Select(dependencyEntry => dependencyEntry.Type).ToHashSet();

        var dependenciesToResolve = constructor.GetParameters()
                                               .Where(
                                                   parameter =>
                                                       !inputDependenciesTypeList.Contains(parameter.ParameterType)
                                               );

        var resolvedDependencyList =
            dependenciesToResolve.Select(
                dependency => _dependencyResolver.GetService(dependency.ParameterType)
            );

        var autocompleteDependencyList = _autocompleteMap.Select(dependencyEntry => dependencyEntry.Instance);
        var fullDependenciesList = autocompleteDependencyList.Concat(resolvedDependencyList).ToArray();

        return (TService)constructor.Invoke(fullDependenciesList);
    }
}