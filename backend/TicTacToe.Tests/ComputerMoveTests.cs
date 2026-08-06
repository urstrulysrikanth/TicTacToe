using TicTacToe.Application.Services;
using TicTacToe.Core.Models;
using Xunit;

namespace TicTacToe.Tests;

public class ComputerMoveTests
{
    private readonly ComputerMoveService _service;

    public ComputerMoveTests()
    {
        _service = new ComputerMoveService();
    }

    [Fact]
    public void Computer_ShouldPlayWinningMove()
    {
        var game = new GameState();

        game.Board[0][0] = "O";
        game.Board[0][1] = "O";

        var move = _service.GetBestMove(game);

        Assert.Equal(0, move.row);
        Assert.Equal(2, move.column);
    }

    [Fact]
    public void Computer_ShouldBlockOpponentWinningMove()
    {
        var game = new GameState();

        game.Board[0][0] = "X";
        game.Board[0][1] = "X";

        var move = _service.GetBestMove(game);

        Assert.Equal(0, move.row);
        Assert.Equal(2, move.column);
    }

    [Fact]
    public void Computer_ShouldTakeCenter_WhenAvailable()
    {
        var game = new GameState();

        var move = _service.GetBestMove(game);

        Assert.Equal(1, move.row);
        Assert.Equal(1, move.column);
    }

    [Fact]
    public void Computer_ShouldTakeCorner_WhenCenterUnavailable()
    {
        var game = new GameState();

        game.Board[1][1] = "X";

        var move = _service.GetBestMove(game);

        Assert.True(
            (move.row == 0 && move.column == 0) ||
            (move.row == 0 && move.column == 2) ||
            (move.row == 2 && move.column == 0) ||
            (move.row == 2 && move.column == 2));
    }

    [Fact]
    public void Computer_ShouldTakeFirstAvailableCell()
    {
        var game = new GameState();

        game.Board =
        [
            ["X","O","X"],
            ["O","X","O"],
            ["","X","O"]
        ];

        var move = _service.GetBestMove(game);

        Assert.Equal(2, move.row);
        Assert.Equal(0, move.column);
    }
}