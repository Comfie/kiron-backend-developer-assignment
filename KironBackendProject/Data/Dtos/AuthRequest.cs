using System.ComponentModel.DataAnnotations;

namespace KironBackendProject.Data.Dtos
{
    public class AuthRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
