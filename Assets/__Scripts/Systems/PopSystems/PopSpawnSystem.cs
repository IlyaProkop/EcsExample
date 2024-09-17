using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class PopSpawnSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private GameData _gameData;

        private EcsFilter<ConveyorProvider, LaunchPop>.Exclude<Timer<TimerIntervalSpawnPop>> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);
                ref ConveyorProvider conveyor = ref entity.Get<ConveyorProvider>();
                ref GameObjectProvider conveyorGo = ref entity.Get<GameObjectProvider>();

                entity.Get<Timer<TimerIntervalSpawnPop>>().Value = _gameData.RuntimeData.GetPopSpawnTime();

                bool isGold = _gameData.RuntimeData.GetGoldPopcornChance();

                EcsEntity popEntity = _world.NewEntity();
                popEntity.Get<Pop>() = new Pop
                {
                    Conveyor = conveyor,
                    ConveyorId = conveyor.Id,
                    ProductLineId = conveyor.ProductLineId
                };

                popEntity.Get<ReadyToLaunch>();

                Transform spawnPoint = conveyor.SpawnPoints[Random.Range(0, conveyor.SpawnPoints.Count)];
                popEntity.Get<SpawnPrefab>() = new SpawnPrefab
                {
                    Prefab = isGold ? _gameData.StaticData.GoldPopcornPrefab : _gameData.StaticData.PopcornPrefab,
                    Position = spawnPoint.position,
                    Rotation = spawnPoint.rotation,
                    Parent = conveyorGo.Value.transform,
                    Entity = popEntity
                };

                if (isGold)
                    popEntity.Get<GoldPop>();

                popEntity.Get<PoolObjectRequest>();

                _world.NewEntity().Get<AddPopEvent>();
            }
        }
    }
}