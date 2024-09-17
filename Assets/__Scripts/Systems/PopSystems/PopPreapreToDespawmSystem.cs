using DG.Tweening;
using Leopotam.Ecs;

namespace Client
{
    public class PopPreapreToDespawmSystem : IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<Pop, PrepareToDespawnRequest> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);
                entity.Get<DespawnTag>();
                entity.Get<GameObjectProvider>().Value.transform.DOComplete(); 
                entity.Get<ChangePopViewRequest>().PopBody = StaticData.PopBody.RawCorn;
                entity.Get<ChangePopEmotionRequest>().Emotion = StaticData.PopEmotions.None;
                entity.Get<ChangePopAdditionRequest>().Addition = StaticData.PopAdditions.None;
                entity.Get<RigidbodyProvider>().Value.isKinematic = false;
                entity.Del<PrepareToDespawnRequest>();
            }
        }
    }
}