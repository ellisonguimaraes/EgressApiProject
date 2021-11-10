using System;
using System.Linq;
using EgressProject.API.Data;
using EgressProject.API.Models;
using EgressProject.API.Models.Utils;
using EgressProject.API.Repositories.Interfaces;

namespace EgressProject.API.Repositories
{
    public class TestimonyRepository : IEntityRepository<Testimony>
    {
        private readonly ApplicationDbContext _context;

        public TestimonyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Testimony GetById(int id)
            => _context.Testimonies
                .Where(te => te.Id == id)
                .SingleOrDefault();

        public PagedList<Testimony> GetPaginate(PaginationParameters paginationParameters)
            => new PagedList<Testimony>(
                _context.Testimonies
                    .OrderBy(te => te.Id),
                paginationParameters.PageNumber,
                paginationParameters.PageSize
            );

        public Testimony Create(Testimony item)
        {
            try {
                _context.Testimonies.Add(item);
                _context.SaveChanges();
                
            } catch(Exception) {
                throw;
            }

            return item;
        }

        public Testimony Update(Testimony item)
        {
            Testimony getItem = GetById(item.Id);

            if (getItem != null)
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
            Testimony getItem = GetById(id);

            if (getItem != null)
            {
                try {
                    _context.Testimonies.Remove(getItem);
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