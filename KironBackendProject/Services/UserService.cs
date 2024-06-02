using KironBackendProject.Data;
using KironBackendProject.Data.Dtos;
using KironBackendProject.Data.Entities;
using KironBackendProject.Services.Interfaces;
using KironBackendProject.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KironBackendProject.Services
{
    public class UserService : IUserService
    {

        private readonly AppDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IConfiguration _configuration;
        private readonly AppSettings _appSettings;

        public UserService(AppDbContext context,
            IPasswordHasher<User> passwordHasher,
            IConfiguration configuration,
            IOptions<AppSettings> appSettings)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
            _appSettings = appSettings.Value;
        }

        public async Task<int?> CreateUserAsync(CreateUserRequest createUserRequest, CancellationToken cancellationToken = default)
        {
            if (await _context.Users.AnyAsync(u => u.Username == createUserRequest.UserName, cancellationToken))
                return null;

            var user = new User
            {
                Username = createUserRequest.UserName,
            };

            await _context.Users.AddAsync(user, cancellationToken);

            var passwordHash = _passwordHasher.HashPassword(user, createUserRequest.Password);

            user.PasswordHash = passwordHash;
            await _context.SaveChangesAsync(cancellationToken);
            return user.Id;
        }

        public async Task<UserAuthResponse?> LoginAsync(AuthRequest loginRequest, CancellationToken cancellationToken = default)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == loginRequest.Username, cancellationToken);
            if (user is null)
                return null;

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginRequest.Password);
            if (result != PasswordVerificationResult.Success)
                return null;

            var token = GenerateJwtToken(user);

            return new UserAuthResponse
            {
                Id = user.Id,
                UserName = user.Username,
                Token = token
            };
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.GivenName, user.Username)
            }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
