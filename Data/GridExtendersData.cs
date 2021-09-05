using System.Collections.Generic;
using Edison;

public static class GridExtendersData
{
    public static List<(Extenders id, string name, double startingPrice, int startingExtension)> Data
        => new() {
            (Extenders.LocalDCGrid, "Local DC Grid", 50, 5),
            (Extenders.LocalACGrid, "Local AC Grid", 200, 20),
            (Extenders.MediumVoltageTransformer, "Medium voltage transformer", 850, 45),
            (Extenders.Substation, "Substation", 7_500, 130),
            (Extenders.HighVoltageTransformer, "High voltage transformer", 135_000, 500),
        };
}
