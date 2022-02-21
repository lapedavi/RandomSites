using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using RandomSites.Generator;
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
            return View(Calculator.getInRange(minValue, maxValue));
        }

        public IActionResult GenerateTo() {
            return View(null);
        }

        [HttpPost]
        public IActionResult GenerateTo(int targetSum, int numberAmonut) {
            int total = 0;
            int count = 0;
            List<int> numbersUsed = new List<int>();
            while (total != targetSum) {
                int secondHalf = (numberAmonut - count - 1);
                int value;
                if (secondHalf == 0) {
                    value = (targetSum - total);
                } else {
                    value = (int)Calculator.getInRange(0, (targetSum - total) - (numberAmonut - count - 1));
                }
                total += value;
                numbersUsed.Add(value);
                count++;
            }
            return View(numbersUsed);
        }
    }
}
