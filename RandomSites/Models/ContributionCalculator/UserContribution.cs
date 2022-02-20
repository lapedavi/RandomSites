using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomSites.ContributionCalculator {
    public class UserContribution {

        #region Private Variables

        private string _User;
        private int _Commits = 0;
        private int _Additions = 0;
        private int _Deletions = 0;

        #endregion

        #region Constructor

        public UserContribution(){}

        public UserContribution(string user) {
            _User = user;
        }

        #endregion

        #region Public Properties

        public string User {
            get {
                return _User;
            }
        }

        public int Commits {
            get {
                return _Commits;
            }
        }

        public int Additions {
            get {
                return _Additions;
            }
        }

        public int Deletions {
            get {
                return _Deletions;
            }
        }

        #endregion

        #region Public Functions

        public decimal getPercentOfTotal(decimal total) {
            return ((Additions + Deletions) / total);
        }

        public void incrementCommit() {
            _Commits++;
        }

        public void increaseAdditions(int amount) {
            _Additions += amount;
        }

        public void increaseDeletions(int amount) {
            _Deletions += amount;
        }

        #endregion
    }
}
