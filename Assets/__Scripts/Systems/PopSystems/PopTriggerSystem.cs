using Leopotam.Ecs;

namespace Client
{
    public class PopTriggerSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private GameData _gameData;

        private EcsFilter<Pop, OnTriggerEnterEvent> _enterFilter;
        private EcsFilter<Pop, OnTriggerExitEvent> _exitfilter;

        public void Run()
        {
            foreach (var idx in _enterFilter)
            {
                ref EcsEntity entity = ref _enterFilter.GetEntity(idx);
                ref GameObjectProvider entityGo = ref entity.Get<GameObjectProvider>();
                //ref ParticleSystemLink entityPs = ref entity.Get<ParticleSystemLink>();
                ref OnTriggerEnterEvent entityCollision = ref entity.Get<OnTriggerEnterEvent>();
                ref PopcornViewProvider popView = ref entity.Get<PopcornViewProvider>();

                //Despawn
                if (entityCollision.Collider.gameObject.CompareTag(_gameData.StaticData.DespawnTag))
                {
                    entity.Get<StopAllMovingRequest>();
                    entity.Get<PrepareToDespawnRequest>();
                }

                if (entityCollision.Collider.gameObject.CompareTag(_gameData.StaticData.CookingZoneTag))
                    entity.Get<Cooking>();

                if (!entity.Has<GoldPop>() && entityCollision.Collider.gameObject.CompareTag(_gameData.StaticData.ChocolateAdditionTag))
                {
                    entity.Get<ChocolateAddtion>();
                    entity.Get<ChangePopAdditionRequest>().Addition = StaticData.PopAdditions.Chocolate;
                }

                if (!entity.Has<PrepareToDespawnRequest>() && entityCollision.Collider.gameObject.CompareTag(_gameData.StaticData.GroundTag))
                {
                    entity.Get<CleanIt>();
                }

                if (!entity.Has<ReadyToSell>() && !entity.Has<CleanIt>() && entityCollision.Collider.gameObject.CompareTag(_gameData.StaticData.SellZoneTag))
                {
                    entity.Get<GetMoneyForPopInSellZone>().PopEntity = entity;

                    entity.Get<StopAllMovingRequest>();

                    entity.Get<Timer<TimerToSellState>>().Value = 0.01f;
                    entity.Get<ShakeBagRequest>().ProductLineId = entity.Get<Pop>().ProductLineId;
                    entity.Get<ChangePopViewRequest>().PopBody = StaticData.PopBody.PopcornWithoutLimbs;
                    entity.Get<ChangePopEmotionRequest>().Emotion = StaticData.PopEmotions.None;
                }
            }

            foreach (var idx in _exitfilter)
            {
                ref EcsEntity entity = ref _exitfilter.GetEntity(idx);
                ref GameObjectProvider entityGo = ref entity.Get<GameObjectProvider>();
                ref OnTriggerExitEvent entityCollision = ref entity.Get<OnTriggerExitEvent>();

                if (entity.Has<Cooking>() && entityCollision.Collider.gameObject.CompareTag(_gameData.StaticData.CookingZoneTag))
                    entity.Del<Cooking>();
            }
        }
    }
}

