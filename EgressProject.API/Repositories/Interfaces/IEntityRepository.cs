using EgressProject.API.Models;
using EgressProject.API.Models.Utils;

namespace EgressProject.API.Repositories.Interfaces
{
    public interface IEntityRepository<Entity> where Entity : EntityBase
    {
        PagedList<Entity> GetPaginate(PaginationParameters paginationParameters);
        Entity GetById(int id);
        Entity Create(Entity item);
        Entity Update(Entity item);
        bool Delete(int id);
    }
}