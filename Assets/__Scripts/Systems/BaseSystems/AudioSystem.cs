using Leopotam.Ecs;

namespace Client
{
    public class AudioSystem : IEcsInitSystem, IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;
        private AudioManager _audioManager;

        private EcsFilter<SetSoundStateRequest> _settingFilter;
        private EcsFilter<PlaySoundRequest> _requestfilter;

        public void Init()
        {
            _audioManager.ToggleAudio(_gameData.PlayerData.IsSoundOn);
            _audioManager.Play(StaticData.AudioSound.MusicSound);
            _audioManager.Play(StaticData.AudioSound.ConveyorSound);
        }

        public void Run()
        {
            foreach (var sfx in _requestfilter)
            {
                ref EcsEntity entity = ref _requestfilter.GetEntity(sfx);
                _audioManager.Play(_requestfilter.Get1(sfx).SoundName);
                entity.Del<PlaySoundRequest>();
            }

            foreach (var idx in _settingFilter)
            {
                EcsEntity entity = _settingFilter.GetEntity(idx);
                _gameData.PlayerData.IsSoundOn = !_gameData.PlayerData.IsSoundOn;
                _audioManager.ToggleAudio(_gameData.PlayerData.IsSoundOn);
                entity.Del<SetSoundStateRequest>();
            }
        }
    }
}