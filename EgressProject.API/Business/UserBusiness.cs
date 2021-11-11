using System;
using EgressProject.API.Business.Interfaces;
using EgressProject.API.Models;
using EgressProject.API.Models.Utils;
using EgressProject.API.Repositories.Interfaces;
using EgressProject.API.Services.Auth;

namespace EgressProject.API.Business
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IAuthorizationRepository _authorizationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IJwTUtils _jwtUtils;

        public UserBusiness(IUserRepository userRepository, 
                            IAuthorizationRepository authorizationRepository, 
                            IJwTUtils jwtUtils)
        {
            _userRepository = userRepository;
            _authorizationRepository = authorizationRepository;
            _jwtUtils = jwtUtils;
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
                RefreshTokenExpiryTime = DateTime.Parse(token.ExpirationDate),
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
            authorization.RefreshTokenExpiryTime = DateTime.Parse(newToken.ExpirationDate);
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
    }
}