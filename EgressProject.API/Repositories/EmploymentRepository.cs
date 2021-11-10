using System;
using System.Linq;
using EgressProject.API.Data;
using EgressProject.API.Models;
using EgressProject.API.Models.Utils;
using EgressProject.API.Repositories.Interfaces;

namespace EgressProject.API.Repositories
{
    public class EmploymentRepository : IEntityRepository<Employment>
    {
        private readonly ApplicationDbContext _context;
        public EmploymentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Employment GetById(int id)
            => _context.Employments
                .Where(em => em.Id == id)
                .SingleOrDefault();

        public PagedList<Employment> GetPaginate(PaginationParameters paginationParameters)
            => new PagedList<Employment>(
                _context.Employments
                    .OrderBy(em => em.Id),
                paginationParameters.PageNumber,
                paginationParameters.PageSize
            );

        public Employment Create(Employment item)
        {
            try {
                _context.Employments.Add(item);
                _context.SaveChanges();

            } catch(Exception) {
                throw;
            }

            return item;
        }

        public Employment Update(Employment item)
        {
            Employment getItem = GetById(item.Id);

            if(getItem != null)
            {
                try {
                    _context.Entry(getItem).CurrentValues.SetValues(item);
                    _context.SaveChanges();

                } catch(Exception) {
                    throw;
                }
            }

            return item;
        }

        public bool Delete(int id)
        {
            Employment getItem = GetById(id);

            if(getItem != null)
            {
                try {
                    _context.Employments.Remove(getItem);
                    _context.SaveChanges();
                    return true;

                } catch(Exception) {
                    throw;
                }
            }

            return false;
        }
    }
}