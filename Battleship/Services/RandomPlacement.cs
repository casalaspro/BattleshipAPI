using Battleship.Constants;
using Battleship.Models;

namespace Battleship.Services
{
    public class RandomPlacement : IPlacementStrategy
    {
        private readonly Random _random = new();
        private readonly List<(string Name, int Size)> _shipDefinitions = new()
        {
            (ShipNames.AircraftCarrier, 5),
            (ShipNames.Battleship, 4),
            (ShipNames.Submarine, 3),
            (ShipNames.Destroyer, 3),
            (ShipNames.PatrolBoat, 2)
        };

        public List<Ship> PlaceShips()
        {
            List<Ship> ships = new();

            // matrix to track occupied cells
            bool[,] occupied = new bool[10, 10];

            // cicle for ship definition
            foreach (var shipDef in _shipDefinitions)
            {
                bool placed = false;
                while (!placed)
                {
                    // define if the placement will be horizontal
                    bool horizontal = _random.Next(2) == 0;

                    // define start coords being sure that the ships will not be outside the grid
                    int x = _random.Next(horizontal ? (10 - shipDef.Size + 1) : 10);
                    int y = _random.Next(horizontal ? 10 : (10 - shipDef.Size + 1));

                    // generate a list of coords for each segment of the ship
                    List<(int, int)> coords = Enumerable.Range(0, shipDef.Size)
                        .Select(i => horizontal ? (x + i, y) : (x, y + i)).ToList();

                    // set occupied the cells 
                    if (coords.Any(c => occupied[c.Item1, c.Item2])) continue;
                    coords.ForEach(c => occupied[c.Item1, c.Item2] = true);

                    // create the ship object
                    ships.Add(new Ship { Name = shipDef.Name, Size = shipDef.Size, Coordinates = coords });

                    // stop while cicle
                    placed = true;
                }
                
            }

            return ships;
        }
        
    }
}
