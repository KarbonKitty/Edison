using System;

namespace Edison
{
    public class GridExtender : ICashBuyable, IHideable
    {
        public Extenders Id { get; }
        public CashValue BasePrice { get; }
        public string Name { get; }
        public int BaseExtension { get; }
        public int NumberBuilt { get; private set; }
        public bool IsHidden { get; private set; }

        public GridExtender(Extenders id, string name, double basePrice, int baseExtension, int numberBuilt = 0, bool isHidden = true)
        {
            Id = id;
            BasePrice = new CashValue(basePrice);
            Name = name;
            BaseExtension = baseExtension;
            NumberBuilt = numberBuilt;
            IsHidden = isHidden;
        }

        public int TotalExtension => SingleExtension * NumberBuilt;

        public int SingleExtension => BaseExtension;

        public CashValue CurrentPrice => BasePrice * Math.Pow(1.15, NumberBuilt);

        public Func<GameState, bool> RevealFunction =>  gs => gs.Cash > BasePrice / 2;

        public void Get() => NumberBuilt++;

        public void Reveal() => IsHidden = false;
    }
}
