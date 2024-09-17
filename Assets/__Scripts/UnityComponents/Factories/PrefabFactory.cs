using Leopotam.Ecs;
using UnityEngine;

public class PrefabFactory : MonoBehaviour
{
    private EcsWorld _world;

    public void InjectWorld(EcsWorld world)
    {
        _world = world;
    }

    public void Spawn(SpawnPrefab spawnPrefab)
    {
        GameObject _go = Instantiate(spawnPrefab.Prefab, spawnPrefab.Position, spawnPrefab.Rotation, spawnPrefab.Parent);
        var monoEntity = _go.GetComponent<MonoEntity>();
        if (monoEntity == null)
            return;
        EcsEntity ecsEntity = _world.NewEntity();
        monoEntity.Provide(ref ecsEntity);
    }

    public EcsEntity SpawnAndGetEntity(SpawnPrefab spawnPrefab)
    {
        GameObject _go = Instantiate(spawnPrefab.Prefab, spawnPrefab.Position, spawnPrefab.Rotation, spawnPrefab.Parent);
        var monoEntity = _go.GetComponent<MonoEntity>();
        EcsEntity ecsEntity = _world.NewEntity();
        monoEntity.Provide(ref ecsEntity);
        return ecsEntity;
    }

    public void SpawnPrefabWithPreInitEntity(SpawnPrefab spawnPrefab, ref EcsEntity entity)
    {
        GameObject _go = Instantiate(spawnPrefab.Prefab, spawnPrefab.Position, spawnPrefab.Rotation, spawnPrefab.Parent);
        var monoEntity = _go.GetComponent<MonoEntity>();
        if (monoEntity == null)
            return;
        monoEntity.Provide(ref entity);
    }

    public ref EcsEntity SpawnPrefabWithPreInitEntityAndGetEntity(SpawnPrefab spawnPrefab, ref EcsEntity entity)
    {
        GameObject _go = Instantiate(spawnPrefab.Prefab, spawnPrefab.Position, spawnPrefab.Rotation, spawnPrefab.Parent);
        var monoEntity = _go.GetComponent<MonoEntity>();
        monoEntity.Provide(ref entity);
        return ref entity;
    }
}
