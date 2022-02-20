using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using RandomSites.CardGame.AutoWar;
using System.Threading.Tasks;

namespace RandomSites.Controllers {
    public class CardGameController : Controller {
        public IActionResult Index() {
            return View();
        }

        public IActionResult AutoWar() {
            Game g = new Game();
            return View();
        }
    }
}
