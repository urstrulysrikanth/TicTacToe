using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TicTacToe.Core.Enums;

namespace TicTacToe.Core.Requests;

public class CreateGameRequest
{
    public GameMode Mode { get; set; }
}
