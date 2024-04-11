using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Blackjack.Extensions;

namespace Blackjack {
    public enum Suit { Clubs, Hearts, Diamonds, Spades }
    public enum Face { Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace }
    public class Deck : List<Card> {
        readonly ReadOnlyCollection<Card> InitialState;
        public Deck() {
            InitialState = GetValues<Suit>()
                .SelectMany(suit => GetValues<Face>()
                .Select(face => new Card(this, suit, face))
            ).ToList().AsReadOnly();
            Recall();
        }

        /// <summary>Resets the deck to its initial state</summary>
        /// <returns></returns>
        public Deck Recall() {
            Clear();
            AddRange(InitialState);
            return this;
        }

        public Deck Shuffle(int count = 1) {
            // Move cards to to shuffler
            IEnumerable<Card> shuffledCards = ToArray(); // Essentially copy the cards in this array
            Clear(); // Since cards moved, clear current deck
            do {
                // Shuffle them
                shuffledCards = shuffledCards.OrderBy(elem => Guid.NewGuid()); // Guid.NewGuid() generates a random Guid, thereby shuffling the cards
            } while (--count > 0); // doWhile ensures this is ran at least once, even if count is negative
            AddRange(shuffledCards); // Put the cards back after shuffling
            return this;
        }

        public bool TryPeek(out Card card) {
            card = Count == 0 ? null : this[0];
            return card != null;
        }

        public bool TryDequeue(out Card card) {
            bool gotCard = TryPeek(out card);
            if (gotCard) RemoveAt(0);
            return gotCard;
        }
    }
    public class Hand : List<Card> {
        public int Value => 0;
    }
}
