using GameLibrary.Models;
using GameLibrary.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : APIControllerBase
    {
        #region// DEPENDENCIES
        AuthService _authService;
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }
        #endregion

        #region// REGISTER
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

        [HttpPost]
        [Route("Token")]
        public IActionResult generateToken([FromBody]Client client, [FromBody)
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


    }
}
