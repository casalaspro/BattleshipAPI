using Battleship.Models;

namespace Battleship.Services
{
    public interface IPlacementStrategy
    {
        List<Ship> PlaceShips();
    }
}
