using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomSites.CardGame.AutoWar {
    public class Game {

        private List<int> _deck;
        private List<Player> _players;
        private Dictionary<int, (int, int)> _rounds = new Dictionary<int, (int, int)>();

        public Game() {
            _deck = createDeck();
            _players = addPlayers();
            dealDeck();
            play();
        }

        public Dictionary<int,(int,int)> Rounds {
            get {
                return _rounds;
            }
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

        private void play() {
            int currentRound = 1;
            while (!_players[0].lost() && !_players[1].lost()) {
                int player1 = _players[0].draw();
                if(player1 == -1) {
                    _players[1].won(new List<int> { player1 });
                }

                int player2 = _players[1].draw();
                if(player2 == -1) {
                    _players[0].won(new List<int> { player1, player2 });
                }

                List<int> cardsInPlay = new List<int> { player1, player2 };
                determine(player1, player2, cardsInPlay);
                _rounds.Add(currentRound, (_players[0].allCards().Count(), _players[1].allCards().Count()));
                currentRound++;
            }
        }

        private List<int> determine(int player1, int player2, List<int> cardsInPlay) {
            if (player1 > player2) {
                _players[0].won(cardsInPlay);
            } else if (player2 > player1) {
                _players[1].won(cardsInPlay);
            } else {
                for (int x = 0; x < 3; x++) {
                    cardsInPlay.AddRange(new List<int> { _players[0].draw(), _players[1].draw() });
                }
                int player1War = _players[0].draw();
                int player2War = _players[1].draw();
                cardsInPlay.AddRange(new List<int> { player1War, player2War });
                determine(player1War, player2War, cardsInPlay);
            }
            return cardsInPlay;
        }
    }
}
