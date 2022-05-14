using System;

namespace Edison
{
    public class Researcher : ICashBuyable, IHideable
    {
        public Researchers Id { get; }
        public CashValue BasePrice { get; }
        public string Name { get; }
        public ResearchPointsValue BaseProduction { get; }
        public int NumberBuilt { get; private set; }
        public bool IsHidden { get; private set; }

        public Researcher(Researchers id, string name, double basePrice, double baseProduction, int numberBuilt = 0, bool isHidden = true)
        {
            Id = id;
            BasePrice = new CashValue(basePrice);
            Name = name;
            BaseProduction = new ResearchPointsValue(baseProduction);
            NumberBuilt = numberBuilt;
            IsHidden = isHidden;
        }

        public ResearchPointsValue SingleProduction => BaseProduction;

        public ResearchPointsValue TotalProduction => SingleProduction * NumberBuilt;

        public CashValue CurrentPrice => BasePrice * Math.Pow(1.15, NumberBuilt);

        public Func<GameState, bool> RevealFunction => gs => gs.Cash > BasePrice / 2;

        public void Get() => NumberBuilt++;

        public void Reveal() => IsHidden = false;
    }
}
