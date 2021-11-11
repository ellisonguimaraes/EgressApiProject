using EgressProject.API.Models;

namespace EgressProject.API.Repositories.Interfaces
{
    public interface IUserRepository : IEntityRepository<User>
    {
        User GetByEmail(string email);
        User GetByLogin(string email, string password);
    }
}