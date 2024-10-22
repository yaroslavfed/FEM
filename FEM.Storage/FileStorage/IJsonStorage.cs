using FEM.Common.DTO.Abstractions;
using FEM.Common.DTO.Domain;

namespace FEM.Storage.FileStorage;

public interface IJsonStorage
{
    Task<Axis> GetAxisAsync();

    Task SaveResultToFileAsync(TestResultBase result, string fileName);
}