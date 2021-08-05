using System;
using System.Collections.Generic;
using System.Linq;

namespace Edison
{
    public class GameEngine : IGameEngine
    {
        public GameState State { get; set; }

        public GameEngine()
        {
            State = new GameState
            {
                LastTick = DateTime.Now,
                LastDiff = 0,
                Cash = 100,
                Generators = new List<PowerGenerator>()
            };

            foreach (var (name, price, production) in PowerGeneratorsData.Data)
            {
                State.Generators.Add(new PowerGenerator(price, name, production));
            }
        }

        public void ProcessTime(DateTime newTime)
        {
            var deltaT = newTime - State.LastTick;
            State.LastDiff = deltaT.TotalMilliseconds;
            State.LastTick = newTime;
            RunGenerators(deltaT.TotalMilliseconds / 1000);
        }

        public bool TryBuy(IBuyable buyable)
        {
            if (State.Cash >= buyable.CurrentPrice)
            {
                State.Cash -= buyable.CurrentPrice;
                buyable.Get();
                return true;
            }
            return false;
        }

        private void RunGenerators(double deltaT)
        {
            var moneyProduced = State.Generators.Sum(g => g.CurrentProduction * deltaT);
            State.Cash += moneyProduced;
        }
    }
}
