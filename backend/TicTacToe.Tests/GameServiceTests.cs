using Moq;
using TicTacToe.Application.Services;
using TicTacToe.Core.Enums;
using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Requests;
using TicTacToe.Repository.Repositories;
using Xunit;

namespace TicTacToe.Tests;

public class GameServiceTests
{
    private readonly GameService _service;
    private readonly Mock<IComputerMoveService> _computerMock;
    private readonly Mock<IScoreboardService> _scoreboardMock;

    public GameServiceTests()
    {
        _computerMock = new Mock<IComputerMoveService>();
        _scoreboardMock = new Mock<IScoreboardService>();

        _service = new GameService(
            new GameRepository(),
            _computerMock.Object,
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
    public void ValidMove_ShouldUpdateBoard()
    {
        var game = _service.CreateGame(GameMode.TwoPlayer);

        _service.MakeMove(game.Id, Move("X", 0, 0));

        Assert.Equal("X", game.Board[0][0]);
    }

    [Fact]
    public void InvalidMove_ShouldThrow()
    {
        var game = _service.CreateGame(GameMode.TwoPlayer);

        _service.MakeMove(game.Id, Move("X", 0, 0));

        Assert.Throws<InvalidOperationException>(() =>
            _service.MakeMove(game.Id, Move("O", 0, 0)));
    }

    [Fact]
    public void TurnShouldSwitch()
    {
        var game = _service.CreateGame(GameMode.TwoPlayer);

        _service.MakeMove(game.Id, Move("X", 0, 0));

        Assert.Equal("O", game.CurrentPlayer);
    }

    [Fact]
    public void RowWin_ShouldDetectWinner()
    {
        var game = _service.CreateGame(GameMode.TwoPlayer);

        _service.MakeMove(game.Id, Move("X", 0, 0));
        _service.MakeMove(game.Id, Move("O", 1, 0));

        _service.MakeMove(game.Id, Move("X", 0, 1));
        _service.MakeMove(game.Id, Move("O", 1, 1));

        _service.MakeMove(game.Id, Move("X", 0, 2));

        Assert.Equal(GameStatus.Won, game.Status);
        Assert.Equal("X", game.Winner);
    }

    [Fact]
    public void ColumnWin_ShouldDetectWinner()
    {
        var game = _service.CreateGame(GameMode.TwoPlayer);

        _service.MakeMove(game.Id, Move("X", 0, 0));
        _service.MakeMove(game.Id, Move("O", 0, 1));

        _service.MakeMove(game.Id, Move("X", 1, 0));
        _service.MakeMove(game.Id, Move("O", 1, 1));

        _service.MakeMove(game.Id, Move("X", 2, 0));

        Assert.Equal(GameStatus.Won, game.Status);
    }
    [Fact]
    public void Draw_ShouldDetect()
    {
        var game = _service.CreateGame(GameMode.TwoPlayer);

        _service.MakeMove(game.Id, Move("X", 0, 0));
        _service.MakeMove(game.Id, Move("O", 0, 1));
        _service.MakeMove(game.Id, Move("X", 0, 2));

        _service.MakeMove(game.Id, Move("O", 1, 1));
        _service.MakeMove(game.Id, Move("X", 1, 0));
        _service.MakeMove(game.Id, Move("O", 1, 2));

        _service.MakeMove(game.Id, Move("X", 2, 1));
        _service.MakeMove(game.Id, Move("O", 2, 0));
        _service.MakeMove(game.Id, Move("X", 2, 2));

        Assert.Equal(GameStatus.Draw, game.Status);
    }
    [Fact]
    public void DiagonalWin_ShouldDetectWinner()
    {
        var game = _service.CreateGame(GameMode.TwoPlayer);

        _service.MakeMove(game.Id, Move("X", 0, 0));
        _service.MakeMove(game.Id, Move("O", 0, 1));

        _service.MakeMove(game.Id, Move("X", 1, 1));
        _service.MakeMove(game.Id, Move("O", 0, 2));

        _service.MakeMove(game.Id, Move("X", 2, 2));

        Assert.Equal(GameStatus.Won, game.Status);
    }

    [Fact]
    public void MoveAfterCompletion_ShouldThrow()
    {
        var game = _service.CreateGame(GameMode.TwoPlayer);

        _service.MakeMove(game.Id, Move("X", 0, 0));
        _service.MakeMove(game.Id, Move("O", 1, 0));

        _service.MakeMove(game.Id, Move("X", 0, 1));
        _service.MakeMove(game.Id, Move("O", 1, 1));

        _service.MakeMove(game.Id, Move("X", 0, 2));

        Assert.Throws<InvalidOperationException>(() =>
            _service.MakeMove(game.Id, Move("O", 2, 2)));
    }

    [Fact]
    public void Reset_ShouldClearBoardAndHistory()
    {
        var game = _service.CreateGame(GameMode.TwoPlayer);

        _service.MakeMove(game.Id, Move("X", 0, 0));

        _service.Reset(game.Id);

        Assert.Empty(game.MoveHistory);
        Assert.Equal("X", game.CurrentPlayer);
        Assert.Equal(GameStatus.InProgress, game.Status);
    }
}