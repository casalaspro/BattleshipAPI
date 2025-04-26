using Battleship.DTOs;
using Battleship.Services;
using Microsoft.AspNetCore.Mvc;

namespace Battleship.Controllers
{
    [ApiController]
    [Route("api/game")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpPost]
        public ActionResult<Guid> CreateGame()
            => Ok(_gameService.InitGame());

        [HttpPost("{gameId}/fire")]
        public ActionResult<FireResponseDTO> Fire(Guid gameId, [FromBody] FireRequestDTO request)
        {
            var result = _gameService.Fire(gameId, request.X, request.Y);
            return Ok(result);

        }
    }
}
