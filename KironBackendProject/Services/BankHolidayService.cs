using KironBackendProject.Data;
using KironBackendProject.Data.Dtos;
using KironBackendProject.Data.Entities;
using KironBackendProject.Data.Repositories;
using KironBackendProject.Services.Interfaces;
using KironBackendProject.Services.Shared.Interfaces;
using KironBackendProject.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace KironBackendProject.Services
{
    public class BankHolidaysService : IBankHolidayService
    {

        private readonly AppDbContext _context;
        private readonly IGenericRepository<BankHoliday> _bankHolidayRepository;
        private readonly IGenericRepository<Region> _regionRepository;
        private readonly IGenericRepository<RegionBankHoliday> _regionBankHolidayRepository;
        private readonly ICacheService _cacheService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly AppSettings _appSettings;
        private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);


        public BankHolidaysService(
                IGenericRepository<BankHoliday> bankHolidayRepository,
                IGenericRepository<Region> regionRepository,
                IGenericRepository<RegionBankHoliday> regionBankHolidayRepository,
                ICacheService cacheService,
                IHttpClientFactory httpClientFactory,
                IOptions<AppSettings> appSettings,
                AppDbContext context)
        {
            _bankHolidayRepository = bankHolidayRepository;
            _regionRepository = regionRepository;
            _regionBankHolidayRepository = regionBankHolidayRepository;
            _cacheService = cacheService;
            _httpClientFactory = httpClientFactory;
            _appSettings = appSettings.Value;
            _context = context;
        }

        public async Task FetchAndStoreBankHolidaysAsync(CancellationToken cancellationToken = default)
        {
            await _semaphore.WaitAsync(cancellationToken);
            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetStringAsync(_appSettings.BankHolidaysUrl, cancellationToken);
                var bankHolidaysData = JsonConvert.DeserializeObject<Dictionary<string, RegionHolidays>>(response);

                if (bankHolidaysData is null)
                    return;

                foreach (var division in bankHolidaysData)
                {
                    var region = await _regionRepository.GetAsync(r => r.Name == division.Key);
                    if (region == null)
                    {
                        region = new Region { Name = division.Key };
                        await _regionRepository.AddAsync(region);
                    }

                    foreach (var holidayEvent in division.Value.Events)
                    {
                        var bankHoliday = await _bankHolidayRepository.GetAsync(b => b.Title == holidayEvent.Title && b.Date == holidayEvent.Date);
                        if (bankHoliday == null)
                        {
                            bankHoliday = new BankHoliday
                            {
                                Title = holidayEvent.Title,
                                Date = holidayEvent.Date,
                                Notes = holidayEvent.Notes,
                                Bunting = holidayEvent.Bunting
                            };
                            await _bankHolidayRepository.AddAsync(bankHoliday);
                        }

                        var result = await _regionBankHolidayRepository.GetAsync(rb => rb.RegionId == region.Id && rb.BankHolidayId == bankHoliday.Id);

                        if (result == null)
                        {
                            var regionBankHoliday = new RegionBankHoliday
                            {
                                RegionId = region.Id,
                                BankHolidayId = bankHoliday.Id
                            };
                            await _regionBankHolidayRepository.AddAsync(regionBankHoliday);
                        }
                    }
                }
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task<List<Region>> GetAllRegionsAsync(CancellationToken cancellationToken = default)
        {
            const string cacheKey = "GetAllRegions";
            if (_cacheService.Exists(cacheKey))
            {
                return _cacheService.Get<List<Region>>(cacheKey);
            }

            var regions = await _regionRepository.GetAllAsync();
            _cacheService.Set(cacheKey, regions, 30);
            return regions.ToList();
        }

        public async Task<List<BankHoliday>> GetHolidaysByRegionAsync(int regionId, CancellationToken cancellationToken = default)
        {
            var cacheKey = $"GetHolidaysByRegion_{regionId}";
            if (_cacheService.Exists(cacheKey))
            {
                return _cacheService.Get<List<BankHoliday>>(cacheKey);
            }

            var holidays = await _context
                .RegionBankHolidays
                .Where(regionHoliday => regionHoliday.RegionId == regionId)
                .Select(regionHoliday => regionHoliday.BankHoliday)
                .ToListAsync(cancellationToken);

            _cacheService.Set(cacheKey, holidays, 30);
            return holidays;
        }
    }
}
