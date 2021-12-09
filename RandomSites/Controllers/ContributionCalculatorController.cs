using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack; 

namespace RandomSites {
    public class ContributionCalculatorController : Controller {
        public IActionResult Index() {
            return View();
        }

        public IActionResult Calculate() {
            return View();
        }

        [HttpPost]
        public IActionResult Calculate(string url) {
            Dictionary<string,UserContribution> userContributions = Calculate_ViewModel.getContributions(url);
            decimal total = 0;
            foreach (UserContribution uc in userContributions.Values) {
                total += uc.Additions + uc.Deletions;
            }
            ViewBag.Total = total;
            return View("Report",userContributions);
        }
    }
}
