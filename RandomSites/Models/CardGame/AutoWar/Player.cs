using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomSites.CardGame.AutoWar {
    public class Player {

        private string _Name;
        private List<int> _currentDeck;
        private List<int> _winnings;

        public Player(string name) {
            _Name = name;
        }

        public List<int> CurrentDeck {
            set {
                _currentDeck = value;
            }
            get {
                return _currentDeck;
            }
        }
    }
}
