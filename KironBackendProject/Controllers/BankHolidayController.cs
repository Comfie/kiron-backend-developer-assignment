using KironBackendProject.Services.Interfaces;
using KironBackendProject.Services.Shared.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KironBackendProject.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class BankHolidayController : ControllerBase
    {
        private readonly IBankHolidayService _bankHolidayService;
        private readonly IAutomatedProcessManager _processManager;

        public BankHolidayController(IBankHolidayService bankHolidayService,
            IAutomatedProcessManager processManager)
        {
            _bankHolidayService = bankHolidayService;
            _processManager = processManager;
        }

        [HttpGet("fetch-holidays")]
        public async Task<IActionResult> FetchBankHolidays()
        {
            if (_processManager.IsProcessStarted())
            {
                return BadRequest("Process already started");
            }
            await _bankHolidayService.FetchAndStoreBankHolidaysAsync();
            _processManager.MarkProcessAsStarted();
            return Ok();
        }

        [HttpGet("get-regions")]
        public async Task<IActionResult> GetRegions()
        {
            var result = await _bankHolidayService.GetAllRegionsAsync();
            return Ok(result);
        }


        [HttpGet("get-bank-holidays/{regionId}")]
        public async Task<IActionResult> GetBankHolidays(int regionId)
        {
            var result = await _bankHolidayService.GetHolidaysByRegionAsync(regionId);
            return Ok(result);
        }
    }
}
