using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Core.Models;

public class Move
{
    public int MoveNumber { get; set; }

    public string Player { get; set; } = string.Empty;

    public int Row { get; set; }

    public int Column { get; set; }
}