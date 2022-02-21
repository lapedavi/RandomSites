using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomSites.Generator {
    public class Calculator {

        public static int? getInRange(int minValue, int maxValue) {
            Random r = new Random();
            int? retint = (int?)r.Next(minValue, maxValue + 1);
            return retint;
        }
    }
}
