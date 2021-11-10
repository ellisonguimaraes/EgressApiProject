using System;
using System.Linq;
using EgressProject.API.Data;
using EgressProject.API.Models;
using EgressProject.API.Models.Utils;
using EgressProject.API.Repositories.Interfaces;

namespace EgressProject.API.Repositories
{
    public class HighlightsRepository : IEntityRepository<Highlights>
    {
        private readonly ApplicationDbContext _context;

        public HighlightsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Highlights GetById(int id)
            => _context.Highlights
                .Where(hi => hi.Id == id)
                .SingleOrDefault();

        public PagedList<Highlights> GetPaginate(PaginationParameters paginationParameters)
            => new PagedList<Highlights>(
                _context.Highlights
                    .OrderBy(hi => hi.Id),
                paginationParameters.PageNumber,
                paginationParameters.PageSize
            );

        public Highlights Create(Highlights item)
        {
            try {
                _context.Highlights.Add(item);
                _context.SaveChanges();

            } catch (Exception) {
                throw;
            }

            return item;
        }

        public Highlights Update(Highlights item)
        {
            Highlights getItem = GetById(item.Id);

            if (getItem != null)
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
            Highlights getItem = GetById(id);

            if (getItem != null)
            {
                try {
                    _context.Highlights.Remove(getItem);
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