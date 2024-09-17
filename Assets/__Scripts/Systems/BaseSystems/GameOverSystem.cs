using Leopotam.Ecs;

namespace Client
{
    public class GameOverSystem : IEcsRunSystem
    {
        private GameUI _gameUi;

        private EcsFilter<GameFailedEvent> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                EcsEntity entity = _filter.GetEntity(idx);
                //_gameUi.SetShowStateLevelFailedScreen(true);
                entity.Del<GameFailedEvent>();
            }
        }
    }
}