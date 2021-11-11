using EgressProject.API.Models;
using EgressProject.API.Models.Utils;

namespace EgressProject.API.Services.Auth
{
    public interface IJwTUtils
    {
        Token GenerateToken(User user);
        string GenerateAccessToken(User user);
        string GenerateRefreshToken();
        int? ValidateJwTToken(string token);
    }
}