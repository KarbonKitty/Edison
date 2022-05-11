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
                Cash = new CashValue(100),
                GridSize = 0,
                TotalPowerProduction = 0,
                Research = new ResearchPointsValue(0),
                Generators = new List<PowerGenerator>(),
                Extenders = new List<GridExtender>(),
                Appliances = new List<Appliance>(),
                Researchers = new List<Researcher>()
            };

            foreach (var (id, name, price, production, hidden) in PowerGeneratorsData.Data)
            {
                State.Generators.Add(new PowerGenerator(id, name, price, production, isHidden: hidden));
            }

            foreach (var (id, name, price, extension, isHidden) in GridExtendersData.Data)
            {
                State.Extenders.Add(new GridExtender(id, name, price, extension, isHidden: isHidden));
            }

            foreach (var (id, name, price, usage) in AppliancesData.Data)
            {
                State.Appliances.Add(new Appliance(id, name, price, usage));
            }

            foreach (var (id, name, price, production) in ResearchersData.Data)
            {
                State.Researchers.Add(new Researcher(id, name, price, production));
            }
        }

        public void ProcessTime(DateTime newTime)
        {
            var deltaT = newTime - State.LastTick;
            State.LastDiff = deltaT.TotalMilliseconds;
            State.LastTick = newTime;
            State.GridSize = State.Extenders.Sum(e => e.TotalExtension);
            State.TotalPowerProduction = RunGenerators(deltaT.TotalMilliseconds / 1000);
            RunResearchers(deltaT.TotalMilliseconds / 1000);
            ProcessEvents();
        }

        public bool CanAfford(ICashBuyable buyable) => State.Cash >= buyable.CurrentPrice;

        public bool CanAfford(IResearchable researchable) => State.Research >= researchable.CurrentPrice;

        public bool TryBuy(ICashBuyable buyable)
        {
            if (State.Cash < buyable.CurrentPrice)
            {
                return false;
            }

            State.Cash -= buyable.CurrentPrice;
            buyable.Get();
            return true;
        }

        public bool TryBuy(IResearchable researchable)
        {
            if (State.Research < researchable.CurrentPrice)
            {
                return false;
            }

            State.Research -= researchable.CurrentPrice;
            researchable.Get();
            return true;
        }

        public async ValueTask SaveGame()
        {
            var gameStateDto = new GameStateDto
            {
                LastTick = State.LastTick.Ticks,
                LastDiff = State.LastDiff,
                Cash = State.Cash.Value,
                Research = State.Research.Value,
                GridSize = State.GridSize,
                TotalPowerProduction = State.TotalPowerProduction,
                Generators = State.Generators.ConvertAll(g => new GeneratorDto { Id = g.Id, NumberBuilt = g.NumberBuilt, IsHidden = g.IsHidden }),
                Extenders = State.Extenders.ConvertAll(e => new ExtenderDto { Id = e.Id, NumberBuilt = e.NumberBuilt }),
                Appliances = State.Appliances.ConvertAll(a => new ApplianceDto { Id = a.Id, IsBought = a.IsBought }),
                Researchers = State.Researchers.ConvertAll(r => new ResearcherDto { Id = r.Id, NumberBuilt = r.NumberBuilt })
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
                Cash = new CashValue(gameStateDto.Cash),
                Research = new ResearchPointsValue(gameStateDto.Research),
                GridSize = gameStateDto.GridSize,
                TotalPowerProduction = gameStateDto.TotalPowerProduction,
                Generators = gameStateDto.Generators.ConvertAll(g => {
                    var (id, name, startingPrice, startingProduction, isHidden) = PowerGeneratorsData.Data.Single(pg => pg.id == g.Id);
                    return new PowerGenerator(g.Id, name, startingPrice, startingProduction, g.NumberBuilt, g.IsHidden);
                }),
                Extenders = gameStateDto.Extenders.ConvertAll(e => {
                    var (id, name, startingPrice, startingExtension, isHidden) = GridExtendersData.Data.Single(ge => ge.id == e.Id);
                    return new GridExtender(e.Id, name, startingPrice, startingExtension, e.NumberBuilt, e.IsHidden);
                }),
                Appliances = gameStateDto.Appliances.ConvertAll(a => {
                    var (id, name, price, additionalUsage) = AppliancesData.Data.Single(ad => ad.id == a.Id);
                    return new Appliance(a.Id, name, price, additionalUsage, a.IsBought);
                }),
                Researchers = gameStateDto.Researchers.ConvertAll(r => {
                    var (id, name, price, startingProduction) = ResearchersData.Data.Single(rd => rd.id == r.Id);
                    return new Researcher(r.Id, name, price, startingProduction, r.NumberBuilt);
                })
            };
        }

        private double RunGenerators(double deltaT)
        {
            var totalPowerProduction = State.Generators.Sum(g => g.TotalProduction);
            var powerProduced = totalPowerProduction * deltaT;
            var powerSold = powerProduced > TotalPowerUsage ? TotalPowerUsage : powerProduced;
            State.Cash += new CashValue(powerSold);
            return totalPowerProduction;
        }

        private void RunResearchers(double deltaT)
        {
            var totalResearchProduction = State.Researchers.Sum(r => r.TotalProduction.Value);
            var researchProduced = totalResearchProduction * deltaT;
            State.Research += new ResearchPointsValue(researchProduced);
        }

        private void ProcessEvents()
        {
            foreach (var h in State.Hideables.Where(x => x.IsHidden && x.RevealFunction(State)))
            {
                h.Reveal();
            }
        }
    }
}
