using Microsoft.AspNetCore.Mvc;
using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Requests;

namespace TicTacToe.Api.Controllers;

[ApiController]
[Route("api/games")]
public class GamesController : ControllerBase
{
    private readonly IGameService _gameService;

    public GamesController(IGameService gameService)
    {
        _gameService = gameService;
    }

    [HttpPost]
    public IActionResult CreateGame(
        [FromBody] CreateGameRequest request)
    {
        var game = _gameService.CreateGame(request.Mode);

        return Ok(game);
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetGame(Guid id)
    {
        var game = _gameService.GetGame(id);

        return Ok(game);
    }

    [HttpPost("{id:guid}/moves")]
    public IActionResult MakeMove(
        Guid id,
        [FromBody] MoveRequest request)
    {
        try
        {
            var game = _gameService.MakeMove(id, request);

            return Ok(game);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status412PreconditionFailed,
        new { Message = ex.Message });
        }
    }

    [HttpPost("{id:guid}/undo")]
    public IActionResult Undo(Guid id)
    {
        try
        {
            var game = _gameService.Undo(id);
            return Ok(game);
        }
        catch (Exception ex)
        {

            return StatusCode(StatusCodes.Status412PreconditionFailed,
        new { Message = ex.Message });
        }

    }

    [HttpPost("{id:guid}/reset")]
    public IActionResult Reset(Guid id)
    {
        var game = _gameService.Reset(id);

        return Ok(game);
    }
}
