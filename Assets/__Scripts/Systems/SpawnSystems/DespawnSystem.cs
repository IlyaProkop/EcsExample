using Leopotam.Ecs;

namespace Client
{
    public class DespawnSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private GameData _gameData;

        private EcsFilter<DespawnTag, PoolObject> _filter;

        public void Run()
        {
            foreach (int idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);
                ref PoolObject poolObject = ref entity.Get<PoolObject>();
                ref GameObjectProvider entityGo = ref entity.Get<GameObjectProvider>();

                poolObject.PoolLink.Pool.Enqueue(entity);
                entityGo.Value.SetActive(false);
                entity.Del<PoolObject>();
                entity.Del<DespawnTag>();
            }
        }
    }
}
