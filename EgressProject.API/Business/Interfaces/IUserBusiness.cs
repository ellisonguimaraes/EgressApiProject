using EgressProject.API.Models;
using EgressProject.API.Models.InputModel;
using EgressProject.API.Models.Utils;
using EgressProject.API.Models.ViewModel;

namespace EgressProject.API.Business.Interfaces
{
    public interface IUserBusiness
    {
        PagedList<UserViewModel> GetPaginate(PaginationParameters paginationParameters);
        UserViewModel GetById(int id);
        UserViewModel Update(UserInputModel user);
        bool Delete(int id);
    }
}