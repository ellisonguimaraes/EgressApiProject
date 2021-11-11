using System;
using EgressProject.API.Repositories.Interfaces;
using EgressProject.API.Models.Utils;
using EgressProject.API.Models;
using EgressProject.API.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EgressProject.API.Repositories
{
    public class AuthorizationRepository : IAuthorizationRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthorizationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Authorization GetByRefreshToken(string refreshToken)
            => _context.Authorizations
                .Include(au => au.User)
                .Where(au => au.RefreshToken.Equals(refreshToken))
                .SingleOrDefault();

        public Authorization GetById(int id) 
            => _context.Authorizations
                .Include(au => au.User)
                .Where(au => au.Id == au.Id)
                .SingleOrDefault();
        
        public PagedList<Authorization> GetPaginate(PaginationParameters paginationParameters)
            => new PagedList<Authorization>(
                _context.Authorizations
                    .Include(au => au.User)
                    .OrderBy(au => au.Id),
                paginationParameters.PageNumber,
                paginationParameters.PageSize
            );

        public Authorization Create(Authorization item)
        {
            try {
                _context.Authorizations.Add(item);
                _context.SaveChanges();

            } catch(Exception) {
                throw;
            }

            return item;
        }

        public Authorization Update(Authorization item)
        {
            Authorization getItem = GetById(item.Id);

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
            Authorization getItem = GetById(id);

            if (getItem != null)
            {
                try {
                    _context.Authorizations.Remove(getItem);
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