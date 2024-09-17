using Leopotam.Ecs;

namespace Client
{
    public class InitGameSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private GameData _gameData;
        private GameUI _gameUi;

        public void Init()
        {
            _gameUi.SetShowStateGameScreen(true);
            _gameUi.SetShowStateUpgradeScreen(true);

            EcsEntity gameStateEntity = _world.NewEntity();
            gameStateEntity.Get<ChangeGameStateRequest>().NewGameState = GameState.Playing;
        }
    }
}