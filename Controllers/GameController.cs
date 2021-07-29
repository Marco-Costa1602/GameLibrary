using GameLibrary.API;
using GameLibrary.Models;
using GameLibrary.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace GameLibrary.Controllers
{
    /// <summary>
    /// Controlls all game related actions
    /// </summary>
    [Route("api/[controller]"), ApiController, Authorize]
    public class GameController : APIControllerBase
    {
        #region// DEPENDENCIES
        GameService _service;
        /// <summary>
        /// Calls the game service
        /// </summary>
        /// <param name="service">Service that manages games</param>
        public GameController(GameService service)
        {
            _service = service;
        }
        #endregion

        #region// GET
        /// <summary>
        /// Endpoint for getting all games
        /// </summary>
        /// <returns>Returns a list of games</returns>
        [HttpGet]
        public IActionResult Get() => ApiOk(_service.Get());

        /// <summary>
        /// Enpoint for getting a game based on its Id
        /// </summary>
        /// <param name="id">Game Id</param>
        /// <returns>Returns a Game Object</returns>
        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id) => ApiOk(_service.Get(id));

        /// <summary>
        /// Enpoint for getting a game based on its Name
        /// </summary>
        /// <param name="name">Game Name</param>
        /// <returns>Returns a Game Object</returns>
        [HttpGet]
        [Route("{name}")]
        public IActionResult getByName(string name) => ApiOk(_service.Get(name));

        /// <summary>
        /// Endpoint for getting games owned by the current user
        /// </summary>
        /// <returns>Returns a list of Game Objects</returns>
        [HttpGet]
        [Route("getCurrent")]
        public IActionResult getCurrent()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            return ApiOk(_service.getCurrent(userId), "You own the following games");
        }
        #endregion

        #region// CREATE
        /// <summary>
        /// Endpoint for creating a new Game Object
        /// </summary>
        /// <param name="game">Game Object</param>
        /// <returns>Returns the created game if successfuly created</returns>
        [HttpPost]
        [RoleAuthorize(RoleTypes.Admin)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Create([FromBody]Game game)
        {
            return _service.Create(game) ?
                ApiOk(game) :
                ApiBadRequest<string>("Game already exists!");
        }
        #endregion

        #region// UPDATE
        /// <summary>
        /// Enpoint for updated an existing game
        /// </summary>
        /// <param name="game">Game Object</param>
        /// <returns>The updated game or a fail message</returns>
        [HttpPut]
        [RoleAuthorize(RoleTypes.Admin)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Update([FromBody] Game game)
        {
            return _service.Update(game) ?
                ApiOk("Game updated successfuly") :
                ApiBadRequest<string>("Game not found or invalid object");
        }
        #endregion

        #region// DELETE
        /// <summary>
        /// Enpoint for deleting an existing game
        /// </summary>
        /// <param name="id">Game Id</param>
        /// <returns>Returns a message</returns>
        [HttpDelete]
        [Route("{id}")]
        [RoleAuthorize(RoleTypes.Admin)]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult Delete(int id)
        {
            return _service.Delete(id) ?
                ApiOk("Game deleted successfuly") :
                ApiNotFound<string>("Game doesn't exists");
        }
        #endregion
    }
}
