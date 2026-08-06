using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Core.Requests;

public class MoveRequest
{
    public string Player { get; set; } = string.Empty;

    public int Row { get; set; }

    public int Column { get; set; }
}
