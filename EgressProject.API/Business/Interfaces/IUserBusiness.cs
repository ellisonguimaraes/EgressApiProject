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
        PagedList<User> GetPaginate(PaginationParameters paginationParameters);
        User GetById(int id);
        User Update(User user);
        bool Delete(int id);
    }

    
}