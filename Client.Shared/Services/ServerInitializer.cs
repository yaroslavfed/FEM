using System.Diagnostics;

namespace Client.Shared.Services;

public class ServerInitializer
{

    #region Fields

    private const           string   PROCESS_NAME    = "FEM.Server.exe";
    private static readonly string   s_fileDirectory = Environment.CurrentDirectory;
    private                 Process? _process;

    #endregion

    #region Methods

    public void Start()
    {
        var pathToExecutable = Path.Combine(s_fileDirectory, PROCESS_NAME);

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
        return new ProcessStartInfo { CreateNoWindow = true, UseShellExecute = false, FileName = filePath };
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

        return processes.FirstOrDefault(process => process.StartInfo.FileName == filePath);
    }

    private void StartProcess(string filePath)
    {
        var startInfo = GenerateProcessStartInfo(filePath);
        _process = Process.Start(startInfo);
    }

    #endregion

}