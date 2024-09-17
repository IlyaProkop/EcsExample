using Leopotam.Ecs;

namespace Client
{
    public class ConveyorBeltViewSystem : IEcsInitSystem, IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;
        private WorldGameUI _worldGameUi;

        private EcsFilter<ConveyorProvider> _filter;
        private EcsFilter<ChangeConveyorBeltSpeedRequest> _requestFilter;

        public void Init()
        {
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);
                ref ConveyorProvider conveyor = ref entity.Get<ConveyorProvider>();
                conveyor.BeltScroller.speedY = _gameData.RuntimeData.GetConveyorBeltSpeedView();
            }
        }

        public void Run()
        {
            foreach (var request in _requestFilter)
            {
                ref EcsEntity requestEntity = ref _requestFilter.GetEntity(request);

                foreach (var idx in _filter)
                {
                    ref EcsEntity entity = ref _filter.GetEntity(idx);
                    ref ConveyorProvider conveyor = ref entity.Get<ConveyorProvider>();
                    conveyor.BeltScroller.speedY = _gameData.RuntimeData.GetConveyorBeltSpeedView();
                }
                requestEntity.Del<ChangeConveyorBeltSpeedRequest>();
            }
        }
    }
}