using FEM.Shared.Domain.Data;

namespace FEM.Core.Storages;

public interface IReadable<TEntity>
{
    Task<TEntity> GetItemAsync();

    Task<IReadOnlyList<TEntity>> ListItemsAsync();
}