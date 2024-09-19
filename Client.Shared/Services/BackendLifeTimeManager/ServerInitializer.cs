using System.Diagnostics;

namespace Client.Shared.Services.BackendLifeTimeManager;

public class ServerInitializer : IServerInitializer
{

    #region Fields

    private const           string   PROCESS_NAME    = "FEM.Server";
    private static readonly string   s_fileDirectory = Environment.CurrentDirectory;
    private                 Process? _process;

    #endregion

    #region Methods

    public void Start()
    {
        var pathToExecutable = GetExecutablePath();

        try
        {
            TryStartServer(pathToExecutable);
        } catch (Exception e)
        {
            Debug.Fail(e.Message);
        }
    }

    public void Kill()
    {
        _process?.Kill();
    }

    private static ProcessStartInfo GenerateProcessStartInfo(string filePath)
    {
        return new() { CreateNoWindow = false, UseShellExecute = true, FileName = filePath };
    }

    private static string GetExecutablePath()
    {
        var pathToExecutable = Path.Combine(s_fileDirectory, PROCESS_NAME);

        if (OperatingSystem.IsWindows())
        {
            pathToExecutable += ".exe";
        }

        return pathToExecutable;
    }

    private void TryStartServer(string filePath)
    {
        var existingServerProcess = FindServerProcess(filePath);

        if (existingServerProcess is not null) return;

        StartProcess(filePath);
    }

    private static Process? FindServerProcess(string filePath)
    {
        var processes = Process.GetProcessesByName(PROCESS_NAME);

        return processes.FirstOrDefault(process => process.MainModule?.FileName == filePath);
    }

    private void StartProcess(string filePath)
    {
        var startInfo = GenerateProcessStartInfo(filePath);
        _process = Process.Start(startInfo);
    }

    #endregion

}