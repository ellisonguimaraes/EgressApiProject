using System;
using System.Linq;
using EgressProject.API.Data;
using EgressProject.API.Models;
using EgressProject.API.Models.Utils;
using EgressProject.API.Repositories.Interfaces;

namespace EgressProject.API.Repositories
{
    public class PersonCourseRepository : IPersonCourseRepository
    {
        private readonly ApplicationDbContext _context;

        public PersonCourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public PersonCourse GetById(int personId, int courseId)
            => _context.PersonCourses
                .Where(pc => pc.PersonId == personId && pc.CourseId == courseId)
                .SingleOrDefault();

        public PagedList<PersonCourse> GetPaginate(PaginationParameters paginationParameters)
            => new PagedList<PersonCourse>(
                _context.PersonCourses
                    .OrderBy(ar => ar.PersonId)
                    .ThenBy(ar => ar.CourseId),
                paginationParameters.PageNumber,
                paginationParameters.PageSize
            );

        public PersonCourse Create(PersonCourse item)
        {
            try {
                _context.PersonCourses.Add(item);
                _context.SaveChanges();
                
            } catch(Exception) {
                throw;
            }
            
            return item;
        }

        public PersonCourse Update(PersonCourse item)
        {
            PersonCourse getItem = GetById(item.PersonId, item.CourseId);

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

        public bool Delete(int personId, int courseId)
        {
            PersonCourse getItem = GetById(personId, courseId);

            if (getItem != null)
            {
                try {
                    _context.PersonCourses.Remove(getItem);
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