namespace KironBackendProject.Services.Shared.Interfaces
{
    public interface ICacheService
    {
        T Get<T>(string key);
        void Set<T>(string key, T value, int durationInMinutes);
        bool Exists(string key);
        void Remove(string key);
    }
}
