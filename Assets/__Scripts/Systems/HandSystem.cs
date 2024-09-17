using System.Collections.Generic;
using Client.Analytics.AnalyticManager;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class HandSystem : IEcsInitSystem, IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;
        private AnalyticService _analyticService;

        private EcsFilter<HandTakenRequest> _takenfilter;
        private EcsFilter<HandProvider> _handsFilter;
        private EcsFilter<ShakeBagRequest> _shakeFilter;

        public void Init()
        {
            foreach (var hand in _handsFilter)
                _handsFilter.Get1(hand).PopCounterText.text = $"{0}/{_gameData.RuntimeData.InBigBagPopcornAmount()}";
        }

        public void Run()
        {
            foreach (var idx in _takenfilter)
            {
                ref EcsEntity entity = ref _takenfilter.GetEntity(idx);
                ref HandTakenRequest requset = ref entity.Get<HandTakenRequest>();

                foreach (var hand in _handsFilter)
                {
                    ref EcsEntity handEntity = ref _handsFilter.GetEntity(hand);
                    ref HandProvider handLink = ref handEntity.Get<HandProvider>();
                    if (requset.ProductLineId == handLink.ProductLineId)
                    {
                        handEntity.Get<ChangeHandAnimationRequest>().Animation = StaticData.HandAnimations.IsTaken;
                        handLink.PopCounterText.text = $"{0}/{_gameData.RuntimeData.InBigBagPopcornAmount()}";

                        double reward = _gameData.RuntimeData.GetBagEarning(); // TODO: Add additions modificator

                        _world.NewEntity().Get<EarnMoneyEvent>().Value = reward;

                        EcsEntity earnViewEntity = _world.NewEntity();
                        earnViewEntity.Get<EarnView>() = new EarnView
                        {
                            Value = reward,
                            Position = handLink.EarnMoneyPoint.position
                        };
                        handLink.EarnMoneyPS.Play();
                        _gameData.PlayerData.BagSold++;
                        
                        Dictionary<string, object> param = new Dictionary<string, object>
                        {
                            {"soldBagCounter", _gameData.PlayerData.BagSold}
                        };
                        _analyticService.LogEvent("popcorn_bag_sold", param);
                        
                        _world.NewEntity().Get<PlaySoundRequest>().SoundName = StaticData.AudioSound.SellBagSound;
                        entity.Del<HandTakenRequest>();
                    }
                }
            }

            foreach (var idx in _shakeFilter)
            {
                ref EcsEntity entity = ref _shakeFilter.GetEntity(idx);
                ref ShakeBagRequest shakeBagRequest = ref entity.Get<ShakeBagRequest>();

                foreach (var hand in _handsFilter)
                {
                    ref EcsEntity handEntity = ref _handsFilter.GetEntity(hand);
                    ref HandProvider handLink = ref handEntity.Get<HandProvider>();

                    if (shakeBagRequest.ProductLineId == handLink.ProductLineId)
                    {
                        handLink.PopCounterText.text = $"{_gameData.RuntimeData.ReadyToSellCounter[shakeBagRequest.ProductLineId] + 1}/{_gameData.RuntimeData.InBigBagPopcornAmount()}";
                        handEntity.Get<ChangeHandAnimationRequest>().Animation = StaticData.HandAnimations.IsPopIn;
                        entity.Del<ShakeBagRequest>();
                    }
                }
            }
        }
    }
}