using Leopotam.Ecs;

namespace Client
{
    public class PopEarningSystem : IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<GetMoneyForPopInSellZone>.Exclude<ReadyToSell> _popFilter;

        public void Run()
        {
            foreach (var idx in _popFilter)
            {
                ref EcsEntity entity = ref _popFilter.GetEntity(idx);
                ref GameObjectProvider entityGo = ref entity.Get<GameObjectProvider>();

                ref EcsEntity popEntity = ref entity.Get<GetMoneyForPopInSellZone>().PopEntity;
                ref Pop pop = ref popEntity.Get<Pop>();

                double reward = _gameData.RuntimeData.GetPopEarning();

                if (popEntity.Has<ChocolateAddtion>())
                    reward *= _gameData.RuntimeData.GetChocoMultiplier();

                _world.NewEntity().Get<EarnMoneyEvent>().Value = reward;

                EcsEntity earnViewEntity = _world.NewEntity();
                earnViewEntity.Get<EarnView>() = new EarnView
                {
                    Value = reward,
                    Position = entityGo.Value.transform.position
                };
                earnViewEntity.Get<CreateEarnViewRequest>();

                popEntity.Get<ReadyToSell>();
                popEntity.Get<AddReadyToSellPopEvent>().ProductLineId = pop.ProductLineId;
                entity.Del<GetMoneyForPopInSellZone>();

                _world.NewEntity().Get<PlaySoundRequest>().SoundName = StaticData.AudioSound.CashSound;
            }
        }
    }
}