namespace Edison
{
    public interface IBuyable
    {
        CashValue CurrentPrice { get; }
        void Get();
    }
}
