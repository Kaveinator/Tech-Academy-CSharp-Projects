using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack {
    public class Card {
        public readonly Deck Origin;
        public readonly Suit Suit;
        public readonly Face Face;

        public Card(Deck origin, Suit suit, Face face) {
            Origin = origin;
            Suit = suit;
            Face = face;
        }

        public override string ToString() => $"{Face} of {Suit}";
    }
}
