using System.Collections.Concurrent;
using Battleship.DTOs;
using Battleship.Enums;
using Battleship.Models;

namespace Battleship.Services
{
    public class GameService : IGameService
    {
        private readonly IPlacementStrategy _placement;
        private readonly ConcurrentDictionary<Guid, Board> _games = new();

        public GameService(IPlacementStrategy placement)
        {
            _placement = placement;
        }

        public Guid InitGame()
        {
            Board board = new Board();
            board.Ships = _placement.PlaceShips();

            Guid id = Guid.NewGuid();
            _games[id] = board;
            return id;
        }

        public FireResponseDTO Fire(Guid gameId, int x, int y)
        {
            if(!_games.TryGetValue(gameId, out Board board))
            {
                throw new Exception("Game not found");
            }

            Cell cell = board.Grid[x, y];

            if (cell.IsHit)
            {
                return new FireResponseDTO() { Response = OutcomeEnum.Miss.ToString() };
            }

            cell.IsHit = true;

            Ship hitShip = board.Ships.FirstOrDefault(s => s.Coordinates.Contains((x, y)));

            if (hitShip == null)
            {
                return new FireResponseDTO() { Response = OutcomeEnum.Miss.ToString() };
            }

            hitShip.Hits.Add((x, y));

            bool allSunk = board.Ships.All(s => s.IsSunk);

            if (allSunk)
            {
                return new FireResponseDTO() { Response = "Each ship sunk!" };
            }

            if (hitShip.IsSunk)
            {
                return new FireResponseDTO() { Response = $"{OutcomeEnum.Sunk.ToString()} {hitShip.Name}" };
            }

            return new FireResponseDTO() { Response = OutcomeEnum.Hit.ToString() };
        }
    }
}
