using Leopotam.Ecs;

namespace Client
{
    public class DebugSystem : IEcsInitSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        public void Init()
        {
        }
    }
}