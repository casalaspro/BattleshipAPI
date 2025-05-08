using Battleship.DTOs;
using Battleship.Enums;
using Battleship.Services;
using Microsoft.AspNetCore.Mvc;

namespace Battleship.Controllers
{
    [ApiController]
    [Route("api/game")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly IServiceProvider _serviceProvider;
        public GameController(IGameService gameService, IServiceProvider serviceProvider)
        {
            _gameService = gameService;
            _serviceProvider = serviceProvider;
        }

        [HttpPost]
        public ActionResult<Guid> CreateGame([FromQuery] string placement = "Json")
        {
            var strategy = placement.Equals(PlacementStrategyEnum.Json.ToString(), StringComparison.OrdinalIgnoreCase)
                ? _serviceProvider.GetRequiredService<JsonPlacement>() as IPlacementStrategy // casting because the compiler not recognized the two services as implementation of the same interface
                : _serviceProvider.GetRequiredService<RandomPlacement>();

            return Ok(_gameService.InitGame(strategy));
        }
           

        [HttpPost("{gameId}/fire")]
        public ActionResult<FireResponseDTO> Fire(Guid gameId, [FromBody] FireRequestDTO request)
        {
            var result = _gameService.Fire(gameId, request.X, request.Y);
            return Ok(result);

        }
    }
}
