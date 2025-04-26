using Battleship.Models;
using Battleship.DTOs;

using Newtonsoft.Json;

namespace Battleship.Services
{
    public class JsonPlacement : IPlacementStrategy
    {
        private readonly IConfiguration _configuration;
        public JsonPlacement(IConfiguration config) { _configuration = config; }
        public List<Ship> PlaceShips()
        {
            string path = _configuration["ShipsPlacementsPath"];
            if (path == null)
            {
                throw new Exception("Json file path missing.");
            }
            string json = File.ReadAllText(path);

            List<ShipDTO> shipDTOs = JsonConvert.DeserializeObject<List<ShipDTO>>(json);

            if(shipDTOs == null)
            {
                throw new Exception("Something went wrong with deserialization.");
            }

            List<Ship> result = new();

            foreach(ShipDTO shipDTO in shipDTOs)
            {
                Ship ship = new Ship()
                {
                    Name = shipDTO.Name,
                    Size = shipDTO.Size,
                    Coordinates = shipDTO.Coordinates
                             .Select(c => (c.X, c.Y))  // o crea nuovi oggetti Position
                             .ToList()
                };

                result.Add(ship);
            }

            if (result == null)
            {
                throw new Exception("Something went wrong with deserialization.");
            }

            return result;
        }
    }
}
