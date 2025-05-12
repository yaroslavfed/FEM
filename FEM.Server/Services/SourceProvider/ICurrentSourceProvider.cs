using FEM.Server.Data.Domain;

namespace FEM.Server.Services.SourceProvider;

public interface ICurrentSourceProvider
{
    Task<IReadOnlyList<CurrentSegment>> GetSourcesAsync(CurrentSource source);
}