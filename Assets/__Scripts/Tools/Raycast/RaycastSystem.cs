using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class RaycastSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private GameData _gameData;

        public void Run()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = _gameData.SceneData.CameraController.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out var hit, 100f))
                    _world.NewEntity().Get<RaycastEvent>() = new RaycastEvent
                    {
                        GameObject = hit.collider.gameObject,
                        HitPoint = hit.point
                    };
            }
        }
    }
}
