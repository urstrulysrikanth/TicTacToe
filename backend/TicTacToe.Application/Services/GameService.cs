using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TicTacToe.Core.Enums;
using TicTacToe.Core.Helper;
using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Models;
using TicTacToe.Core.Requests;
using TicTacToe.Repository.Repositories;

namespace TicTacToe.Application.Services;

public class GameService : IGameService
{
    private readonly GameRepository _gameRepository;
    private readonly IComputerMoveService _computerMoveService;
    private readonly IScoreboardService _scoreboardService;

    public GameService(
        GameRepository gameRepository,
        IComputerMoveService computerMoveService,
        IScoreboardService scoreboardService)
    {
        _gameRepository = gameRepository;
        _computerMoveService = computerMoveService;
        _scoreboardService = scoreboardService;
    }

    public GameState CreateGame(GameMode mode)
    {
        var game = new GameState
        {
            Id = Guid.NewGuid(),
            Mode = mode,
            CurrentPlayer = "X",
            Status = GameStatus.InProgress
        };

        _gameRepository.Add(game);

        return game;
    }

    public GameState GetGame(Guid id)
    {
        return _gameRepository.Get(id);
    }

    public GameState MakeMove(Guid id, MoveRequest request)
    {
        try
        {
            var game = _gameRepository.Get(id);

            ValidateMove(game, request);

            ApplyMove(
                game,
                request.Player,
                request.Row,
                request.Column);

            EvaluateGame(game);

            // Computer turn
            if (game.Mode == GameMode.Computer &&
                game.Status == GameStatus.InProgress &&
                game.CurrentPlayer == "O")
            {
                ExecuteComputerMove(game);
            }

            _gameRepository.Update(game);

            return game;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public GameState Undo(Guid id)
    {
        var game = _gameRepository.Get(id);

        try
        {
            if (game.Status != GameStatus.InProgress)
                throw new InvalidOperationException(
                    "Undo is disabled after game completion.");

            if (!game.MoveHistory.Any())
                throw new InvalidOperationException(
                    "No moves available to undo.");

            if (game.Mode == GameMode.TwoPlayer)
            {
                RemoveLastMove(game);
            }
            else
            {
                // Computer mode
                if (game.MoveHistory.Count >= 2)
                {
                    RemoveLastMove(game);
                    RemoveLastMove(game);
                }
                else
                {
                    RemoveLastMove(game);
                }
            }

            game.Status = GameStatus.InProgress;
            game.Winner = null;
            game.WinningCells.Clear();

            _gameRepository.Update(game);

            return game;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public GameState Reset(Guid id)
    {
        var game = _gameRepository.Get(id);

        game.Board = new string[][]
        {
            new string[3],
            new string[3],
            new string[3]
        };
        game.MoveHistory.Clear();

        game.CurrentPlayer = "X";
        game.Status = GameStatus.InProgress;
        game.Winner = null;
        game.WinningCells.Clear();

        _gameRepository.Update(game);

        return game;
    }

    private void ValidateMove(
        GameState game,
        MoveRequest request)
    {
        try
        {
            if (game.Status != GameStatus.InProgress)
                throw new InvalidOperationException(
                    "Game already completed.");

            if (request.Row < 0 || request.Row > 2)
                throw new InvalidOperationException(
                    "Invalid row.");

            if (request.Column < 0 || request.Column > 2)
                throw new InvalidOperationException(
                    "Invalid column.");

            if (request.Player != game.CurrentPlayer)
                throw new InvalidOperationException(
                    "Not player's turn.");

            if (!string.IsNullOrWhiteSpace(
                    game.Board[request.Row][request.Column]))
            {
                throw new InvalidOperationException(
                    "Cell already occupied.");
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ApplyMove(
        GameState game,
        string player,
        int row,
        int column)
    {
        game.Board[row][column] = player;

        game.MoveHistory.Add(new Move
        {
            MoveNumber = game.MoveHistory.Count + 1,
            Player = player,
            Row = row,
            Column = column
        });

        game.CurrentPlayer =
            player == "X" ? "O" : "X";
    }

    private void ExecuteComputerMove(GameState game)
    {
        var move = _computerMoveService
            .GetBestMove(game);

        ApplyMove(
            game,
            "O",
            move.row,
            move.column);

        EvaluateGame(game);
    }

    private void EvaluateGame(GameState game)
    {
        var winner = CheckWinner(game);

        if (winner != null)
        {
            game.Status = GameStatus.Won;
            game.Winner = winner.Player;
            game.WinningCells = winner.Cells;

            _scoreboardService.AddWinner(
                winner.Player);

            return;
        }

        if (IsDraw(game))
        {
            game.Status = GameStatus.Draw;

            _scoreboardService.AddDraw();
        }
    }

    private WinnerResult? CheckWinner(
        GameState game)
    {
        string[] board =
        {
            game.Board[0][0],
            game.Board[0][1],
            game.Board[0][2],

            game.Board[1][0],
            game.Board[1][1],
            game.Board[1][2],

            game.Board[2][0],
            game.Board[2][1],
            game.Board[2][2]
        };

        foreach (var pattern in WinningPatterns.Patterns)
        {
            var first = board[pattern[0]];

            if (string.IsNullOrWhiteSpace(first))
                continue;

            if (first == board[pattern[1]] &&
                first == board[pattern[2]])
            {
                return new WinnerResult
                {
                    Player = first,
                    Cells = pattern.ToList()
                };
            }
        }

        return null;
    }

    private bool IsDraw(GameState game)
    {
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                if (string.IsNullOrWhiteSpace(
                        game.Board[row][col]))
                {
                    return false;
                }
            }
        }

        return true;
    }

    private void RemoveLastMove(GameState game)
    {
        if (!game.MoveHistory.Any())
            return;

        var lastMove = game.MoveHistory.Last();

        game.Board[lastMove.Row][
                   lastMove.Column] = string.Empty;

        game.MoveHistory.Remove(lastMove);

        game.CurrentPlayer = lastMove.Player;
    }

    private sealed class WinnerResult
    {
        public string Player { get; set; } = "";

        public List<int> Cells { get; set; } = new();
    }
}
