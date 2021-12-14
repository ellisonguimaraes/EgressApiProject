using System.Text.Json;
using EgressProject.API.Business.Interfaces;
using EgressProject.API.Models.Utils;
using Microsoft.AspNetCore.Mvc;

namespace EgressProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmploymentController : ControllerBase
    {
        private readonly IEmploymentBusiness _employmentBusiness;

        public EmploymentController(IEmploymentBusiness employmentBusiness)
        {
            _employmentBusiness = employmentBusiness;
        }

        [HttpGet]
        [Route("{PageNumber}/{PageSize}")]
        public IActionResult Get([FromRoute] PaginationParameters paginationParameters)
        {
            var employments = _employmentBusiness.GetPaginate(paginationParameters);

            var metadata = new {
                employments.TotalCount,
                employments.PageSize,
                employments.CurrentPage,
                employments.HasPrevious,
                employments.HasNext,
                employments.TotalPages
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));

            return Ok(employments);
        }
    }
}