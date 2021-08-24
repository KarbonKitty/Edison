using System;

namespace Edison
{
    public class GridExtender : IBuyable
    {
        public Extenders Id { get; }
        public double BasePrice { get; }
        public string Name { get; }
        public int BaseExtension { get; }
        public int NumberBuilt { get; private set; }

        public GridExtender(Extenders id, string name, double basePrice, int baseExtension, int numberBuilt = 0)
        {
            Id = id;
            BasePrice = basePrice;
            Name = name;
            BaseExtension = baseExtension;
            NumberBuilt = numberBuilt;
        }

        public int TotalExtension => SingleExtension * NumberBuilt;

        public int SingleExtension => BaseExtension;

        public double CurrentPrice => BasePrice * Math.Pow(1.15, NumberBuilt);

        public void Get() => NumberBuilt++;

        public bool CanAfford(GameState state) => state.Cash >= CurrentPrice;
    }
}
