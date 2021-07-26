using GameLibrary.Data;
using GameLibrary.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Services
{
    public class AuthService
    {
        UserManager<Client> _userManager;
        GLContext _context;
        IConfiguration _configuration;
        public AuthService(GLContext context,UserManager<Client> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _context = context;
            _configuration = configuration;
        }


        #region// CREATE
        public async Task<IdentityResult> Create(Client client)
        {
            client.Funds = 100;
            var created = await _userManager.CreateAsync(client, client.PasswordHash);
            if (created.Succeeded == true)
            {
                var x = _context.Clients.FirstOrDefault(c => c.Email == client.Email);
                x.Funds = 100;
            }
            return created;
        }
        #endregion

        #region// VALIDATE
        public async Task<bool> isValidLogin(Client client)
        {
            var user = await _userManager.FindByEmailAsync(client.Email);
            return await _userManager.CheckPasswordAsync(user, client.PasswordHash);
        }
        #endregion

        #region// TOKEN
        public string generateToken(Client client)
        {
            if (!isValidLogin(client).Result) throw new Exception("Invalid Credentials");

            var user = _userManager.FindByEmailAsync(client.Email).Result;
            //var role = _userManager.GetRolesAsync(user).Result[0];

            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var expireDate = DateTime.UtcNow.AddHours(2);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    //new Claim(ClaimTypes.Role, role)
                }),
                Expires = expireDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        #endregion

    }
}
