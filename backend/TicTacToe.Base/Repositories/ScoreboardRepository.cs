using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Core.Models;
using TicTacToe.Repository.Storage;

namespace TicTacToe.Repository.Repositories
{
    public class ScoreboardRepository
    {
        public Scoreboard Get()
        {
            return InMemoryStore.Scoreboard;
        }

        public void Save(Scoreboard scoreboard)
        {
            InMemoryStore.Scoreboard = scoreboard;
        }
    }
}