using KironBackendProject.Data.Entities;

namespace KironBackendProject.Services.Interfaces
{
    public interface IBankHolidayService
    {
        Task FetchAndStoreBankHolidaysAsync(CancellationToken cancellationToken = default);
        Task<List<Region>> GetAllRegionsAsync(CancellationToken cancellationToken = default);
        Task<List<BankHoliday>> GetHolidaysByRegionAsync(int regionId, CancellationToken cancellationToken = default);
    }
}
