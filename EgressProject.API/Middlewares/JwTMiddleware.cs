using System.Linq;
using System.Threading.Tasks;
using EgressProject.API.Repositories.Interfaces;
using EgressProject.API.Services.Auth;
using Microsoft.AspNetCore.Http;

namespace EgressProject.API.Middlewares
{
    public class JwTMiddleware
    {
        private readonly RequestDelegate _next;
        
        public JwTMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        
        public async Task Invoke(HttpContext context, IUserRepository userRepository, IJwTUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            var userId = jwtUtils.ValidateJwTToken(token);

            if(userId != null)
                context.Items["User"] = userRepository.GetById(userId.Value);

            await _next(context);
        }
    }
}