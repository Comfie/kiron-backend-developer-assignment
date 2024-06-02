using KironBackendProject.Services.Interfaces;
using KironBackendProject.Services.Shared.Interfaces;

namespace KironBackendProject.Services.Shared
{
    public class BankHolidaysUpdateService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IAutomatedProcessManager _processManager;

        public BankHolidaysUpdateService(IServiceProvider serviceProvider, IAutomatedProcessManager processManager)
        {
            _serviceProvider = serviceProvider;
            _processManager = processManager;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken = default)
        {
            await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_processManager.IsProcessStarted())
                {

                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var bankHolidaysService = scope.ServiceProvider.GetRequiredService<IBankHolidayService>();
                        await bankHolidaysService.FetchAndStoreBankHolidaysAsync(stoppingToken);
                    }

                    await Task.Delay(TimeSpan.FromHours(24), stoppingToken);

                }
            }
        }
    }

}
