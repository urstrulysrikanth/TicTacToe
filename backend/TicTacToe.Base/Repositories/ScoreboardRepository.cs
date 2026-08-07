using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using TicTacToe.Core.Models;
using TicTacToe.Repository.Storage;
using TicTacToe.Core.Interfaces;

namespace TicTacToe.Repository.Repositories
{
    public class ScoreboardRepository : IScoreboardRepository
    {
        public Scoreboard Get()
        {
            // Return the current scoreboard instance. Caller should avoid mutating it directly.
            return InMemoryStore.Scoreboard;
        }

        public void Save(Scoreboard scoreboard)
        {
            // Ensure updates to the scoreboard are performed atomically
            lock (InMemoryStore.ScoreboardLock)
            {
                InMemoryStore.Scoreboard = scoreboard;
            }
        }
    }
}
