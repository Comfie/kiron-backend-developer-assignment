namespace KironBackendProject.Data.Dtos
{
    public class UserAuthResponse
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string UserName { get; set; } = string.Empty;
    }
}
