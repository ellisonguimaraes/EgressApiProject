using System;
using System.Linq;
using EgressProject.API.Data;
using EgressProject.API.Models;
using EgressProject.API.Models.Utils;
using EgressProject.API.Repositories.Interfaces;

namespace EgressProject.API.Repositories
{
    public class PersonRepository : IEntityRepository<Person>
    {
        private readonly ApplicationDbContext _context;

        public PersonRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Person GetById(int id)
            => _context.Persons
                .Where(pe => pe.Id == id)
                .SingleOrDefault();

        public PagedList<Person> GetPaginate(PaginationParameters paginationParameters)
            => new PagedList<Person>(
                _context.Persons
                    .OrderBy(pe => pe.Id),
                paginationParameters.PageNumber,
                paginationParameters.PageSize
            );

        public Person Create(Person item)
        {
            try {
                _context.Persons.Add(item);
                _context.SaveChanges();
                
            } catch(Exception) {
                throw;
            }

            return item;
        }

        public Person Update(Person item)
        {
            Person getItem = GetById(item.Id);

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
            Person getItem = GetById(id);

            if (getItem != null)
            {
                try {
                    _context.Persons.Remove(getItem);
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