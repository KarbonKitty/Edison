using System;

namespace Edison
{
    public class PowerGenerator : IBuyable
    {
        public double BasePrice { get; }
        public string Name { get; }
        public double BaseProduction { get; }
        public int NumberBuilt { get; private set; }

        public PowerGenerator(double basePrice, string name, double baseProduction)
        {
            BasePrice = basePrice;
            Name = name;
            BaseProduction = baseProduction;
            NumberBuilt = 0;
        }

        public double CurrentProduction => BaseProduction * NumberBuilt;

            // TODO: shouldn't we have the multiplier defined separately?
        public double CurrentPrice => BasePrice * Math.Pow(1.15, NumberBuilt);

        public void Buy() => NumberBuilt++;

        public bool CanAfford(GameState state) => state.Cash >= CurrentPrice;
    }
}
