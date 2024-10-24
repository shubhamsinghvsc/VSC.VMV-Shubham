using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using VSC.VMV.Common.Constants;
using VSC.VMV.Common.Helper;
using VSC.VMV.Common.Models;

namespace VSC.VMV.Server.Controllers
{
    [Route(RouteMapConstants.BaseControllerRoute)]
    public class AppInteractionsController : Controller
    {
        private readonly ILogger _logger;

        public AppInteractionsController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<PeopleManagementController>();
        }

        [HttpGet]
        public string Default()
        {
            return "You have reached the VMV App interactions server.";
        }

        [HttpGet]
        [Route(nameof(GetAvailableCultures))]
        public IActionResult GetAvailableCultures()
        {
            try
            {
                return Ok(VMVCultureHelper.GetAvailableEnglishCultures());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
