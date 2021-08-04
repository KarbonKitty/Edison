using System;
using System.Collections.Generic;

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

            // TODO: temp
            State.Generators.Add(new PowerGenerator(10.0, "Solar", 1.0));
            State.Generators.Add(new PowerGenerator(100, "Hydro", 2.0));
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
            // TODO: write this better
            foreach (var generator in State.Generators)
            {
                var moneyProduced = generator.CurrentProduction * deltaT;
                State.Cash += moneyProduced;
            }
        }
    }
}
