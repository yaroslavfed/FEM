namespace FEM.Server.Services.BoundaryProvider;

public interface IBoundaryProvider
{
    Task<IEnumerable<int>> GetConstrainedDegreesOfFreedomAsync();
}