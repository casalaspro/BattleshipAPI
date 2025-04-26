namespace Battleship.Models
{
    public class Board
    {
        public Cell[,] Grid { get; }
        public List<Ship> Ships { get; set; }

        public Board()
        {
            Grid = new Cell[10, 10];
            for(int x=0; x<10; x++)
            {
                for(int y=0; y<10; y++)
                {
                    Grid[x, y] = new Cell { X = x, Y = y, IsHit = false };
                }
            }
        }
    }
}
