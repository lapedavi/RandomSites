using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomSites.CardGame.AutoWar {
    public class Game {

        private List<int> _deck;
        private List<Player> _players;

        public Game() {
            _deck = createDeck();
            _players = addPlayers();
            dealDeck();
        }

        private List<int> createDeck() {
            List<int> retList = new List<int>();
            for (int y = 1; y < 5; y++) {
                for (int x = 1; x < 14; x++) {
                    retList.Add(x);
                }
            }
            return retList;
        }

        private List<Player> addPlayers() {
            List<Player> retList = new List<Player>();
            retList.Add(new Player("Player 1"));
            retList.Add(new Player("Player 2"));
            return retList;
        }

        private void dealDeck() {
            Random r = new Random();
            List<int> player1 = new List<int>();
            List<int> player2 = new List<int>();
            while (_deck.Count != 0) {
                int cardIndex = r.Next(0, _deck.Count());
                player1.Add(_deck[cardIndex]);
                _deck.RemoveAt(cardIndex);

                cardIndex = r.Next(0, _deck.Count());
                player2.Add(_deck[cardIndex]);
                _deck.RemoveAt(cardIndex);
            }
            _players[0].CurrentDeck = player1;
            _players[1].CurrentDeck = player2;
        }

    }
}
