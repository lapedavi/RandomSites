using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomSites.Controllers {
    public class DNDController : Controller {
        public IActionResult Index() {
            return View();
        }

        public IActionResult CharacterSheet() {
            return View();
        }
    }
}
