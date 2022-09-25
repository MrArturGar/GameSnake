using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Models
{
    public class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Coordinate Clone()
        {
            return new Coordinate(X, Y);
        }

        public static bool operator ==(Coordinate left, Coordinate right)
        {
            if (left.X == right.X && left.Y == right.Y)
                return true;
            else
                return false;
        }
        public static bool operator !=(Coordinate left, Coordinate right)
        {
            if (left.X == right.X && left.Y == right.Y)
                return false;
            else
                return true;
        }
    }
}
