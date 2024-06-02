using Microsoft.EntityFrameworkCore;

namespace KironBackendProject.Data
{
    public class AppDbConnectionManager
    {
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(10); // limit to 10 connections
        private readonly DbContextOptions<AppDbContext> _options;

        public AppDbConnectionManager(DbContextOptions<AppDbContext> options)
        {
            _options = options;
        }

        public async Task<AppDbContext> GetDbContextAsync()
        {
            await _semaphore.WaitAsync();

            try
            {
                return new AppDbContext(_options);
            }
            catch
            {
                _semaphore.Release();
                throw;
            }
        }

        public void ReleaseDbContext()
        {
            _semaphore.Release();
        }
    }
}
