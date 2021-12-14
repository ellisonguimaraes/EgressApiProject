using EgressProject.API.Models;
using EgressProject.API.Models.InputModel;
using EgressProject.API.Models.Utils;

namespace EgressProject.API.Business.Interfaces
{
    public interface IAuthenticateBusiness
    {
        Token Authenticate(Login login, string ipAddress);
        Token RefreshToken(Token token, string ipAddress);
        bool RevokeToken(Token token);
        bool RevokeToken(User user);
        User Register(RegisterInputModel registerInputModel);
    }
}