using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace RandomSites {
    public class Option : DatabaseRecordNamed {

        #region Constructors

        public Option() {

        }

        public Option(SqlDataReader dr) {
            Fill(dr);
        }

        #endregion

        #region Database Strings

        internal const string db_ID = "OptionID";
        internal const string db_Name = "Name";
        internal const string db_SurveyID = "SurveyID";

        #endregion

        #region Private Variables

        private int _SurveyID;
        private Survey _Survey;

        #endregion

        #region Public Properties

        public int SurveyID {
            get {
                return _SurveyID;
            }
            set {
                _SurveyID = value;
            }
        }

        #endregion

        #region Protected Functions

        protected override int dbAdd() {
            ID = -1; // DAL.AddOption(this);
            return ID;
        }

        protected override int dbUpdate() {
            ID = -1; // DAL.UpdateOption(this);
            return ID;
        }

        #endregion

        #region Public Functions

        public override int dbSave() {
            if (ID < 0) {
                return dbAdd();
            } else {
                return dbUpdate();
            }
        }

        public override string ToString() {
            return String.Format("{0} - {1}, {2}", this.ID, this.SurveyID, this.Name);
        }

        public override void Fill(SqlDataReader dr) {
            ID = (int)dr[db_ID];
            Name = (string)dr[db_Name];
            SurveyID = (int)dr[SurveyID];
        }

        #endregion
    }
}
