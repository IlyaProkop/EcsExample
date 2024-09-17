using Leopotam.Ecs;

namespace Client
{
    public class SpawnSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private GameData _gameData;

        private EcsFilter<SpawnPrefab> _spawnFilter = null;

        private PrefabFactory _factory;

        public void PreInit()
        {
            _factory = _gameData.SceneData.PrefabFactory;
            _factory.InjectWorld(_world);
        }

        public void Run()
        {
            if (_spawnFilter.IsEmpty())
                return;

            foreach (int index in _spawnFilter)
            {
                ref EcsEntity spawnEntity = ref _spawnFilter.GetEntity(index);
                var spawnPrefabData = spawnEntity.Get<SpawnPrefab>();

                if (spawnPrefabData.Entity.IsNull())
                    _factory.Spawn(spawnPrefabData);
                else
                    _factory.SpawnPrefabWithPreInitEntity(spawnPrefabData, ref spawnPrefabData.Entity);

                spawnEntity.Del<SpawnPrefab>();
            }
        }
    }
}
