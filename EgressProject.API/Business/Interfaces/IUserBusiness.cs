using EgressProject.API.Models;
using EgressProject.API.Models.InputModel;
using EgressProject.API.Models.Utils;

namespace EgressProject.API.Business.Interfaces
{
    public interface IUserBusiness
    {
        Token Authenticate(Login login, string ipAddress);
        Token RefreshToken(Token token, string ipAddress);
        bool RevokeToken(Token token);
        User Register(RegisterInputModel registerInputModel);
    }

    
}