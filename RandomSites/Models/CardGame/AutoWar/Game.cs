using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomSites.CardGame.AutoWar {

    /// <summary>
    /// Class representing the simulated game of war
    /// </summary>
    public class Game {

        //List of all cards in a normal deck
        private List<int> _deck;

        //List of players in the game
        private List<Player> _players;

        //Dictionary of rounds inlcuding round number, and players card counts
        private Dictionary<int, (int, int)> _rounds = new Dictionary<int, (int, int)>();

        /// <summary>
        /// Initializes the game
        /// </summary>
        public Game() {

            //Creates deck of cards
            _deck = createDeck();

            //Creates Player List
            _players = addPlayers();

            //Deals deck out to players
            dealDeck();

            //Runs the game simulation
            play();
        }

        /// <summary>
        /// Get rounds for Game
        /// </summary>
        public Dictionary<int, (int, int)> Rounds {
            get {
                return _rounds;
            }
        }

        /// <summary>
        /// Creates deck of 1-13 representing Ace to King, does so 4 times representing 4 suits
        /// </summary>
        /// <returns>
        /// Returns a List of cards, 52 in total
        /// </returns>
        private List<int> createDeck() {

            //Empty List to Return
            List<int> retList = new List<int>();

            //For each suit
            for (int y = 1; y < 5; y++) {

                //For each card in each suit
                for (int x = 1; x < 14; x++) {

                    //Add all to return list
                    retList.Add(x);
                }
            }

            //Return List
            return retList;
        }

        /// <summary>
        /// Creates 2 players for the simulation
        /// </summary>
        /// <returns>
        /// Returns list of newly created players
        /// </returns>
        private List<Player> addPlayers() {

            //Return List
            List<Player> retList = new List<Player>();

            //Create player with name Player 1
            retList.Add(new Player("Player 1"));

            //Create player with name Player 2
            retList.Add(new Player("Player 2"));

            //Return List
            return retList;
        }

        /// <summary>
        /// Deals deck out to the players
        /// </summary>
        private void dealDeck() {

            //Random Object
            Random r = new Random();

            //Creates List of cards that will be each players decks
            List<int> player1 = new List<int>();
            List<int> player2 = new List<int>();

            //While the deck has cards
            while (_deck.Count != 0) {

                //get Card index between 0 and the number of available cards
                int cardIndex = r.Next(0, _deck.Count());

                //Give card at index to the player
                player1.Add(_deck[cardIndex]);

                //Remove that card from the deck
                _deck.RemoveAt(cardIndex);

                //get Card index between 0 and the number of available cards
                cardIndex = r.Next(0, _deck.Count());

                //Give card at index to the player
                player2.Add(_deck[cardIndex]);

                //Remove that card from the deck
                _deck.RemoveAt(cardIndex);
            }

            //Give players their deck of cards
            _players[0].CurrentDeck = player1;
            _players[1].CurrentDeck = player2;
        }

        /// <summary>
        /// Simulate game of war
        /// </summary>
        private void play() {

            //Starting round
            int currentRound = 1;

            //While the players have not lost
            while (!_players[0].lost() && !_players[1].lost()) {

                //Draw card from player 1 deck
                int player1 = _players[0].draw();

                //Draw cards form player 2 deck
                int player2 = _players[1].draw();

                //Detemine result of both cards
                determine(player1, player2);

                //Add round containing current round and the count of both players cards
                foreach(Player p in _players) {
                    p.deleteInvalids();
                }
                _rounds.Add(currentRound, (_players[0].allCards().Count(), _players[1].allCards().Count()));

                //Increment rounds
                currentRound++;
            }
        }

        /// <summary>
        /// Determine the result of the comparison of the two players cards
        /// </summary>
        /// <param name="player1">
        /// Player 1s card
        /// </param>
        /// <param name="player2">
        /// Player 2s card
        /// </param>
        /// <param name="cardsInPlay">
        /// cards currently in play if any
        /// </param>
        private void determine(int player1, int player2, List<int> cardsInPlay = null) {

            //If lists is null add cards to a new list
            if (cardsInPlay == null) {
                cardsInPlay = new List<int> { player1, player2 };
            } 

            //If player1s card is higher than player2s card, player1 wins this round
            if (player1 > player2) {
                _players[0].won(cardsInPlay);
            }
            //If player2s card is higher than player1s card, player1 wins this round
            else if (player2 > player1) {
                _players[1].won(cardsInPlay);
            }
            //If the cards are equal, a war is triggered
            else {

                //Take top three cards from the deck and put them into play
                for (int x = 0; x < 3; x++) {
                    cardsInPlay.AddRange(new List<int> { _players[0].draw(), _players[1].draw() });
                }

                //Player 1 war card
                int player1War = _players[0].draw();

                //Player 2 war card
                int player2War = _players[1].draw();

                //Put played cards into the list
                cardsInPlay.AddRange(new List<int> { player1War, player2War });

                //Determine winner
                determine(player1War, player2War, cardsInPlay);
            }

            //Return once complete
            return;
        }
    }
}
