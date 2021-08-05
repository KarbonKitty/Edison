using System.Collections.Generic;

public static class PowerGeneratorsData
{
    public static List<(string name, double startingPrice, double startingProduction)> Data
        => new() {
            ("Solar", 15, 1),
            ("Hydro", 250, 2),
            ("Coal", 1000, 2.5),
            ("Gas", 10_000, 3),
            ("Fission", 65_000, 4),
            ("Fusion", 150_000, 5),
            ("Orbital solar", 475_000, 5.5),
            ("Zero-point", 1_000_000, 7)
        };
}
