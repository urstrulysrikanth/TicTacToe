using Microsoft.AspNetCore.Mvc;
using TicTacToe.Core.Interfaces;

namespace TicTacToe.Api.Controllers;

[ApiController]
[Route("api/scoreboard")]
public class ScoreboardController : ControllerBase
{
    private readonly IScoreboardService _scoreboardService;

    public ScoreboardController(
        IScoreboardService scoreboardService)
    {
        _scoreboardService = scoreboardService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_scoreboardService.Get());
    }

    [HttpPost("reset")]
    public IActionResult Reset()
    {
        _scoreboardService.Reset();

        return Ok(new
        {
            Message = "Scoreboard reset successfully"
        });
    }
}