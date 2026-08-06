using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TicTacToe.Core.Enums;

namespace TicTacToe.Core.Models;

public class GameState
{
    public Guid Id { get; set; }

    public string[][] Board { get; set; }
=
[
    ["","",""],
    ["","",""],
    ["","",""]
];

    public string CurrentPlayer { get; set; } = "X";

    public GameMode Mode { get; set; }

    public GameStatus Status { get; set; } = GameStatus.InProgress;

    public string? Winner { get; set; }

    public List<int> WinningCells { get; set; } = new();

    public List<Move> MoveHistory { get; set; } = new();
}
