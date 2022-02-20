using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RandomSites.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RandomSites {
    public class HomeController : BaseController {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger) {
            _logger = logger;
        }

        public IActionResult Index() {
            //AddMessage(Message.type.Warning, "Warning Message");
            //AddMessage(Message.type.Error, "Error Message");
            //AddMessage(Message.type.Success, "Success Message");
            return View();
        }

        public IActionResult Privacy() {
            return View();
        }

        public IActionResult Sites() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
