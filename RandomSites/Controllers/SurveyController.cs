using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomSites {
    public class SurveyController : BaseController {
        public IActionResult Index() {
            return View();
        }
    }
}
