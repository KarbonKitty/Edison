using System.Collections.Generic;

namespace Edison
{
    public static class GridExtendersData
    {
        public static List<(Extenders id, string name, double startingPrice, int startingExtension, bool isHidden)> Data
            => new()
            {
                (Extenders.LocalDCGrid, "Local DC Grid", 50, 5, false),
                (Extenders.LocalACGrid, "Local AC Grid", 200, 20, true),
                (Extenders.MediumVoltageTransformer, "Medium voltage transformer", 850, 45, true),
                (Extenders.Substation, "Substation", 7_500, 130, true),
                (Extenders.HighVoltageTransformer, "High voltage transformer", 135_000, 500, true),
            };
    }
}
