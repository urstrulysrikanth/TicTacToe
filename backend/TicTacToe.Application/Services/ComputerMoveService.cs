using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TicTacToe.Core.Helper;
using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Models;

namespace TicTacToe.Application.Services;

public class ComputerMoveService : IComputerMoveService
{
    public (int row, int column) GetBestMove(GameState game)
    {
        //Try to win
        var winningMove = FindWinningMove(game, "O");
        if (winningMove.HasValue)
            return winningMove.Value;

        // Block player X
        var blockingMove = FindWinningMove(game, "X");
        if (blockingMove.HasValue)
            return blockingMove.Value;

        // Take center
        if (string.IsNullOrWhiteSpace(game.Board[1][1]))
            return (1, 1);

        // Take corner
        var corners = new List<(int row, int col)>
        {
            (0,0),
            (0,2),
            (2,0),
            (2,2)
        };

        foreach (var corner in corners)
        {
            if (string.IsNullOrWhiteSpace(
                    game.Board[corner.row][corner.col]))
            {
                return corner;
            }
        }

        //  Take first available cell
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                if (string.IsNullOrWhiteSpace(game.Board[row][col]))
                {
                    return (row, col);
                }
            }
        }

        throw new InvalidOperationException(
            "No valid moves available.");
    }

    private (int row, int column)? FindWinningMove(
        GameState game,
        string player)
    {
        foreach (var pattern in WinningPatterns.Patterns)
        {
            var cells = pattern
                .Select(index => GetCellValue(game, index))
                .ToArray();

            int playerCount =
                cells.Count(x => x == player);

            int emptyCount =
                cells.Count(string.IsNullOrWhiteSpace);

            if (playerCount == 2 && emptyCount == 1)
            {
                int emptyIndex = Array.FindIndex(
                    cells,
                    string.IsNullOrWhiteSpace);

                int boardIndex = pattern[emptyIndex];

                return ConvertToRowColumn(boardIndex);
            }
        }

        return null;
    }

    private static string GetCellValue(
        GameState game,
        int index)
    {
        int row = index / 3;
        int col = index % 3;

        return game.Board[row][col];
    }

    private static (int row, int column)
        ConvertToRowColumn(int index)
    {
        return (index / 3, index % 3);
    }
}
