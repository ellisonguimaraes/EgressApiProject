using System.Text.Json;
using AutoMapper;
using EgressProject.API.Business.Interfaces;
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

        [HttpGet]
        [Route("{PageNumber}/{PageSize}")]
        public IActionResult Get([FromRoute] PaginationParameters paginationParameters)
        {
            var usersViewModel = _userBusiness.GetPaginate(paginationParameters);

            var metadata = new {
                usersViewModel.TotalCount,
                usersViewModel.PageSize,
                usersViewModel.CurrentPage,
                usersViewModel.HasPrevious,
                usersViewModel.HasNext,
                usersViewModel.TotalPages
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));

            return Ok(usersViewModel);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            var userViewModel = _userBusiness.GetById(id);

            if (userViewModel == null) return BadRequest("Usuário inválido");

            return Ok(userViewModel);
        }

        [HttpPut]
        public IActionResult Update([FromBody] UserInputModel userInputModel)
        {
            var userViewModel = _userBusiness.Update(userInputModel);
            return Ok(userViewModel);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            if (!_userBusiness.Delete(id)) return BadRequest("Não foi possível remover");
            return NoContent();
        }
    }
}