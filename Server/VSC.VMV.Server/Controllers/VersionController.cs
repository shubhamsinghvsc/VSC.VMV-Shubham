using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VSC.VMV.Common.Constants;

namespace VSC.VMV.Server.Controllers
{
    [Route(RouteMapConstants.BaseControllerRoute)]
    public class VersionController : Controller
    {
        private static Version? _version = null;

        public VersionController()
        {
            if (_version == null)
            {
                _version = GetType().Assembly.GetName().Version;
            }
        }

        [HttpGet]
        public string Default()
        {
            return "You have reached the VMV server.";
        }

        [HttpGet]
        [Route(nameof(GetVersion))]
        public IActionResult GetVersion()
        {
            try
            {
                if (_version != null)
                    return Ok(_version.ToString());
                else
                    return BadRequest("Getting Version is failed!");
            }
            catch
            { }
            return BadRequest();
        }
    }
}
