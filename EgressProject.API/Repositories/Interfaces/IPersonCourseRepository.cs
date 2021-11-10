using EgressProject.API.Models;
using EgressProject.API.Models.Utils;

namespace EgressProject.API.Repositories.Interfaces
{
    public interface IPersonCourseRepository
    {
        PagedList<PersonCourse> GetPaginate(PaginationParameters paginationParameters);
        PersonCourse GetById(int personId, int courseId);
        PersonCourse Create(PersonCourse item);
        PersonCourse Update(PersonCourse item);
        bool Delete(int personId, int courseId);
    }
}