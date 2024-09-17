using Leopotam.Ecs;

namespace Client
{
    public class UpgradeScreenSystem : IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<ShowLabUpgradeScreenRequest> _showRequestFilter;

        public void Run()
        {

            foreach (var idx in _showRequestFilter)
            {
                ref EcsEntity entity = ref _showRequestFilter.GetEntity(idx);
                _gameUi.SetShowStateLabUpgradeScreen(true);
                _world.NewEntity().Get<PlaySoundRequest>().SoundName = StaticData.AudioSound.UiNavigationSound;
                entity.Del<ShowLabUpgradeScreenRequest>();
            }
        }
    }
}