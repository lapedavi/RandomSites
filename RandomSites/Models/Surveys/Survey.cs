using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace RandomSites {
    public class Survey : DatabaseRecordNamed {

        #region Constructors

        public Survey() {

        }

        public Survey(SqlDataReader dr) {
            Fill(dr);
        }

        #endregion

        #region Database Strings

        internal const string db_ID = "SurveyID";
        internal const string db_Name = "Name";
        internal const string db_Description = "Description";
        internal const string db_NumOfOptions = "NumOfOptions";
        internal const string db_MultipleSelections = "MultipleSelections";

        #endregion

        #region Private Variables

        private string _Description;
        private int _NumOfOptions;
        private bool _MultipleSelections;
        private List<Option> _Options;

        #endregion

        #region Public Properties

        public string Description {
            get {
                return _Description;
            }
            set {
                _Description = value;
            }
        }

        public int NumOfOptions {
            get {
                return _NumOfOptions;
            }
            set {
                _NumOfOptions = value;
            }
        }

        public bool MulitpleSelections {
            get {
                return _MultipleSelections;
            }
            set {
                _MultipleSelections = value;
            }
        }

        #endregion

        #region Protected Functions

        protected override int dbAdd() {
            ID = -1; // DAL.AddSurvey(this);
            return ID;
        }

        protected override int dbUpdate() {
            ID = -1; // DAL.UpdateSurvey(this);
            return ID;
        }

        #endregion

        #region Public Functions

        public override int dbSave() {
            if(ID < 0) {
                return dbAdd();
            } else {
                return dbUpdate();
            }
        }

        public override string ToString() {
            return String.Format("{0} - {1}, {2}", this.ID, this.Name, this.Description);
        }

        public override void Fill(SqlDataReader dr) {
            ID = (int)dr[db_ID];
            Name = (string)dr[db_Name];
            Description = (string)dr[db_Description];
            NumOfOptions = (int)dr[db_NumOfOptions];
            MulitpleSelections = (bool)dr[db_MultipleSelections];

        }

        #endregion
    }
}
