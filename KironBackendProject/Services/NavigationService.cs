using KironBackendProject.Data.Dtos;
using KironBackendProject.Data.Entities;
using KironBackendProject.Data.Repositories;
using KironBackendProject.Services.Interfaces;

namespace KironBackendProject.Services
{
    public class NavigationService : INavigationService
    {
        private readonly IGenericRepository<Navigation> _navigationRepository;

        public NavigationService(IGenericRepository<Navigation> navigationRepository)
        {
            _navigationRepository = navigationRepository;
        }

        public async Task<List<NavigationDto>> GetNavigationItemsAsync()
        {
            var items = (await _navigationRepository.GetAllAsync()).ToList();
            return BuildHierarchy(items);
        }

        private List<NavigationDto> BuildHierarchy(List<Navigation> items)
        {
            var lookup = items.ToLookup(item => item.ParentId);
            var rootItems = lookup[-1];

            List<NavigationDto> BuildNodes(int parentId)
            {
                return lookup[parentId]
                    .Select(item => new NavigationDto
                    {
                        Text = item.Text,
                        Children = BuildNodes(item.Id)
                    })
                    .ToList();
            }

            return BuildNodes(-1);
        }
    }
}
