using System;
using System.Linq;
using EgressProject.API.Data;
using EgressProject.API.Models;
using EgressProject.API.Models.Utils;
using EgressProject.API.Repositories.Interfaces;

namespace EgressProject.API.Repositories
{
    public class NewsRepository : IEntityRepository<News>
    {
        private readonly ApplicationDbContext _context;

        public NewsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public News GetById(int id)
            => _context.News
                .Where(ne => ne.Id == id)
                .SingleOrDefault();

        public PagedList<News> GetPaginate(PaginationParameters paginationParameters)
            => new PagedList<News>(
                _context.News
                    .OrderBy(ne => ne.Id),
                paginationParameters.PageNumber,
                paginationParameters.PageSize
            );
        
        public News Create(News item)
        {
            try {
                _context.News.Add(item);
                _context.SaveChanges();
                    
            } catch(Exception) {
                throw;
            }

            return item;
        }

        public News Update(News item)
        {
            News getItem = GetById(item.Id);

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
            News getItem = GetById(id);

            if (getItem != null) 
            {
                try {
                    _context.News.Remove(getItem);
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