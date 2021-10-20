using System;
using System.Threading.Tasks;

namespace Edison
{
    public interface IGameEngine
    {
        void ProcessTime(DateTime newTime);
        bool TryBuy(ICashBuyable buyable);
        ValueTask SaveGame();
        ValueTask<string> GetSavedGameString();
        void LoadGame(string serializedState);
    }
}
