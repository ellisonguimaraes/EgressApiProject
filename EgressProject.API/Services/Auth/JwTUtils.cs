using System.Security.Cryptography;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using EgressProject.API.Models;
using EgressProject.API.Models.Utils;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace EgressProject.API.Services.Auth
{
    public class JwTUtils : IJwTUtils
    {
        private readonly TokenConfiguration _tokenConfiguration;

        public JwTUtils(TokenConfiguration tokenConfiguration)
        {
            _tokenConfiguration = tokenConfiguration;
        }

        public Token GenerateToken(User user)
        {
            // Gerando novos tokens
            string accessToken = GenerateAccessToken(user);
            string refreshToken = GenerateRefreshToken();

            // Gerando data de criação/expiração do Token de Acesso
            DateTime createdDate = DateTime.Now;
            DateTime expirationDate = createdDate.AddMinutes(_tokenConfiguration.Minutes);

            return new Token{
                Authenticated = true,
                CreatedDate = createdDate.ToString("yyyy-MM-dd HH:mm:ss"),
                ExpirationDate = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public string GenerateAccessToken(User user)
        {
            // Obtendo Secret do appsettings.json
            SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenConfiguration.Secret));

            // Definindo SigningCredentials
            SigningCredentials signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            // Definindo Claims
            List<Claim> claims = new List<Claim>{
                new Claim("id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            // Definindo a descrição do Token
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                Audience = _tokenConfiguration.Audience,
                Issuer = _tokenConfiguration.Issuer,
                Expires = DateTime.UtcNow.AddMinutes(_tokenConfiguration.Minutes),
                SigningCredentials = signingCredentials
            };

            // Gerando token
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Retornando o token em string
            return tokenHandler.WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            string refreshToken = null;
            
            using(var rng = RandomNumberGenerator.Create())
            {   
                rng.GetBytes(randomNumber);
                refreshToken = Convert.ToBase64String(randomNumber);
            }

            return refreshToken;
        }

        public int? ValidateJwTToken(string token)
        {
            if(token == null) return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_tokenConfiguration.Secret);

            try{
                tokenHandler.ValidateToken(token, new TokenValidationParameters {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateLifetime = false
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                return userId;

            } catch(Exception) {
                return null;
            }
        }
    }
}