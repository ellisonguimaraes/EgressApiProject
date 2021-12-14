using System;
using EgressProject.API.Business.Interfaces;
using EgressProject.API.Models;
using EgressProject.API.Models.Utils;
using EgressProject.API.Repositories.Interfaces;

namespace EgressProject.API.Business
{
    public class EmploymentBusiness : IEmploymentBusiness
    {
        private readonly IEntityRepository<Employment> _employmentRepository;

        public EmploymentBusiness(IEntityRepository<Employment> employmentRepository)
        {
            _employmentRepository = employmentRepository;
        }

        public PagedList<Employment> GetPaginate(PaginationParameters paginationParameters)
        {
            return _employmentRepository.GetPaginate(paginationParameters);
        }

        public Employment Create(Employment employment)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Employment Update(Employment employment)
        {
            throw new NotImplementedException();
        }
    }
}