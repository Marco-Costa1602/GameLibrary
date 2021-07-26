using GameLibrary.Models;
using GameLibrary.Services;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : APIControllerBase
    {
        #region// DEPENDENCIES
        GameService _service;
        public GameController(GameService service)
        {
            _service = service;
        }
        #endregion

        #region// GET
        [HttpGet]
        public IActionResult Get() => ApiOk(_service.Get());

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id) => ApiOk(_service.Get(id));

        [HttpGet]
        [Route("{name}")]
        public IActionResult getByName(string name) => ApiOk(_service.Get(name));
        #endregion

        #region// CREATE
        [HttpPost]
        public IActionResult Create([FromBody]Game game)
        {
            return _service.Create(game) ?
                ApiOk(game) :
                ApiBadRequest<string>("Game already exists!");
        }
        #endregion

        #region// UPDATE
        [HttpPut]
        public IActionResult Update([FromBody] Game game)
        {
            return _service.Update(game) ?
                ApiOk("Game updated successfuly") :
                ApiBadRequest<string>("Game not found or invalid object");
        }
        #endregion

        #region// DELETE
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            return _service.Delete(id) ?
                ApiOk("Game deleted successfuly") :
                ApiNotFound<string>("Game doesn't exists");
        }
        #endregion


    }
}
