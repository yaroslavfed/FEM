using FEM.Common.Data.Domain;

namespace FEM.Storage.FileStorage;

public interface IJsonStorage
{
    Task<Axis> GetAxisAsync();

    Task SaveResultToFileAsync(TestResult result, string fileName);
}