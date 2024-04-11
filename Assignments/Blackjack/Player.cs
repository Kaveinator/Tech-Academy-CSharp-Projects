using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Blackjack.Blackjack.Rules;

namespace Blackjack {
    public enum BlackjackState { None, Blackjack, Busted }
    public abstract class Player {
        public readonly string Name;
        public readonly List<Card> Hand;
        public int Balance;
        public bool IsActivelyPlaying;

        public Player(string name, int balance) {
            Name = name;
            Hand = new List<Card>();
            Balance = balance;
        }

        public void Deal() {

        }

        public bool TryBet(int ammt) {
            if (Balance < ammt) {
                Console.WriteLine("You do not have enough to place this bet");
                return false;
            }
            Balance -= ammt;
            return true;
        }

        public abstract void EvaluateState();

        public override string ToString() => Name;
    }
    public class BlackjackPlayer : Player {
        public BlackjackState State { get; protected set; }
        public Blackjack CurrentGame { get; protected set; }
        public BlackjackPlayer(string name, int balance) : base(name, balance) {
            Reset(null);
        }

        public virtual HitOrStay ShouldHitOrStay() => ConsoleUtils.ReadEnum<HitOrStay>("Hit or stay?");

        public void Reset(Blackjack bindToGame) {
            CurrentGame = bindToGame;
            Hand.Clear();
        }

        public override void EvaluateState() {
            BlackjackState oldState = State;
            State = IsBlackjack(this) ? BlackjackState.Blackjack
                : IsBusted(this) ? BlackjackState.Busted
                : BlackjackState.None;
            if (oldState != State)
                CurrentGame.Logger.Log($"{this}'s state changed from '{oldState}' to '{State}'");
        }
    }
}
