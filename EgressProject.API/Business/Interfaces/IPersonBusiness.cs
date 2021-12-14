using EgressProject.API.Models;
using EgressProject.API.Models.Utils;

namespace EgressProject.API.Business.Interfaces
{
    public interface IPersonBusiness
    {
        PagedList<Person> GetPaginate(PaginationParameters paginationParameters);
        Person Create(Person person);
        Person Update(Person person);
        bool Delete(int id);
    }
}