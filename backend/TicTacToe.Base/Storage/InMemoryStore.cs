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
        public static Dictionary<Guid, GameState> Games
            = new();

        public static Scoreboard Scoreboard
            = new();
    }
}
