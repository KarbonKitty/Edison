using System.Collections.Generic;

namespace Edison
{
    public class GameStateDto
    {
        public long LastTick { get; set; }
        public double LastDiff { get; set; }
        public double Cash { get; set; }
        public double Research { get; set; }
        public int GridSize { get; set; }
        public double TotalPowerProduction { get; set; }
        public List<GeneratorDto> Generators { get; set; }
        public List<ExtenderDto> Extenders { get; set; }
        public List<ApplianceDto> Appliances { get; set; }
    }
}
