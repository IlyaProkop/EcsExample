using Leopotam.Ecs;
using UnityEngine;

public class DestroyGameObjectSystem : IEcsRunSystem
{
    private EcsFilter<DestroyTag, GameObjectProvider> _goToDestroyFilter;

    public void Run()
    {
        foreach (var idx in _goToDestroyFilter)
        {
            ref EcsEntity entity = ref _goToDestroyFilter.GetEntity(idx);
            ref GameObjectProvider go = ref _goToDestroyFilter.Get2(idx);
            //GameObject.Destroy(go.Value);
            go.Value.SetActive(false);
            entity.Destroy();
        }
    }
}