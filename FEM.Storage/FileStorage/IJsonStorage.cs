using FEM.SharedDTO.Abstractions;
using FEM.SharedDTO.Domain;

namespace FEM.Storage.FileStorage;

public interface IJsonStorage
{
    Task<Axis> GetAxisAsync();

    Task SaveResultToFileAsync<TData>(TData result, string fileName) where TData : TestResultBase;
}