using KironBackendProject.Services.Shared.Interfaces;

namespace KironBackendProject.Services.Shared
{
    public class AutomatedProcessManager : IAutomatedProcessManager
    {
        private bool _isProcessStarted = false;
        private bool _isProcessCompleted = false;

        public bool IsProcessStarted() => _isProcessStarted;

        public void MarkProcessAsStarted()
        {
            _isProcessStarted = true;
        }

        public void MarkProcessAsCompleted()
        {
            _isProcessCompleted = true;
        }
    }
}
