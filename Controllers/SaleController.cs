using GameLibrary.Models;
using GameLibrary.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace GameLibrary.Controllers
{
    /// <summary>
    /// Controller of Sales
    /// </summary>
    [Route("api/[controller]"), ApiController, Authorize]
    public class SaleController : APIControllerBase
    {
        #region// DEPENDENCIES
        SaleService _service;
        /// <summary>
        /// Sale Controller constructor that calls the sale service
        /// </summary>
        /// <param name="service">Service that manage the sales</param>
        public SaleController(SaleService service)
        {
            _service = service;
        }
        #endregion

        #region// GET
        /// <summary>
        /// Endpoint that gets ALL sales made
        /// </summary>
        /// <returns>Returns a List containing ALL sales made</returns>
        [HttpGet]
        public IActionResult Get() => ApiOk(_service.Get());

        /// <summary>
        /// Endpoint that gets all sales made by the current Client
        /// </summary>
        /// <returns>Returns a List of Sales of the user</returns>
        [HttpGet]
        [Route("userSales")]
        public IActionResult getSales()
        {
            var clientId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            return ApiOk(_service.Get(clientId));
        }
        #endregion

        #region // CREATE
        /// <summary>
        /// Endpoint that created a new sale for the current Client
        /// </summary>
        /// <param name="sale">Sale Object</param>
        /// <returns>Returns a message</returns>
        [HttpPost]
        public IActionResult Create([FromBody] Sale sale)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            sale.ClientId = userId;
            if(_service.Create(sale, userId))
            {
                return ApiOk("Sale made successfuly");
            }
            return ApiBadRequest("Not possible to complete the sale");
        }
        #endregion
    }
}
