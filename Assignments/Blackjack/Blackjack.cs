using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Blackjack.ConsoleUtils;
using static Blackjack.Blackjack.Rules;

namespace Blackjack {
    public class Blackjack : Game<Blackjack, BlackjackPlayer>, IWalkAway {
        public readonly Deck Deck = new Deck();
        public BlackjackDealer Dealer;
        public readonly LogFile Logger;

        public Blackjack(LogFile logFile) : base("Blackjack") {
            Dealer = new BlackjackDealer(Deck);
            Logger = logFile;
        }

        public override void Play() {
            // Ensure the dealer is last one dealing
            if (Players.Contains(Dealer))
                Players.Remove(Dealer);
            Players.Add(Dealer);

            Players.Do(plr => plr.Reset(this));
            Deck.Recall();
            Deck.Shuffle(3);
            Bets.Clear();

            Players.ForEach(player => {
                if (player == Dealer) return;
                byte bet = ReadNumeral<byte>(
                    prompt:   $"{player}, place your bet:",
                    minValue: 1,
                    maxValue: player.Balance <= byte.MaxValue ? (byte)player.Balance : byte.MaxValue
                );
                player.Balance -= bet;
                //if (!player.TryBet(bet)) return;
                Bets.Add(player, bet);
            });
            // Give out the cards
            Console.WriteLine("Giving out cards...");
            foreach (byte i in new byte[] { 0, 1 }) {
                foreach (BlackjackPlayer player in Players) {
                    if (!Dealer.TryDeal(player, out Card drawnCard)) {
                        Console.WriteLine("Could not get a card from deck, were the cards recalled?");
                        continue;
                    }
                    //Console.WriteLine($"{player} got a {drawnCard}");
                    if (i == 0 || player.State == BlackjackState.None) continue;
                    if (player.State == BlackjackState.Blackjack) {
                        Console.WriteLine($"{player} wins!");
                        if (player == Dealer) {
                            Dealer.Balance += Bets.ToArray().Sum(pair => {
                                Console.WriteLine($"{pair.Value} lost {pair.Value} tokens");
                                Bets.Remove(pair.Key);
                                return pair.Value;
                            });
                        }
                        else if (Bets.TryGetValue(player, out int betAmmt)) {
                            Bets.Remove(player);
                            Console.WriteLine($"Blackjack! {player.Name} won {player.Balance += (int)(betAmmt * 2.5m)} tokens");
                        }
                        else Console.WriteLine($"Blackjack! {player.Name} won! (Error: Failed to get bet value)");
                    }
                }
            }

            foreach (BlackjackPlayer player in Players) {
                StringBuilder sb = new StringBuilder($"{player}'s hand is:\n");
                player.Hand.ForEach(card => sb.AppendLine($" - {card}"));
                Console.Write(sb);
                //bool autoWin = Players.All(plr => plr == player || plr.State == BlackjackState.Busted);
                //if (autoWin) goto finish;
                while (player.State == BlackjackState.None
                    && player.ShouldHitOrStay() == HitOrStay.Hit
                ) {
                    _ = Dealer.TryDeal(player, out Card drawnCard);
                    if (player != Dealer) ClearLastLine(); ClearLastLine();
                    Console.WriteLine($" - {drawnCard}");
                }
                if (player.State == BlackjackState.Busted) {
                    if (player == Dealer) {
                        Console.WriteLine($"{player} busted! Everyone gets their funds back");
                        Bets.ForEach(bet => _ = bet.Key.Balance += bet.Value);
                        Bets.Clear();
                        goto playAgain;
                    }
                    else if (Bets.TryGetValue(player, out int balance)) {
                        Bets.Remove(player);
                        Dealer.Balance += balance;
                        Console.WriteLine($"{player} busted! You lost {balance} tokens. Your balance is {player.Balance}");
                    }
                }
            
            }

            finish:
            foreach (var group in Players.Where(player => player.State != BlackjackState.Busted && player != Dealer).GroupBy(player => CompareHands(player, Dealer))) {
                switch (group.Key) {
                    case null:
                        group.ForEach(player => {
                            if (player == Dealer) return;
                            _ = Bets.TryGetValue(player, out int betAmmt);
                            Bets.Remove(player);
                            Console.WriteLine($"{player} got a push! Got their {player.Balance += betAmmt} token(s) back");
                        });
                        break;
                    case true:
                        group.ForEach(player => {
                            if (player == Dealer) return;
                            _ = Bets.TryGetValue(player, out int betAmmt);
                            Bets.Remove(player);
                            Console.WriteLine($"{player} won! Received {betAmmt *= 2} token(s). Balance is now {player.Balance += betAmmt}");
                        });
                        continue;
                    case false:
                        group.ForEach(player => {
                            if (player == Dealer) return;
                            _ = Bets.TryGetValue(player, out int betAmmt);
                            Bets.Remove(player);
                            Console.WriteLine($"{player} lost! Dealer received {betAmmt} token(s)");
                            Dealer.Balance += betAmmt;
                        });
                        continue;
                    default:
                        Console.WriteLine($"Invalid grouping {group.Key}");
                        group.ForEach(player => 
                            Console.WriteLine($"{player} is in an invalid group")
                        );
                        continue;
                }
            }
            playAgain:
            Players.ToArray().ForEach(player => {
                if (player == Dealer) return;
                if (player.Balance < 1) {
                    Console.WriteLine($"{player}, you do not have enough funds to play again");
                    return;
                }
                if (!(player.IsActivelyPlaying = ReadEnum<BoolResponse>($"{player}, want to play again").AsBool()))
                    Players.Remove(player);
            });
        }

