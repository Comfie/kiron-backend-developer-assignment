namespace KironBackendProject.Data.Dtos
{
    public class NavigationDto
    {
        public string Text { get; set; }
        public List<NavigationDto> Children { get; set; } = new List<NavigationDto>();
    }
}
