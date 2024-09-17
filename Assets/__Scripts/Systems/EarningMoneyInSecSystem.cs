using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class EarningMoneyInSecSystem : IEcsInitSystem, IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<MoneyInSecUpdateTimerContainer>.Exclude<Timer<TimerUpdateMoneyInSec>> _filter;

        public void Init()
        {
            _world.NewEntity().Get<MoneyInSecUpdateTimerContainer>();
        }

        public void Run()
        {
            foreach (var idx in _filter)
            {
                float heatingSpeed = _gameData.RuntimeData.GetHeatingSpeed();
                float popSpeed = _gameData.RuntimeData.GetPopSpeed();
                float spawnTime= _gameData.RuntimeData.GetPopSpawnTime();
                float cookingTime = _gameData.RuntimeData.GetPopCookingTime();

                double popEarn = _gameData.RuntimeData.GetPopEarning();
                double moneyInSecFromPops = popEarn / ((spawnTime + cookingTime) / (popSpeed + heatingSpeed));

                double bagEarn = _gameData.RuntimeData.GetBagEarning();
                double moneyInSecFromBags = bagEarn / ((spawnTime + cookingTime) / (popSpeed + heatingSpeed));

                // TODO: GetMilkyChocoMultipiler()

                int conveyorCounter = 0;
                foreach (var item in _gameData.PlayerData.ConveyorBuyed)
                    if (item)
                        conveyorCounter++;

                _gameData.PlayerData.MoneyInSec = moneyInSecFromPops + moneyInSecFromBags;
                _gameData.PlayerData.MoneyInSec *= conveyorCounter;
                _gameData.PlayerData.MoneyInSec *= 0.1f;

                _gameUi.GameScreen.UpdateMoneyInSecText(_gameData.PlayerData.MoneyInSec);

                _filter.GetEntity(idx).Get<Timer<TimerUpdateMoneyInSec>>().Value = 1.0f;
            }
        }
    }
}