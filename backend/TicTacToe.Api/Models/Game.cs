using System;
using System.Collections.Generic;
using TicTacToe.Api.Enums;

namespace TicTacToe.Api.Models
{
    public class Game
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public GameMode GameMode { get; set; }

        public GameStatus Status { get; set; } = GameStatus.InProgress;

        public Player CurrentPlayer { get; set; } = Player.X;

        public Player? Winner { get; set; }

        public Player[,] Board { get; set; } = new Player[3, 3];

        public List<Move> MoveHistory { get; set; } = [];

        public List<Cell> WinningCells { get; set; } = [];
    }
}
