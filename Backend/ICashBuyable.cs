namespace Edison;

public interface ICashBuyable
{
    CashValue CurrentPrice { get; }
    void Get();
}
