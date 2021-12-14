using EgressProject.API.Models;
using EgressProject.API.Models.Utils;

namespace EgressProject.API.Business.Interfaces
{
    public interface IEmploymentBusiness
    {
        PagedList<Employment> GetPaginate(PaginationParameters paginationParameters);
        Employment Create(Employment employment);
        Employment Update(Employment employment);
        bool Delete(int id);
    }
}