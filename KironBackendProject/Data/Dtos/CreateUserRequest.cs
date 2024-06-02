using System.ComponentModel.DataAnnotations;

namespace KironBackendProject.Data.Dtos
{
    public class CreateUserRequest
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {3} and at max {10} characters long.", MinimumLength = 3)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {10} characters long.", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
