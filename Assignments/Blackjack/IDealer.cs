using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack {
    public interface IDealer {
        Deck Deck { get; }
    }
    public class BlackjackDealer : BlackjackPlayer, IDealer {
        public readonly Deck Deck;
        Deck IDealer.Deck => Deck; // Links to readonly deck
        public BlackjackDealer(Deck deck) : base("Dealer", 0) {
            Deck = deck;
        }

        public bool TryDeal(Player targetPlayer, out Card drawnCard) {
            if (!Deck.TryDequeue(out drawnCard)) {
                Console.WriteLine(CurrentGame.Logger.Log($"Dealer.TryDeal() Error: Could not dequeue card for {targetPlayer} (deck is likely out of cards)"));
                return false;
            }
            targetPlayer.Hand.Add(drawnCard);
            Console.WriteLine(CurrentGame.Logger.Log($"{targetPlayer.Name} got a {drawnCard}"));
            targetPlayer.EvaluateState();
            return true;
        }

        public override HitOrStay ShouldHitOrStay()
            => Blackjack.Rules.GetAllPossibleValues(Hand).Any(value => value < 17) ? HitOrStay.Hit : HitOrStay.Stay;
    }
}
