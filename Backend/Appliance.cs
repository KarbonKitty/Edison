namespace Edison
{
    public class Appliance : IResearchable
    {
        public Appliances Id { get; }
        public ResearchPointsValue Price { get; }
        public string Name { get; }
        public double AdditionalUsage { get; }
        public bool IsBought { get; private set; }

        public Appliance(Appliances id, string name, double price, double additionalUsage, bool isBought = false)
        {
            Id = id;
            Price = new ResearchPointsValue(price);
            Name = name;
            AdditionalUsage = additionalUsage;
            IsBought = isBought;
        }

        public ResearchPointsValue CurrentPrice => Price;

        public void Get() => IsBought = true;
    }
}
