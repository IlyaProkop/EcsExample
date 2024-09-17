using Leopotam.Ecs;

namespace Client
{
    public class CameraSystem : IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<SetCameraZoomReqeust> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);
                ref SetCameraZoomReqeust zoomRequest = ref entity.Get<SetCameraZoomReqeust>();

                float zoom = zoomRequest.ZoomObjects * 0.5f;
                _gameData.SceneData.CameraController.SetZoom(zoom);

                entity.Del<SetCameraZoomReqeust>();
            }
        }
    }
}