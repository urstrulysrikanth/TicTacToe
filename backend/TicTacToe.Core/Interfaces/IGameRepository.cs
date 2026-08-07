using System;
using TicTacToe.Core.Models;

namespace TicTacToe.Core.Interfaces;

public interface IGameRepository
{
    void Add(GameState game);

    GameState Get(Guid id);

    void Update(GameState game);
}
