namespace Edison
{

    public interface IBuyable
    {
        double CurrentPrice { get; }
        bool CanAfford(GameState state);
        void Buy();
    }
}
