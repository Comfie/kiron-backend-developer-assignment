namespace KironBackendProject.Services.Shared.Interfaces
{
    public interface IAutomatedProcessManager
    {
        bool IsProcessStarted();
        void MarkProcessAsStarted();
        void MarkProcessAsCompleted();
    }
}
