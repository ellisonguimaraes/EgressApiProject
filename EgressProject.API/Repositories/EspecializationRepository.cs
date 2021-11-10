using System;
using System.Linq;
using EgressProject.API.Data;
using EgressProject.API.Models;
using EgressProject.API.Models.Utils;
using EgressProject.API.Repositories.Interfaces;

namespace EgressProject.API.Repositories
{
    public class EspecializationRepository : IEntityRepository<Especialization>
    {
        private readonly ApplicationDbContext _context;
        
        public EspecializationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Especialization GetById(int id)
            => _context.Especializations
                .Where(es => es.Id == id)
                .SingleOrDefault();

        public PagedList<Especialization> GetPaginate(PaginationParameters paginationParameters)
            => new PagedList<Especialization>(
                _context.Especializations
                    .OrderBy(es => es.Id),
                paginationParameters.PageNumber,
                paginationParameters.PageSize
            );

        public Especialization Create(Especialization item)
        {
            try {
                _context.Especializations.Add(item);
                _context.SaveChanges(); 

            } catch (Exception) {
                throw;
            }

            return item;
        }

        public Especialization Update(Especialization item)
        {
            Especialization getItem = GetById(item.Id);

            if(getItem != null)
            {
                try {
                    _context.Entry(getItem).CurrentValues.SetValues(item);
                    _context.SaveChanges(); 

                } catch (Exception) {
                    throw;
                }
            }

            return item;
        }

        public bool Delete(int id)
        {
            Especialization getItem = GetById(id);

            if(getItem != null)
            {
                try {
                    _context.Especializations.Remove(getItem);
                    _context.SaveChanges(); 
                    return true;

                } catch (Exception) {
                    throw;
                }
            }

            return false;
        }
    }
}