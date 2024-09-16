using FEM.Common.Data.Domain;
using FEM.Common.Data.TestSession;

namespace FEM.Storage.FileStorage;

public interface IJsonStorage
{
    Task<Axis> GetAxisAsync();

    Task SaveResultToFileAsync(TestResult result, string fileName);
}