using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace RandomSites {
    public abstract class DatabaseRecord {

        private int _ID = -1;

        public int ID {
            get { return _ID; }
            set { _ID = value; }
        }

        public abstract int dbSave();

        protected abstract int dbAdd();

        protected abstract int dbUpdate();

        public abstract void Fill(SqlDataReader dr);

        public abstract override string ToString();
    }

    public abstract class DatabaseRecordNamed : DatabaseRecord {

        private string _Name;

        public string Name {
            get { return _Name; }
            set { _Name = value; }
        }
    }
}
