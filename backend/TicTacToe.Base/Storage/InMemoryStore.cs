using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Core.Models;

namespace TicTacToe.Repository.Storage
{ 

    public static class InMemoryStore
    {
        // Use ConcurrentDictionary for thread-safe access across requests
        public static System.Collections.Concurrent.ConcurrentDictionary<Guid, GameState> Games
            = new();

        // Scoreboard instance and lock for thread-safe updates
        public static Scoreboard Scoreboard
            = new();

        public static readonly object ScoreboardLock = new();
    }
}
