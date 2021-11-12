using System.Text.Json;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public UserController(IUserBusiness userBusiness, IMapper mapper)
        {
            _userBusiness = userBusiness;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("signin")]
        public IActionResult Signin([FromBody] Login login)
        {
            string ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();

            Token token = _userBusiness.Authenticate(login, ipAddress);

            if (token == null) return BadRequest("Não foi possível realizar a autenticação");

            return Ok(token);
        }

        [HttpPost]
        [Route("refreshtoken")]
        public IActionResult RefreshToken([FromBody] TokenInputModel tokenInputModel)
        {
            string ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();

            Token token = _userBusiness.RefreshToken(_mapper.Map<Token>(tokenInputModel), ipAddress);

            if (token == null) return BadRequest("Refresh Token inválido");

            return Ok(token);
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody] RegisterInputModel registerInputModel)
        {
            User user = _userBusiness.Register(registerInputModel);

            if (user == null) return BadRequest("Registro não pode ser efetuado");

            string ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();

            Token token = _userBusiness.Authenticate(_mapper.Map<Login>(registerInputModel), ipAddress);

            if (token == null) return BadRequest("Não foi possível realizar a autenticação");

            return Ok(token);
        }

        [HttpGet]
        [Route("{PageNumber}/{PageSize}")]
        public IActionResult Get([FromRoute] PaginationParameters paginationParameters)
        {
            var users = _userBusiness.GetPaginate(paginationParameters);

            var metadata = new {
                users.TotalCount,
                users.PageSize,
                users.CurrentPage,
                users.HasPrevious,
                users.HasNext,
                users.TotalPages
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));

            return Ok(users);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            User user = _userBusiness.GetById(id);

            if (user == null) return BadRequest("Usuário inválido");

            return Ok(user);
        }
    }
}