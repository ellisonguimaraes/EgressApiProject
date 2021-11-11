using System.Text;
using System.Security.Cryptography;
using System;
using System.Linq;
using EgressProject.API.Data;
using EgressProject.API.Models;
using EgressProject.API.Models.Utils;
using EgressProject.API.Repositories.Interfaces;

namespace EgressProject.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public User GetById(int id)
            => _context.Users
                .Where(user => user.Id == id)
                .SingleOrDefault();

        public PagedList<User> GetPaginate(PaginationParameters paginationParameters)
            => new PagedList<User>(
                _context.Users
                    .OrderBy(user => user.Id),
                paginationParameters.PageNumber,
                paginationParameters.PageSize
            );

        public User GetByEmail(string email)
            => _context.Users
                .Where(user => user.Email.ToLower().Equals(email.ToLower()))
                .SingleOrDefault();

        public User GetByLogin(string email, string password)
        {
            var passwordEncripted = new SHA256CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(password));
            return _context.Users
                .Where(user => user.Email.ToLower().Equals(email.ToLower()) 
                                && user.Password.Equals(BitConverter.ToString(passwordEncripted)))
                .SingleOrDefault();
        }

        public User Create(User item)
        {
            try {
                _context.Users.Add(item);
                _context.SaveChanges();
            } catch(Exception) {
                throw;
            }

            return item;
        }

        public User Update(User item)
        {
            User getItem = GetById(item.Id);

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
            User getItem = GetById(id);

            if (getItem != null)
            {
                try {
                    _context.Users.Remove(getItem);
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