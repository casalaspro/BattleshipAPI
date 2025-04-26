namespace Battleship.Models
{
    public class Ship
    {
        public string Name { get; set; }
        public int Size { get; set; }
        public List<(int x, int y)> Coordinates { get; set; }
        public HashSet<(int x, int y)> Hits {get; set;} = new();

        public bool IsSunk => Hits.Count >= Size;
    }
}
