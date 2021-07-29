using GameLibrary.API;
using GameLibrary.Data;
using GameLibrary.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Services
{
    /// <summary>
    /// Service for Authorization and Authentication
    /// </summary>
    public class AuthService
    {
        #region// DEPENDENCIES
        UserManager<Client> _userManager;
        IConfiguration _configuration;
        /// <summary>
        /// Constructor for calling dependencies
        /// </summary>
        /// <param name="context">Calls the context class</param>
        /// <param name="userManager">Calls the UserManager service</param>
        /// <param name="configuration">Calls the configuration from the startup</param>
        public AuthService(GLContext context, UserManager<Client> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        #endregion

        #region// CREATE
        /// <summary>
        /// Creates a new Client (IdentityUser)
        /// </summary>
        /// <param name="client">Client object</param>
        /// <returns>Returns the result of the task</returns>
        public async Task<IdentityResult> Create(Client client)
        {
            client.Funds = 100;
            client.GameLibrary = null;
            var created = await _userManager.CreateAsync(client, client.PasswordHash);
            if (created.Succeeded == true)
            {
                _userManager.AddToRoleAsync(client, Enum.GetName(typeof(RoleTypes), 0)).Wait();
            }
            return created;
        }
        #endregion

        #region// VALIDATE
        /// <summary>
        /// Checks if the credentials are valid
        /// </summary>
        /// <param name="client">Client object</param>
        /// <returns>Returns a task boolean</returns>
        public async Task<bool> isValidLogin(Client client)
        {
            var user = await _userManager.FindByEmailAsync(client.Email);
            var roles = _userManager.GetRolesAsync(user).Result;
            if (roles.Count == 0) return false;
            return await _userManager.CheckPasswordAsync(user, client.PasswordHash);
        }
        #endregion

        #region// TOKEN
        /// <summary>
        /// Generates a token for the user
        /// </summary>
        /// <param name="client">Client object</param>
        /// <returns>Returs a token in string format</returns>
        public string generateToken(Client client)
        {
            if (!isValidLogin(client).Result) throw new Exception("Invalid Credentials");

            var user = _userManager.FindByEmailAsync(client.Email).Result;

            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var expireDate = DateTime.UtcNow.AddHours(2);

            var role = _userManager.GetRolesAsync(user).Result[0];

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Role, role),
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
