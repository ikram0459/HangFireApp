using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using INService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace HangFireApp.Controllers
{
    public class HangFireController : Controller
    {
        private readonly IINService _inService;
        public HangFireController(IINService inservice)
        {
            this._inService = inservice;
        }
        [Route("Api/HangFire")]
        public IActionResult Index()
        {
            RecurringJob.AddOrUpdate("TestJob1", () => this._inService.RunTask(), Cron.Minutely);
            return Ok();
        }
    }
}
