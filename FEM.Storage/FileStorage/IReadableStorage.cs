using FEM.Common.Data.Domain;

namespace FEM.Storage.FileStorage;

public interface IReadableStorage
{
    public Task<Axis> GetAxisAsync();
}