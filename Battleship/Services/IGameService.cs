using Battleship.DTOs;

namespace Battleship.Services
{
    public interface IGameService
    {
        Guid InitGame(IPlacementStrategy placementStrategy);
        FireResponseDTO Fire(Guid gameId, int x, int y);
    }
}
