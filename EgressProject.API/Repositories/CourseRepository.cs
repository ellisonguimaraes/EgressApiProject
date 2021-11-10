using System;
using System.Linq;
using EgressProject.API.Data;
using EgressProject.API.Models;
using EgressProject.API.Models.Utils;
using EgressProject.API.Repositories.Interfaces;

namespace EgressProject.API.Repositories
{
    public class CourseRepository : IEntityRepository<Course>
    {
        private readonly ApplicationDbContext _context;

        public CourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Course GetById(int id)
            => _context.Courses
                .Where(co => co.Id == id)
                .SingleOrDefault();

        public PagedList<Course> GetPaginate(PaginationParameters paginationParameters)
            => new PagedList<Course>(
                _context.Courses
                    .OrderBy(co => co.Id),
                paginationParameters.PageNumber,
                paginationParameters.PageSize
            );

        public Course Create(Course item)
        {
            try {
                _context.Courses.Add(item);
                _context.SaveChanges();

            } catch(Exception) {
                throw;
            }

            return item;
        }  

        public Course Update(Course item)
        {
            Course getItem = GetById(item.Id);

            if(getItem != null){
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
            Course getItem = GetById(id);

            if(getItem != null){
                try {
                    _context.Courses.Remove(getItem);
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