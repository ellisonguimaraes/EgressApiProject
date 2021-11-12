using System;
using System.Text;
using EgressProject.API.Business.Interfaces;
using EgressProject.API.Models;
using EgressProject.API.Models.InputModel;
using EgressProject.API.Models.Utils;
using EgressProject.API.Repositories.Interfaces;
using EgressProject.API.Services.Auth;
using System.Security.Cryptography;
using EgressProject.API.Models.Enums;

namespace EgressProject.API.Business
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IAuthorizationRepository _authorizationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IJwTUtils _jwtUtils;
        private readonly TokenConfiguration _tokenConfiguration;

        public UserBusiness(IUserRepository userRepository, 
                            IAuthorizationRepository authorizationRepository, 
                            IJwTUtils jwtUtils,
                            TokenConfiguration tokenConfiguration)
        {
            _userRepository = userRepository;
            _authorizationRepository = authorizationRepository;
            _jwtUtils = jwtUtils;
            _tokenConfiguration = tokenConfiguration;
        }
        
        public Token Authenticate(Login login, string ipAddress)
        {
            User user = _userRepository.GetByLogin(login.Email, login.Password);

            if (user == null) return null;

            Token token = _jwtUtils.GenerateToken(user);

            Authorization authorization = new Authorization{
                Token = token.AccessToken,
                IpAddress = ipAddress,
                CreatedDate = DateTime.Parse(token.CreatedDate),
                RefreshToken = token.RefreshToken,
                RefreshTokenExpiryTime = DateTime.Now.AddDays(_tokenConfiguration.DaysToExpiry),
                IsValid = true,
                UserId = user.Id
            };
            _authorizationRepository.Create(authorization);

            return token;
        }

        public Token RefreshToken(Token token, string ipAddress)
        {
            Authorization authorization = _authorizationRepository.GetByRefreshToken(token.RefreshToken);

            if (authorization == null || authorization.RefreshTokenExpiryTime <= DateTime.Now || authorization.IsValid == false)
                return null;

            Token newToken = _jwtUtils.GenerateToken(authorization.User);

            authorization.Token = newToken.AccessToken;
            authorization.IpAddress = ipAddress;
            authorization.CreatedDate = DateTime.Parse(newToken.CreatedDate);
            authorization.RefreshToken = newToken.RefreshToken;
            authorization.RefreshTokenExpiryTime = DateTime.Now.AddDays(_tokenConfiguration.DaysToExpiry);
            authorization.IsValid = true;
            authorization.UserId = authorization.User.Id;

            _authorizationRepository.Update(authorization);

            return newToken;
        }

        public bool RevokeToken(Token token)
        {
            Authorization authorization = _authorizationRepository.GetByRefreshToken(token.RefreshToken);

            if (authorization == null)
                return false;

            authorization.IsValid = false;
            _authorizationRepository.Update(authorization);
            
            return true;
        }
        
        public User Register(RegisterInputModel registerInputModel)
        {
            if(_userRepository.GetByEmail(registerInputModel.Email) != null) return null;

            User user = _userRepository.Create(new User{
                Email = registerInputModel.Email,
                Password = BitConverter.ToString(new SHA256CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(registerInputModel.Password))),
                Role = Role.Egress,
                IsValidated = true
            });

            return user;
        }

        public PagedList<User> GetPaginate(PaginationParameters paginationParameters)
            => _userRepository.GetPaginate(paginationParameters);

        public User GetById(int id)
            => _userRepository.GetById(id);

        public User Update(User user)
            => _userRepository.Update(user);

        public bool Delete(int id)
            => _userRepository.Delete(id);
    }
}