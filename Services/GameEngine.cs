using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using System.Text.Json;

namespace Edison
{
    public class GameEngine : IGameEngine
    {
        private const int BasePowerUsage = 1;

        public GameState State { get; set; }

        public double PowerUsage => BasePowerUsage + State.Appliances.Where(a => a.IsBought).Sum(a => a.AdditionalUsage);
        public double TotalPowerUsage => State.GridSize * PowerUsage;

        private readonly IJSRuntime JS;

        public GameEngine(IJSRuntime js)
        {
            JS = js;

            State = new GameState
            {
                LastTick = DateTime.Now,
                LastDiff = 0,
                Cash = 100,
                GridSize = 0,
                TotalPowerProduction = 0,
                Generators = new List<PowerGenerator>(),
                Extenders = new List<GridExtender>(),
                Appliances = new List<Appliance>()
            };

            foreach (var (id, name, price, production) in PowerGeneratorsData.Data)
            {
                State.Generators.Add(new PowerGenerator(id, name, price, production));
            }

            foreach (var (id, name, price, extension) in GridExtendersData.Data)
            {
                State.Extenders.Add(new GridExtender(id, name, price, extension));
            }

            foreach (var (id, name, price, usage) in AppliancesData.Data)
            {
                State.Appliances.Add(new Appliance(id, name, price, usage));
            }
        }

        public void ProcessTime(DateTime newTime)
        {
            var deltaT = newTime - State.LastTick;
            State.LastDiff = deltaT.TotalMilliseconds;
            State.LastTick = newTime;
            State.GridSize = State.Extenders.Sum(e => e.TotalExtension);
            State.TotalPowerProduction = RunGenerators(deltaT.TotalMilliseconds / 1000);
        }

        public bool CanAfford(IBuyable buyable) => State.Cash >= buyable.CurrentPrice;

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

        public async ValueTask SaveGame()
        {
            var gameStateDto = new GameStateDto
            {
                LastTick = State.LastTick.Ticks,
                LastDiff = State.LastDiff,
                Cash = State.Cash,
                GridSize = State.GridSize,
                TotalPowerProduction = State.TotalPowerProduction,
                Generators = State.Generators.ConvertAll(g => new GeneratorDto { Id = g.Id, NumberBuilt = g.NumberBuilt }),
                Extenders = State.Extenders.ConvertAll(e => new ExtenderDto { Id = e.Id, NumberBuilt = e.NumberBuilt }),
                Appliances = State.Appliances.ConvertAll(a => new ApplianceDto { Id = a.Id, IsBought = a.IsBought })
            };
            await JS.InvokeVoidAsync("localStorage.setItem", "data", JsonSerializer.Serialize(gameStateDto));
        }

        public async ValueTask<string> GetSavedGameString()
            => await JS.InvokeAsync<string>("localStorage.getItem", "data");

        public void LoadGame(string serializedState)
        {
            var gameStateDto = JsonSerializer.Deserialize<GameStateDto>(serializedState);
            State = new GameState
            {
                LastTick = new DateTime(gameStateDto.LastTick),
                LastDiff = gameStateDto.LastDiff,
                Cash = gameStateDto.Cash,
                GridSize = gameStateDto.GridSize,
                TotalPowerProduction = gameStateDto.TotalPowerProduction,
                Generators = gameStateDto.Generators.ConvertAll(g => {
                    var (id, name, startingPrice, startingProduction) = PowerGeneratorsData.Data.Single(pg => pg.id == g.Id);
                    return new PowerGenerator(g.Id, name, startingPrice, startingProduction, g.NumberBuilt);
                }),
                Extenders = gameStateDto.Extenders.ConvertAll(e => {
                    var (id, name, startingPrice, startingExtension) = GridExtendersData.Data.Single(ge => ge.id == e.Id);
                    return new GridExtender(e.Id, name, startingPrice, startingExtension, e.NumberBuilt);
                }),
                Appliances = gameStateDto.Appliances.ConvertAll(a => {
                    var (id, name, price, additionalUsage) = AppliancesData.Data.Single(ad => ad.IDictionary == a.Id);
                    return new Appliance(a.Id, name, price, additionalUsage, a.IsBought);
                })
            };
        }

        private double RunGenerators(double deltaT)
        {
            var totalPowerProduction = State.Generators.Sum(g => g.TotalProduction);
            var powerProduced = totalPowerProduction * deltaT;
            var powerSold = powerProduced > TotalPowerUsage ? TotalPowerUsage : powerProduced;
            State.Cash += powerSold;
            return totalPowerProduction;
        }
    }
}
