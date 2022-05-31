using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DapperPostgreSQL;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using multi_tenant.DAO;
using multi_tenant.Models;
using Utility.Authorize.Helper;
using Utility.PasswordEncryption.Helper;

namespace multi_tenant.Services
{
    public interface IAuthService
    {
        Task<(bool,string,AuthenticationResponse)> Authentication(AuthenticationRequest param);
    }
    
    public class AuthService : IAuthService
    {
        private readonly SQLConn _db;
        private readonly AuthDao _authDao;
        private readonly JwtSettings _jwtSettings;

        public AuthService(SQLConn db, AuthDao authDao,
            IOptions<JwtSettings> jwtSettings)
        {
            this._db = db;
            this._authDao = authDao;
            this._jwtSettings = jwtSettings.Value;
        }
        
        public async Task<(bool,string,AuthenticationResponse)> Authentication(AuthenticationRequest param)
        {
            try
            {
                user_account user = await _authDao.Authentication(param);
                if (user is null)
                {
                    return (false,"", new AuthenticationResponse());
                }

                if (!PasswordHasher.Check(user.password, param.password))
                {
                    return (false,"", new AuthenticationResponse());
                }
                
                string token = this.generateJwtToken(user.id_user,user.id_tenant);

                return (true,"",new AuthenticationResponse()
                {
                    id_user = user.id_user,
                    email = user.email,
                    Token = token,
                    id_tenant = user.id_tenant
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        private string generateJwtToken(long id_user, long id_tenant)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);


            List<Claim> claims = new List<Claim>
            {
                new Claim("id", id_user.ToString()),
                new Claim("id_tenant", id_tenant.ToString())
            };
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}