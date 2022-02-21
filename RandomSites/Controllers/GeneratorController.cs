using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomSites.Controllers {
    public class GeneratorController : Controller {
        public IActionResult Index() {
            return View();
        }

        public IActionResult RandomNumber() {
            return View(null);
        }

        [HttpPost]
        public IActionResult RandomNumber(int minValue, int maxValue) {
            Random r = new Random();
            int? retint = (int?)r.Next(minValue, maxValue + 1);
            return View(retint);
        }
    }
}
