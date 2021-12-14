using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EgressProject.API.Business.Interfaces;
using EgressProject.API.Models;
using EgressProject.API.Models.Utils;

namespace EgressProject.API.Business
{
    public class PersonBusiness : IPersonBusiness
    {
        public Person Create(Person person)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public PagedList<Person> GetPaginate(PaginationParameters paginationParameters)
        {
            throw new NotImplementedException();
        }

        public Person Update(Person person)
        {
            throw new NotImplementedException();
        }
    }
}