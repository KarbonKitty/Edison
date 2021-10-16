using System.Collections.Generic;

namespace Edison
{
    public static class AppliancesData
    {
        public static List<(Appliances IDictionary, string name, double price, double additionalUsage)> Data
            => new()
            {
                (Appliances.Lightbulb, "Lightbulb", 150, 0.25),
                (Appliances.WashingMachine, "Washing machine", 2000, 1),
                (Appliances.HairDryer, "Hair dryer", 3000, 0.5),
                (Appliances.Refrigerator, "Refrigerator", 15_000, 2.25)
            };
    }
}
