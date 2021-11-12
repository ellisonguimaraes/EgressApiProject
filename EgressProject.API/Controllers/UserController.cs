using EgressProject.API.Business.Interfaces;
using EgressProject.API.Models;
using EgressProject.API.Models.InputModel;
using EgressProject.API.Models.Utils;
using Microsoft.AspNetCore.Mvc;

namespace EgressProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness _userBusiness;

        public UserController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }

        [HttpPost]
        [Route("signin")]
        public IActionResult Signin([FromBody] Login login)
        {
            if (login == null) return BadRequest("Dados inválidos");

            string ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();

            Token token = _userBusiness.Authenticate(login, ipAddress);
            if (token == null) return BadRequest("Não foi possível realizar a autenticação");

            return Ok(token);
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody] RegisterInputModel registerInputModel)
        {
            if (registerInputModel == null) return BadRequest("Dados inválidos");

            User user = _userBusiness.Register(registerInputModel);

            System.Console.WriteLine($"Entrou e {registerInputModel.Email}");

            if (user == null) return BadRequest("Usuário é nulo");

            string ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();

            Token token = _userBusiness.Authenticate(new Login { Email = registerInputModel.Email, Password = registerInputModel.Password }, ipAddress);
            if (token == null) return BadRequest("Token e nulo.");

            return Ok(token);
        }
    }
}