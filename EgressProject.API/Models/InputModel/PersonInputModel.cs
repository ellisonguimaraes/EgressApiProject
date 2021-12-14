using System;
using Microsoft.AspNetCore.Http;

namespace EgressProject.API.Models.InputModel
{
    public class PersonInputModel
    {
        public int? Id { get; set; }
        public string Cpf { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public byte? Sex { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumber2 { get; set; }
        public IFormFile PerfilImageSrc { get; set; }
        public bool? ExposeData { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
}