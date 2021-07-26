using GameLibrary.API;
using Microsoft.AspNetCore.Mvc;

namespace GameLibrary.Controllers
{
    public class APIControllerBase : ControllerBase
    {
        #region// CUSTOM RESPONSES
        protected IActionResult ApiOk<T>(T Results) => Ok(CustomResponse(true, "", Results));
        protected IActionResult ApiOk<T>(T Results, string Message) => Ok(CustomResponse(true, Message, Results));
        protected IActionResult ApiNotFound<T>(string Message) => NotFound(CustomResponse(false, Message, ""));
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
