using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Models;

namespace TicTacToe.Services.Services;

public class ScoreboardService : IScoreboardService
{
    private readonly IScoreboardRepository _repository;

    public ScoreboardService(IScoreboardRepository repository)
    {
        _repository = repository;
    }

    public Scoreboard Get()
    {
        return _repository.Get();
    }

    public void Reset()
    {
        _repository.Save(new Scoreboard());
    }

    public void AddWinner(string player)
    {
        var board = _repository.Get();

        if (player == "X")
            board.XWins++;
        else
            board.OWins++;

        _repository.Save(board);
    }

    public void AddDraw()
    {
        var board = _repository.Get();

        board.Draws++;

        _repository.Save(board);
    }
}
