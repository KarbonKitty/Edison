using System.Collections.Generic;

public static class PowerGeneratorsData
{
    public static List<(Generators id, string name, double startingPrice, double startingProduction)> Data
        => new() {
            (Generators.Solar, "Solar", 50, 1),
            (Generators.Hydro, "Hydro", 250, 2),
            (Generators.Coal, "Coal", 1000, 2.5),
            (Generators.Gas, "Gas", 10_000, 3),
            (Generators.Fission, "Fission", 65_000, 4),
            (Generators.Fusion, "Fusion", 150_000, 5),
            (Generators.Orbital, "Orbital solar", 475_000, 5.5),
            (Generators.ZeroPoint, "Zero-point", 1_000_000, 7)
        };
}
