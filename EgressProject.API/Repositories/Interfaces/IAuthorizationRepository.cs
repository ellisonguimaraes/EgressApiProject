using EgressProject.API.Models;

namespace EgressProject.API.Repositories.Interfaces
{
    public interface IAuthorizationRepository : IEntityRepository<Authorization>
    {
        Authorization GetByRefreshToken(string refreshToken);
    }
}