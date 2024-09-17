using Leopotam.Ecs;

namespace Client
{
    public class PopEmotionSystem : IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<Pop, ChangePopEmotionRequest> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);
                ref PopcornViewProvider popView = ref entity.Get<PopcornViewProvider>();
                ref ChangePopEmotionRequest changePopEmotionRequest = ref entity.Get<ChangePopEmotionRequest>();

                foreach (var item in popView.Emotions)
                    item.SetActive(false);

                popView.Emotions[(int)changePopEmotionRequest.Emotion].SetActive(true);

                entity.Del<ChangePopEmotionRequest>();
            }
        }
    }
}