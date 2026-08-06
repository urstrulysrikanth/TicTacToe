using Moq;
using TicTacToe.Application.Services;
using TicTacToe.Core.Enums;
using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Requests;
using TicTacToe.Repository.Repositories;
using Xunit;

namespace TicTacToe.Tests;

public class ScoreboardTests
{
    private readonly Mock<IScoreboardService> _scoreboardMock;
    private readonly GameService _service;

    public ScoreboardTests()
    {
        _scoreboardMock = new Mock<IScoreboardService>();

        _service = new GameService(
            new GameRepository(),
            new Mock<IComputerMoveService>().Object,
            _scoreboardMock.Object);
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
    public void Winner_ShouldUpdateScoreboard()
    {
        var game = _service.CreateGame(GameMode.TwoPlayer);

        _service.MakeMove(game.Id, Move("X", 0, 0));
        _service.MakeMove(game.Id, Move("O", 1, 0));

        _service.MakeMove(game.Id, Move("X", 0, 1));
        _service.MakeMove(game.Id, Move("O", 1, 1));

        _service.MakeMove(game.Id, Move("X", 0, 2));

        _scoreboardMock.Verify(
            x => x.AddWinner("X"),
            Times.Once);
    }
}