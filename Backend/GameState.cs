using System;
using System.Collections.Generic;
using System.Linq;

namespace Edison
{
    public class GameState
    {
        public DateTime LastTick { get; set; }
        public double LastDiff { get; set; }
        public CashValue Cash { get; set; }
        public ResearchPointsValue Research { get; set; }
        public int GridSize { get; set; }
        public double TotalPowerProduction { get; set; }
        public List<PowerGenerator> Generators { get; set; }
        public List<GridExtender> Extenders { get; set; }
        public List<Appliance> Appliances { get; set; }
        public List<Researcher> Researchers { get; set; }
        public IEnumerable<IHideable> Hideables =>
            Generators
                .Concat<IHideable>(Extenders)
                .Concat(Researchers);
    }
}
