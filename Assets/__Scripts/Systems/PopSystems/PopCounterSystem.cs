using System.Collections.Generic;
using Client.Analytics.AnalyticManager;
using DG.Tweening;
using Leopotam.Ecs;

namespace Client
{
    public class PopCounterSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private GameData _gameData;
        private GameUI _gameUi;
        private AnalyticService _analyticService;

        private EcsFilter<AddPopEvent> _filter;
        private EcsFilter<AddGoldPopEvent> _goldFilter;
        private EcsFilter<SpendGoldPopEvent> _spendGoldFilter;
        private EcsFilter<AddReadyToSellPopEvent> _readyToSellFilter;

        public void Init()
        {
            _gameUi.GameScreen.UpdatePopcornAmountText(_gameData.PlayerData.PopcornAmount);
            _gameUi.GameScreen.UpdateGoldPopcornAmountText(_gameData.PlayerData.GoldPopcornAmount);
        }

        public void Run()
        {
            foreach (var idx in _filter)
            {
                _gameData.PlayerData.PopcornAmount += 1;
                _gameUi.GameScreen.UpdatePopcornAmountText(_gameData.PlayerData.PopcornAmount);
                _filter.GetEntity(idx).Del<AddPopEvent>();
            }

            foreach (var idx in _goldFilter)
            {
                _gameData.PlayerData.GoldPopcornAmount += 1;
                _gameUi.GameScreen.UpdateGoldPopcornAmountText(_gameData.PlayerData.GoldPopcornAmount);
                _gameUi.GameScreen.BounceGoldPopcron();
                _goldFilter.GetEntity(idx).Del<AddGoldPopEvent>();
                
                Dictionary<string, object> param = new Dictionary<string, object>
                {
                    {"goldPopcornAmount", _gameData.PlayerData.GoldPopcornAmount}
                };
                _analyticService.LogEvent("gold_tap", param);
            }

            foreach (var idx in _spendGoldFilter)
            {
                _gameData.PlayerData.GoldPopcornAmount -= _spendGoldFilter.Get1(idx).Value;
                _gameUi.GameScreen.UpdateGoldPopcornAmountText(_gameData.PlayerData.GoldPopcornAmount);
                _gameUi.GameScreen.BounceGoldPopcron();
                _spendGoldFilter.GetEntity(idx).Del<SpendGoldPopEvent>();
            }

            foreach (var idx in _readyToSellFilter)
            {
                int productLine = _readyToSellFilter.Get1(idx).ProductLineId;
                _gameData.RuntimeData.ReadyToSellCounter[productLine] += 1;

                _readyToSellFilter.GetEntity(idx).Del<AddReadyToSellPopEvent>();
                if (_gameData.RuntimeData.ReadyToSellCounter[productLine] >= _gameData.RuntimeData.InBigBagPopcornAmount())
                {
                    _gameData.RuntimeData.ReadyToSellCounter[productLine] = 0;
                    _world.NewEntity().Get<HandTakenRequest>().ProductLineId = productLine;
                }
            }
        }
    }
}