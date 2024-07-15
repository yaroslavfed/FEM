using FEM.Common.Data.Domain;

namespace FEM.Core.Storages;

public interface IReadableStorage
{
    public Task<Axis> GetAxisAsync();
}