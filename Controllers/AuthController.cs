using GameLibrary.Models;
using GameLibrary.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GameLibrary.Controllers
{
    /// <summary>
    /// Controller of the Client Authentication
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : APIControllerBase
    {
        #region// DEPENDENCIES
        AuthService _authService;
        /// <summary>
        /// Calls the AuthService
        /// </summary>
        /// <param name="authService">Auth Service</param>
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }
        #endregion

        #region// REGISTER
        /// <summary>
        /// Endpoint for registering a new Client
        /// </summary>
        /// <param name="client">Client object</param>
        /// <returns>Returns the created client</returns>
        [HttpPost]
        [Route("Register")]
        public IActionResult Register([FromBody]Client client)
        {
            try
            {
                var result = _authService.Create(client).Result;
                if (result.Succeeded)
                {
                    client.PasswordHash = default;
                    client.SecurityStamp = default;
                    client.ConcurrencyStamp = default;
                    return Ok();
                }
                return BadRequest();
            }
            catch(Exception e)
            {
                return ApiBadRequest<string>(e.Message);
            };
        }
        #endregion

        #region// TOKEN
        /// <summary>
        /// Endpoint for generating an Auth Token for the Client
        /// </summary>
        /// <param name="client">Client Object</param>
        /// <returns>Returns a token string</returns>
        [HttpPost]
        [Route("Token")]
        public IActionResult generateToken([FromBody]Client client)
        {
            try
            {
                return ApiOk(_authService.generateToken(client));

            }
            catch(Exception e)
            {
                return ApiBadRequest<string>(e.Message);
            }
        }
        #endregion
    }
}
