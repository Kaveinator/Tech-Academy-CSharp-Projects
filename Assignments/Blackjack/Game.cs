using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack {
    public abstract class Game<TGame, TPlayer> where TGame : Game<TGame, TPlayer> where TPlayer : Player {
        public readonly List<TPlayer> Players = new List<TPlayer>();
        public readonly Dictionary<TPlayer, int> Bets = new Dictionary<TPlayer, int>();
        public readonly string Name;
        protected Game(string name) {
            Name = name;
        }

        public virtual void ListPlayers() {
            foreach (var player in Players)
                Console.WriteLine(player.Name);
        }

        public virtual void Play() => throw new NotImplementedException();

        public static TGame operator +(Game<TGame, TPlayer> game, TPlayer player) {
            if (!game.Players.Contains(player)) {
                game.Players.Add(player);
                player.IsActivelyPlaying = true;
            }
            return game as TGame;
        }

        public static TGame operator -(Game<TGame, TPlayer> game, TPlayer player) {
            if (game.Players.Contains(player)) {
                game.Players.Remove(player);
                player.IsActivelyPlaying = false;
            }
            return game as TGame;
        }
    }
}
