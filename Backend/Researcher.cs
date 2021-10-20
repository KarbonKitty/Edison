using System;

namespace Edison
{
    public class Researcher : IBuyable
    {
        public Researchers Id { get; }
        public CashValue BasePrice { get; }
        public string Name { get; }
        public ResearchPointsValue BaseProduction { get; }
        public int NumberBuilt { get; private set; }

        public Researcher(Researchers id, string name, double basePrice, double baseProduction, int numberBuilt = 0)
        {
            Id = id;
            BasePrice = new CashValue(basePrice);
            Name = name;
            BaseProduction = new ResearchPointsValue(baseProduction);
            NumberBuilt = numberBuilt;
        }

        public ResearchPointsValue SingleProduction => BaseProduction;

        public ResearchPointsValue TotalProduction => SingleProduction * NumberBuilt;

        public CashValue CurrentPrice => BasePrice * Math.Pow(1.15, NumberBuilt);

        public void Get() => NumberBuilt++;
    }
}
