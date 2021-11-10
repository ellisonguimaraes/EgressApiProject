using System;
using System.Linq;
using EgressProject.API.Data;
using EgressProject.API.Models;
using EgressProject.API.Models.Utils;
using EgressProject.API.Repositories.Interfaces;

namespace EgressProject.API.Repositories
{
    public class AreaRepository : IAreaRepository
    {
        private readonly ApplicationDbContext _context;

        public AreaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Area GetById(int courseId, int jobId)
            => _context.Areas
                .Where(ar => ar.CourseId == courseId && ar.JobId == jobId)
                .SingleOrDefault();

        public PagedList<Area> GetPaginate(PaginationParameters paginationParameters)
            => new PagedList<Area>(
                _context.Areas
                    .OrderBy(ar => ar.CourseId)
                    .ThenBy(ar => ar.JobId),
                paginationParameters.PageNumber,
                paginationParameters.PageSize
            );
        
        public Area Create(Area item)
        {
            try {
                _context.Areas.Add(item);
                _context.SaveChanges();
                
            } catch(Exception) {
                throw;
            }

            return item;
        }

        public Area Update(Area item)
        {
            Area getItem = GetById(item.CourseId, item.JobId);

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

        public bool Delete(int courseId, int jobId)
        {
            Area getItem = GetById(courseId, jobId);

            if (getItem != null)
            {
                try {
                    _context.Areas.Remove(getItem);
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