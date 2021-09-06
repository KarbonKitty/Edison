namespace Edison
{
    public class Appliance : IBuyable
    {
        public Appliances Id { get; }
        public double Price { get; }
        public string Name { get; }
        public double AdditionalUsage { get; }
        public bool IsBought { get; private set; }

        public Appliance(Appliances id, string name, double price, double additionalUsage, bool isBought = false)
        {
            Id = id;
            Price = price;
            Name = name;
            AdditionalUsage = additionalUsage;
            IsBought = isBought;
        }

        public double CurrentPrice => Price;

        public void Get() => IsBought = true;
    }
}
