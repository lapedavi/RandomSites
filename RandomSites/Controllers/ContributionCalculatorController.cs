using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using RandomSites.ContributionCalculator;

namespace RandomSites {
    public class ContributionCalculatorController : BaseController {
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
            ViewBag.TimeTaken = Calculate_ViewModel.sw.Elapsed.Minutes + " Minutes " + Calculate_ViewModel.sw.Elapsed.Seconds + " Seconds";
            return View("Report",userContributions);
        }
    }
}
