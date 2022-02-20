using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomSites.CardGame.AutoWar {
    public class Player {

        private string _Name;
        private List<int> _currentDeck;
        private List<int> _winnings = new List<int>();

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

        public int draw() {
            checkDeck();
            int card;
            if (CurrentDeck.Count() == 0) {
                card = -1;
            } else {
                card = CurrentDeck[0];
                _currentDeck.RemoveAt(0);
            }
            return card;
        }

        public void won(List<int> winnings) {
            _winnings.AddRange(winnings);
        }

        public bool lost() {
            bool decision = false;
            if (_currentDeck.Count <= 0 && _winnings.Count <= 0) {
                decision = true;
            }
            return decision;
        }

        public List<int> allCards() {
            List<int> allCards = new List<int>();
            allCards.AddRange(CurrentDeck);
            allCards.AddRange(_winnings);
            return allCards;
        }

        private void checkDeck() {
            if(_currentDeck.Count() <= 0) {
                _currentDeck.AddRange(_winnings);
                _winnings.Clear();
            }
        }

        public void deleteInvalids() {
            if (_currentDeck.Contains(-1)) {
                while (_currentDeck.Contains(-1)) {
                    _currentDeck.Remove(-1);
                }
            }
            if (_winnings.Contains(-1)) {
                while (_winnings.Contains(-1)) {
                    _winnings.Remove(-1);
                }
            }
        }
    }
}
