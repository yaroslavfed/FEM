using FEM.Common.Data.TestSession;

namespace FEM.Server.Services.SaverService;

public interface ISaverService
{
    Task SaveResultAsync(TestResult result);

    Task WriteListToFileAsync<T>(string fileName, IList<T> list);
}