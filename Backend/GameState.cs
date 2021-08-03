using System;
using System.Collections.Generic;

namespace Edison
{
    public class GameState
    {
        public DateTime LastTick { get; set; }
        public double LastDiff { get; set; }
        public double Cash { get; set; }
        public List<PowerGenerator> Generators { get; set; }
    }
}
