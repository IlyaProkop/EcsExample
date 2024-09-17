using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class EarningViewSystem : IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<CreateEarnViewRequest> _requestFilter;
        private EcsFilter<WorldTextProvider, EarnView, InUseTag>.Exclude<Timer<TimerEarningView>> _initInfoFilter;
        private EcsFilter<WorldTextProvider, EarnView, InUseTag, TimerDoneEvent<TimerEarningView>>.Exclude<DespawnTag> _despawnInfoFilter;

        public void Run()
        {
            foreach (var idx in _requestFilter)
            {
                ref EcsEntity entity = ref _requestFilter.GetEntity(idx);
                ref EarnView entityEarnView = ref entity.Get<EarnView>();

                entity.Get<PoolObject>();

                entity.Get<SpawnPrefab>() = new SpawnPrefab
                {
                    Prefab = _gameData.StaticData.EarnInfoPrefab,
                    Position = entityEarnView.Position + Vector3.up,
                    Rotation = Quaternion.identity,
                    Parent = null,
                    Entity = entity
                };

                entity.Get<PoolObjectRequest>();
                entity.Get<InUseTag>();
                entity.Del<CreateEarnViewRequest>();
            }

            foreach (var idx in _initInfoFilter)
            {
                ref EcsEntity entity = ref _initInfoFilter.GetEntity(idx);
                ref EarnView entityEarnView = ref entity.Get<EarnView>();
                ref WorldTextProvider entityEarnInfoView = ref entity.Get<WorldTextProvider>();

                entityEarnInfoView.Value.text = $"+{Utility.FormatMoney(entityEarnView.Value)}$";
                entity.Get<Timer<TimerEarningView>>().Value = 1.0f;
            }

            foreach (var idx in _despawnInfoFilter)
            {
                _despawnInfoFilter.GetEntity(idx).Get<DespawnTag>();
                _despawnInfoFilter.GetEntity(idx).Del<InUseTag>();
            }
        }
    }

    public struct InUseTag
    {
    }
}