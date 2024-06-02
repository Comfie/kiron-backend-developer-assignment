using KironBackendProject.Data.Dtos;

namespace KironBackendProject.Services.Interfaces
{
    public interface INavigationService
    {

        Task<List<NavigationDto>> GetNavigationItemsAsync();
    }
}
