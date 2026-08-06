using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Core.Models;
using TicTacToe.Repository.Storage;

namespace TicTacToe.Repository.Repositories
{
    public class GameRepository
    {
        public void Add(GameState game)
        {
            InMemoryStore.Games[game.Id] = game;
        }

        public GameState Get(Guid id)
        {
            if (!InMemoryStore.Games.ContainsKey(id))
                throw new Exception("Game not found");

            return InMemoryStore.Games[id];
        }

        public void Update(GameState game)
        {
            InMemoryStore.Games[game.Id] = game;
        }
    }
}
