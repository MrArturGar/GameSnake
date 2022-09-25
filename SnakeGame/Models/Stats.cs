using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Models
{
    public class Stats
    {
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public int startSankeSize { get; set; }
        public int endSankeSize { get; set; }
        public Coordinate GameSize { get; set; }
    }
}
