using System.Collections.Generic;
using Client.Analytics.AnalyticManager;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class UpgradeSystem : IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;
        private AnalyticService _analyticService;

        private EcsFilter<UpgradeRequest> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);
                ref UpgradeRequest upgrade = ref entity.Get<UpgradeRequest>();

                _gameData.PlayerData.UpgradeLevels[upgrade.UpgradeType]++;

                _world.NewEntity().Get<UpgradeEvent>().UpgradeType = upgrade.UpgradeType;
                
                Dictionary<string, object> param = new Dictionary<string, object>
                {
                    {upgrade.UpgradeType.ToString(), _gameData.PlayerData.UpgradeLevels[upgrade.UpgradeType]}
                };
                _analyticService.LogEvent("upgrade", param);
                
                //_world.NewEntity().Get<PlaySoundRequest>().SoundName = StaticData.AudioSound.BuyUpdateSound;
                entity.Del<UpgradeRequest>();
            }
        }
    }
}