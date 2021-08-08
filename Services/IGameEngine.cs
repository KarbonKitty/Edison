using System;
using System.Threading.Tasks;

namespace Edison
{
    public interface IGameEngine
    {
        void ProcessTime(DateTime newTime);
        bool TryBuy(IBuyable buyable);
        ValueTask SaveGame();
        ValueTask LoadGame();
    }
}