        public override void ListPlayers() {
            Console.WriteLine("Welcome to Blackjack");
            base.ListPlayers();
        }

        public void WalkAway(Player player) {
            if (player is BlackjackPlayer blackjackPlayer 
                && Players.Contains(blackjackPlayer)
            ) Players.Remove(blackjackPlayer);
        }

        public static class Rules {
            static Dictionary<Face, byte> CardValues = new Dictionary<Face, byte>() {
                { Face.Two,   2 },
                { Face.Three, 3 },
                { Face.Four,  4 },
                { Face.Five,  5 },
                { Face.Six,   6 },
                { Face.Seven, 7 },
                { Face.Eight, 8 },
                { Face.Nine,  9 },
                { Face.Ten,   10 },
                { Face.Jack,  10 },
                { Face.Queen, 10 },
                { Face.King,  10 },
                { Face.Ace,   1 }
            };

            public static int[] GetAllPossibleValues(List<Card> hand) {
                int baseValue = hand.Where(card => card.Face != Face.Ace && CardValues.ContainsKey(card.Face))
                    .Sum(card => CardValues[card.Face]);
                IReadOnlyCollection<Card> aces = hand.Where(card => card.Face == Face.Ace).ToList().AsReadOnly();
                if (aces.Count == 0) return new int[] { baseValue };
                List<int> possibleValues = new List<int>() { baseValue };
                foreach (Card ace in aces) {
                    int interateCount = possibleValues.Count;
                    for (int i = 0; i < interateCount; i++)
                        possibleValues.Add((possibleValues[i] += 1) + 10);
                }
                return possibleValues.ToArray();
            }

            public static bool IsBlackjack(List<Card> hand)
                => GetAllPossibleValues(hand).Any(value => value == 21);
            public static bool IsBusted(List<Card> hand)
                => GetAllPossibleValues(hand).All(value => value > 21);
            public static bool ShouldDealerStay(List<Card> hand)
                => GetAllPossibleValues(hand).Any(value => 17 <= value);

            public static bool IsBlackjack(BlackjackPlayer player) => IsBlackjack(player.Hand);
            public static bool IsBusted(BlackjackPlayer player) => IsBusted(player.Hand);
            /// <summary>Compares the score against each hand</summary>
            /// <param name="a">First player</param>
            /// <param name="b">Second Player</param>
            /// <returns>True if player a has a higher hand, False if player b has a higher hand, and null if equal</returns>
            public static bool? CompareHands(BlackjackPlayer a, BlackjackPlayer b) => CompareHands(a.Hand, b.Hand);
            /// <summary>Compares the score against each hand</summary>
            /// <param name="a">First player hand</param>
            /// <param name="b">Second Player hand</param>
            /// <returns>True if first hand has a higher score, False if second hand has a higher score, and null if equal</returns>
            public static bool? CompareHands(List<Card> a, List<Card> b) {
                int aScore = GetAllPossibleValues(a).Max(value => 21 < value ? 0 : value),
                    bScore = GetAllPossibleValues(b).Max(value => 21 < value ? 0 : value);

                if (aScore == bScore) return null;
                return aScore > bScore;
            }
        }
    }
    public enum HitOrStay { Hit, Stay }
}
