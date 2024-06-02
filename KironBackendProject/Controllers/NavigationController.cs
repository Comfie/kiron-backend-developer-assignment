using KironBackendProject.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KironBackendProject.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class NavigationController : ControllerBase
    {
        private readonly INavigationService _navigationService;

        public NavigationController(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
                
        [HttpGet("get-navigation-items")]
        public async Task<IActionResult> GetNavigationItems()
        {
            var result = await _navigationService.GetNavigationItemsAsync();
            return Ok(result);
        }
    }
}
