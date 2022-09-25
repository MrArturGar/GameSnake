using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Models
{
    public class Food
    {
        public Coordinate Coordinate { get; set; }
        public Brush Color { get; set; }

        public Food(int x, int y)
        {
            Coordinate = new Coordinate(x, y);
            Color = Brushes.Red;
        }
    }
}
