using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChristmasMothers.Web.Api.Private.Controllers
{
    [Route("api/health")]
    [AllowAnonymous]
    public class HealthCheckController : Controller
    {
       
        /// <summary>
        /// heath check endpoint
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ApiExplorerSettings(GroupName = "private", IgnoreApi = true)]
        public OkResult HealthCheck()
        {
            return Ok();
        }

        //TODO : FM -- endpoint fordatabase health

    }
}
