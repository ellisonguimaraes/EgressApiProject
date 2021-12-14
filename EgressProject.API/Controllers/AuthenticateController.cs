using System;
using System.Net;
using System.Text.Json;
using AutoMapper;
using EgressProject.API.Business.Interfaces;
using EgressProject.API.Models;
using EgressProject.API.Models.InputModel;
using EgressProject.API.Models.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EgressProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticateBusiness _authenticateBusiness;
        private readonly IMapper _mapper;

        public AuthenticateController(IAuthenticateBusiness authenticateBusiness, IMapper mapper)
        {
            _authenticateBusiness = authenticateBusiness;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("signin")]
        public IActionResult Signin([FromBody] Login login)
        {
            string ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();

            Token token = _authenticateBusiness.Authenticate(login, ipAddress);

            if (token == null) return BadRequest("Não foi possível realizar a autenticação");

            return Ok(token);
        }

        [HttpPost]
        [Route("refreshtoken")]
        public IActionResult RefreshToken([FromBody] TokenInputModel tokenInputModel)
        {
            string ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();

            Token token = _authenticateBusiness.RefreshToken(_mapper.Map<Token>(tokenInputModel), ipAddress);

            if (token == null) return BadRequest("Refresh Token inválido");

            return Ok(token);
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody] RegisterInputModel registerInputModel)
        {
            User user = _authenticateBusiness.Register(registerInputModel);

            if (user == null) return BadRequest("Registro não pode ser efetuado");

            string ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();

            Token token = _authenticateBusiness.Authenticate(_mapper.Map<Login>(registerInputModel), ipAddress);

            if (token == null) return BadRequest("Não foi possível realizar a autenticação");

            return Ok(token);
        }

        [Authorize]
        [HttpGet]
        [Route("logout/{refreshToken}")]
        public IActionResult Revoke(string refreshToken)
        {
            Token token = new Token {
                AccessToken = ((string)HttpContext.Request.Headers["Authorization"])?.Split(" ")[1],
                RefreshToken = refreshToken
            };

            if (!_authenticateBusiness.RevokeToken(token)) return BadRequest("Revoke token não foi efetuado");

            return NoContent();
        }

        [Authorize]
        [HttpGet]
        [Route("logout/all")]
        public IActionResult Revoke()
        {
            User user = (User)HttpContext.Items["User"];

            if (!_authenticateBusiness.RevokeToken(user)) return BadRequest("Revoke token não foi efetuado");
            
            return NoContent();
        }
    }
}