using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EgressProject.API.Business.Interfaces;
using EgressProject.API.Models.InputModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EgressProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonBusiness _personBusiness;

        public PersonController(IPersonBusiness personBusiness)
        {
            _personBusiness = personBusiness;
        }

        [HttpPost]
        public IActionResult Post([FromForm]PersonInputModel personInputModel)
        {
            return Ok($"Oláaa");
        }
    }
}