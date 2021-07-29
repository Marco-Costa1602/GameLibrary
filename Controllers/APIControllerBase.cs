using GameLibrary.API;
using Microsoft.AspNetCore.Mvc;

namespace GameLibrary.Controllers
{
    /// <summary>
    /// Layer between the controller based. Used for customizing the ActionResult's messages
    /// </summary>
    public class APIControllerBase : ControllerBase
    {
        #region// CUSTOM RESPONSES
        /// <summary>
        /// Ok Message for get endpoints where there's only one return
        /// </summary>
        /// <typeparam name="T">Receives any type</typeparam>
        /// <param name="Results">Any type of object</param>
        /// <returns>Returna an OkObjectResult with a Custom message</returns>
        protected IActionResult ApiOk<T>(T Results) => Ok(CustomResponse(true, "", Results));

        /// <summary>
        /// Ok message for get endpoints where a message is also presented
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Results">Objet</param>
        /// <param name="Message">String message</param>
        /// <returns></returns>
        protected IActionResult ApiOk<T>(T Results, string Message) => Ok(CustomResponse(true, Message, Results));
        
        /// <summary>
        /// NotFound message for when a object isn't found
        /// </summary>
        /// <typeparam name="T">Type of param in the message</typeparam>
        /// <param name="Message">String message</param>
        /// <returns>Retuns a NotFoundObjectResult with a Custom message</returns>
        protected IActionResult ApiNotFound<T>(string Message) => NotFound(CustomResponse(false, Message, ""));
        
        /// <summary>
        /// BadRequest message for error
        /// </summary>
        /// <typeparam name="T">Used for displaying exceptions</typeparam>
        /// <param name="Results">Any type of object</param>
        /// <returns>Returns an BadRequest with a custom response that carries an exception</returns>
        protected IActionResult ApiBadRequest<T>(T Results) => BadRequest(CustomResponse(false, "", Results));
        #endregion

        #region// PRIVATE METHOD
        APIResponse<T> CustomResponse<T>(bool Succeed = true, string Message = "", T Results = default)
        {
            return new APIResponse<T>
            {
                Succeed = Succeed,
                Message = Message,
                Results = Results,
            };
        }
        #endregion
    }
}
