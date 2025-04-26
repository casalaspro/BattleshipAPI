using System.Text.Json.Serialization;
using Battleship.Models;
using Newtonsoft.Json;

namespace Battleship.Services
{
    public class PlacementStrategy : IPlacementStrategy
    {
        private readonly IConfiguration _configuration;

        public List<Ship> PlaceShips()
        {
            string path = _configuration["ShipsPlacementsPath"];
            if(path == null)
            {
                throw new Exception("Json file path missing.");
            }
            string json = File.ReadAllText(path);

            List<Ship>? result = JsonConvert.DeserializeObject<List<Ship>>(json);
            
            if(result == null)
            {
                throw new Exception("Something went wrong with deserialization.");
            }

            return result;
        }
    }
}
