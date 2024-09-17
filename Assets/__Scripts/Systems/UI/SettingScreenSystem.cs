using Leopotam.Ecs;

namespace Client
{
    public class SettingScreenSystem : IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<ShowSettingScreenRequest> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);
                _gameUi.SetShowStateSettingScreen(true);
                _world.NewEntity().Get<PlaySoundRequest>().SoundName = StaticData.AudioSound.UiNavigationSound;
                entity.Del<ShowSettingScreenRequest>();
            }
        }
    }
}