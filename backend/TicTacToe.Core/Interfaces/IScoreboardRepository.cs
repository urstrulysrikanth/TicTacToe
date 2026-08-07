using TicTacToe.Core.Models;

namespace TicTacToe.Core.Interfaces;

public interface IScoreboardRepository
{
    Scoreboard Get();

    void Save(Scoreboard scoreboard);
}
