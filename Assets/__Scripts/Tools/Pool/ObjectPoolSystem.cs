using Leopotam.Ecs;
using UnityEngine;
using System.Collections.Generic;

namespace Client
{
    public class ObjectPoolSystem : IEcsInitSystem, IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<ObjectPoolProvider> _poolFilter;
        private EcsFilter<SpawnPrefab, PoolObjectRequest> _requestsFilter;

        public void Init()
        {
            foreach (var idx in _poolFilter)
            {
                ref EcsEntity entity = ref _poolFilter.GetEntity(idx);
                ref GameObjectProvider entityGo = ref entity.Get<GameObjectProvider>();
                ref ObjectPoolProvider pool = ref entity.Get<ObjectPoolProvider>();

                pool.Pool = new Queue<EcsEntity>();

                for (int i = 0; i < pool.PoolDepth; i++)
                {
                    EcsEntity poolObjectEntity = _world.NewEntity();

                    poolObjectEntity.Get<SpawnPrefab>() = new SpawnPrefab
                    {
                        Prefab = pool.Prefab,
                        Position = Vector3.one * 100.0f,
                        Rotation = Quaternion.identity,
                        Parent = entityGo.Value.transform,
                        Entity = poolObjectEntity
                    };

                    _gameData.SceneData.PrefabFactory.SpawnPrefabWithPreInitEntity(poolObjectEntity.Get<SpawnPrefab>(), ref poolObjectEntity);
                    poolObjectEntity.Del<SpawnPrefab>();
                    ref GameObjectProvider poolObjectEntityGo = ref poolObjectEntity.Get<GameObjectProvider>();

                    poolObjectEntityGo.Value.SetActive(false);
                    pool.Pool.Enqueue(poolObjectEntity);
                }
            }
        }

        public void Run()
        {
            foreach (var request in _requestsFilter)
            {
                ref EcsEntity requestEntity = ref _requestsFilter.GetEntity(request);
                ref SpawnPrefab spawnPrefabRequest = ref requestEntity.Get<SpawnPrefab>();

                foreach (var poolItem in _poolFilter)
                {
                    ref EcsEntity poolEntity = ref _poolFilter.GetEntity(poolItem);
                    ref ObjectPoolProvider pool = ref poolEntity.Get<ObjectPoolProvider>();

                    if (spawnPrefabRequest.Prefab == pool.Prefab) // in this pool
                    {
                        requestEntity.Get<PoolObject>().PoolLink = pool;

                        requestEntity.Del<PoolObjectRequest>();

                        if (pool.Pool.Count > 0)
                        {
                            EcsEntity entity = pool.Pool.Dequeue();

                            MonoEntity monoEntity = entity.Get<GameObjectProvider>().Value.GetComponent<MonoEntity>();
                            entity = _world.NewEntity();
                            monoEntity.Provide(ref entity);

                            ref GameObjectProvider entityGo = ref entity.Get<GameObjectProvider>();

                            entity.Get<GameObjectProvider>().Value.transform.position = spawnPrefabRequest.Position;
                            entity.Get<GameObjectProvider>().Value.transform.rotation = spawnPrefabRequest.Rotation;
                            entity.Get<GameObjectProvider>().Value.transform.SetParent(null);
                            entity.Get<GameObjectProvider>().Value.transform.localScale = pool.Prefab.transform.localScale;
                            entity.Get<GameObjectProvider>().Value.transform.parent = spawnPrefabRequest.Parent;
                            entity.Get<GameObjectProvider>().Value.SetActive(true);

                            requestEntity.MoveTo(entity);
                            entity.Del<SpawnPrefab>();
                        }
                    }
                }
            }
        }

    }
}