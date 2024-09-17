using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class OfflineBonusSystem : IEcsInitSystem, IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<GetOfflineBonusRewardRequest> _filter;

        double reward = 0;

        public void Init()
        {
            int sec = (int)Mathf.Clamp(TimeManager.Offline.LoadOfflineBonus().Second, 60 * 60, 60 * 60 * _gameData.BalanceData.OfflineTimeHourCap.y);
            reward = sec * _gameData.PlayerData.MoneyInSec * Mathf.Pow(GameData.Instance.BalanceData.EarningOfflineMultiplierForLevel, _gameData.PlayerData.UpgradeLevels[StaticData.UpgradeType.Earn]);

            if (sec > 60 * 60 * _gameData.BalanceData.OfflineTimeHourCap.x)
                _gameUi.SetShowStateOfflineBonusScreen(true, reward);
        }

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);
                ref GetOfflineBonusRewardRequest rewardRequest = ref entity.Get<GetOfflineBonusRewardRequest>();
                _world.NewEntity().Get<EarnMoneyEvent>().Value = reward;
                entity.Del<GetOfflineBonusRewardRequest>();
            }
        }
    }
}