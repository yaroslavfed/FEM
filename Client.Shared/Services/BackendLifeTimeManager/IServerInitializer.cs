namespace Client.Shared.Services.BackendLifeTimeManager;

public interface IServerInitializer
{
    void Start();

    void Kill();
}