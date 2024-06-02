using KironBackendProject.Data.Dtos;

namespace KironBackendProject.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserAuthResponse?> LoginAsync(AuthRequest loginRequest, CancellationToken cancellationToken = default);
        Task<int?> CreateUserAsync(CreateUserRequest createUserRequest, CancellationToken cancellationToken = default);
    }
}
