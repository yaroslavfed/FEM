using FEM.NonStationary.DTO.TestingContext;

namespace FEM.Server.Services.SaverService;

public interface ISaverService
{
    Task SaveResultAsync(NonStationaryTestResult result);

    Task WriteListToFileAsync<T>(string fileName, IList<T> list);
}