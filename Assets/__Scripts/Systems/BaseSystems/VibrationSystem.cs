using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class VibrationSystem : IEcsInitSystem, IEcsRunSystem
    {
        private GameData _gameData;

        private EcsFilter<SetVibrationStateRequest> _settingFilter;
        private EcsFilter<VibrationRequest> _requestFilter;

        public void Init()
        {
            Handheld.Vibrate();
        }

        public void Run()
        {
            foreach (var idx in _settingFilter)
            {
                EcsEntity entity = _settingFilter.GetEntity(idx);
                _gameData.PlayerData.IsVibrationOn = !_gameData.PlayerData.IsVibrationOn;
                entity.Del<SetVibrationStateRequest>();
            }

            foreach (var idx in _requestFilter)
            {
                EcsEntity entity = _requestFilter.GetEntity(idx);
                MoreMountains.NiceVibrations.MMVibrationManager.Haptic(entity.Get<VibrationRequest>().HapticType);
                entity.Del<VibrationRequest>();
            }
        }
    }
}