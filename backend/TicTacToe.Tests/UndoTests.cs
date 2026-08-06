using Moq;
using TicTacToe.Application.Services;
using TicTacToe.Core.Enums;
using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Models;
using TicTacToe.Core.Requests;
using TicTacToe.Repository.Repositories;
using Xunit;

namespace TicTacToe.Tests;

public class UndoTests
{
    private readonly GameService _service;

    public UndoTests()
    {
        _service = new GameService(
            new GameRepository(),
            new Mock<IComputerMoveService>().Object,
            new Mock<IScoreboardService>().Object);
    }

    private MoveRequest Move(string player, int row, int col)
    {
        return new MoveRequest
        {
            Player = player,
            Row = row,
            Column = col
        };
    }

    [Fact]
    public void Undo_TwoPlayer_ShouldRemoveLastMove()
    {
        var game = _service.CreateGame(GameMode.TwoPlayer);

        _service.MakeMove(game.Id, Move("X", 0, 0));
        _service.MakeMove(game.Id, Move("O", 1, 1));

        _service.Undo(game.Id);

        Assert.Equal("", game.Board[1][1]);
        Assert.Equal("O", game.CurrentPlayer);
        Assert.Single(game.MoveHistory);
    }
    [Fact]
    public void Undo_ComputerMode_ShouldRemoveMovePair()
    {
        var computerMock = new Mock<IComputerMoveService>();

        computerMock
            .Setup(x => x.GetBestMove(It.IsAny<GameState>()))
            .Returns((1, 1));

        var service = new GameService(
            new GameRepository(),
            computerMock.Object,
            new Mock<IScoreboardService>().Object);

        var game = service.CreateGame(GameMode.Computer);

        service.MakeMove(game.Id,
            new MoveRequest
            {
                Player = "X",
                Row = 0,
                Column = 0
            });

        Assert.Equal(2, game.MoveHistory.Count);

        service.Undo(game.Id);

        Assert.Empty(game.MoveHistory);
        Assert.Equal("X", game.CurrentPlayer);
    }
    [Fact]
    public void Undo_WithNoMoves_ShouldThrow()
    {
        var game = _service.CreateGame(GameMode.TwoPlayer);

        Assert.Throws<InvalidOperationException>(() =>
            _service.Undo(game.Id));
    }
}