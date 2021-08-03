using System;

namespace Edison
{
    public interface IGameEngine
    {
        void ProcessTime(DateTime newTime);
        bool TryBuy(IBuyable buyable);
    }
}
