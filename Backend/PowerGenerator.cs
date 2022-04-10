using System;

namespace Edison;

public class PowerGenerator : ICashBuyable, IHideable
{
    public Generators Id { get; }
    public CashValue BasePrice { get; }
    public string Name { get; }
    public double BaseProduction { get; }
    public int NumberBuilt { get; private set; }
    public bool IsHidden { get; private set; }

    public PowerGenerator(Generators id, string name, double basePrice, double baseProduction, int numberBuilt = 0, bool isHidden = true)
    {
        Id = id;
        BasePrice = new CashValue(basePrice);
        Name = name;
        BaseProduction = baseProduction;
        NumberBuilt = numberBuilt;
        IsHidden = isHidden;
    }

    public double TotalProduction => SingleProduction * NumberBuilt;

    // Upgrades will be applied here
    public double SingleProduction => BaseProduction;

    // TODO: shouldn't we have the multiplier defined separately?
    public CashValue CurrentPrice => BasePrice * Math.Pow(1.15, NumberBuilt);

    public void Get() => NumberBuilt++;

    public void Reveal() => IsHidden = false;
}
