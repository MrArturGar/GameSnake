using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Models
{
    public class Snake
    {
        public List<Coordinate> CoordinateList { get; set; }
        public Direction Direction { get; set; }

        public Snake(Direction direction)
        {
            Direction = direction;
            CoordinateList = new List<Coordinate>();
        }

        public int Find(Coordinate coordinate)
        {
            return CoordinateList.Where(c => c.X == coordinate.X && c.Y == coordinate.Y).Count();
        }
    }
}
