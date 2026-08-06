using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TicTacToe.Core.Enums;
using TicTacToe.Core.Models;
using TicTacToe.Core.Requests;

namespace TicTacToe.Core.Interfaces;

public interface IGameService
{
    GameState CreateGame(GameMode mode);

    GameState GetGame(Guid id);

    GameState MakeMove(Guid id, MoveRequest request);

    GameState Undo(Guid id);

    GameState Reset(Guid id);
}
