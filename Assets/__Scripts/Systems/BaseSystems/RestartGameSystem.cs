using Leopotam.Ecs;
using UnityEngine.SceneManagement;

namespace Client
{
    public class RestartGameSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private GameData _gameData;
        private GameUI _gameUi;

        private EcsFilter<RestartGameRequest> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                EcsEntity entity = _filter.GetEntity(idx);
                _gameData.ResetData();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                entity.Del<RestartGameRequest>();
            }

            //var levelName = staticData.Levels.Scenes[index];
            //SceneManager.LoadScene(levelName);
        }
    }
}