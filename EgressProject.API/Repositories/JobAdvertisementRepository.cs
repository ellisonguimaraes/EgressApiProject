using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EgressProject.API.Data;
using EgressProject.API.Models;
using EgressProject.API.Models.Utils;
using EgressProject.API.Repositories.Interfaces;

namespace EgressProject.API.Repositories
{
    public class JobAdvertisementRepository : IEntityRepository<JobAdvertisement>
    {
        private readonly ApplicationDbContext _context;
        
        public JobAdvertisementRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public JobAdvertisement GetById(int id)
            => _context.JobAdvertisements
                .Where(jo => jo.Id == id)
                .SingleOrDefault();

        public PagedList<JobAdvertisement> GetPaginate(PaginationParameters paginationParameters)
            => new PagedList<JobAdvertisement>(
                _context.JobAdvertisements
                    .OrderBy(jo => jo.Id),
                paginationParameters.PageNumber,
                paginationParameters.PageSize
            );

        public JobAdvertisement Create(JobAdvertisement item)
        {
            try {
                _context.JobAdvertisements.Add(item);
                _context.SaveChanges();

            } catch(Exception) {
                throw;
            }

            return item;
        }

        public JobAdvertisement Update(JobAdvertisement item)
        {
            JobAdvertisement getItem = GetById(item.Id);

            if (getItem != null) {
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
            JobAdvertisement getItem = GetById(id);

            if (getItem != null) {
                try {
                    _context.JobAdvertisements.Remove(getItem);
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