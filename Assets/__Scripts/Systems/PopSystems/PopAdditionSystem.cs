using Leopotam.Ecs;

namespace Client
{
    public class PopAdditionSystem : IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<Pop, ChangePopAdditionRequest> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);
                ref PopcornViewProvider popView = ref entity.Get<PopcornViewProvider>();
                ref ChangePopAdditionRequest changePopAdditionRequest = ref entity.Get<ChangePopAdditionRequest>();

                foreach (var item in popView.Additions)
                    item.SetActive(false);

                popView.Additions[(int)changePopAdditionRequest.Addition].SetActive(true);

                entity.Del<ChangePopAdditionRequest>();
            }
        }
    }
}