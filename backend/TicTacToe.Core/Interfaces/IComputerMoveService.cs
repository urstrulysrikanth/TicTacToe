using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TicTacToe.Core.Models;

namespace TicTacToe.Core.Interfaces;

public interface IComputerMoveService
{
    (int row, int column) GetBestMove(GameState game);
}