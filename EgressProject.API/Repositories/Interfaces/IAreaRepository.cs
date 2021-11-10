using EgressProject.API.Models;
using EgressProject.API.Models.Utils;

namespace EgressProject.API.Repositories.Interfaces
{
    public interface IAreaRepository
    {
        PagedList<Area> GetPaginate(PaginationParameters paginationParameters);
        Area GetById(int courseId, int jobId);
        Area Create(Area item);
        Area Update(Area item);
        bool Delete(int courseId, int jobId);
    }
}