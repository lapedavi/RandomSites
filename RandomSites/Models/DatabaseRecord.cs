using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomSites {
    public class DatabaseRecord {

        private int _ID;

        public int ID {
            get {
                return _ID;
            }
        }
    }

    public class DatabaseRecordNamed : DatabaseRecord {

        private string _Name;

        public string Name {
            get {
                return _Name;
            }
        }
    }
}
