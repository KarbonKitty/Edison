using System.Collections.Generic;

namespace Edison
{
    public static class PowerGeneratorsData
    {
        public static List<(Generators id, string name, double startingPrice, double startingProduction, bool isHiddenOnStart)> Data
            => new()
            {
                (Generators.Solar, "Solar", 50, 1, false),
                (Generators.Hydro, "Hydro", 250, 2, true),
                (Generators.Coal, "Coal", 1000, 2.5, true),
                (Generators.Gas, "Gas", 10_000, 3, true),
                (Generators.Fission, "Fission", 65_000, 4, true),
                (Generators.Fusion, "Fusion", 150_000, 5, true),
                (Generators.Orbital, "Orbital solar", 475_000, 5.5, true),
                (Generators.ZeroPoint, "Zero-point", 1_000_000, 7, true)
            };
    }
}
