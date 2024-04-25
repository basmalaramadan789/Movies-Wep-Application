using MoviesApp.Model;

namespace MoviesApp.Services
{
    public interface IAuthService
    {
        Task<Authentication> Register(RegisterModel model);
        Task<Authentication> GetTokenAsync(TokenRequestModel model);
        Task<string> AddRoleAsync(AddRoleModel model);
    }
}
