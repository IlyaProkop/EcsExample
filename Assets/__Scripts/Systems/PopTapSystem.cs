using Leopotam.Ecs;

namespace Client
{
    public class PopTapSystem : IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<TapPopButtonEvent> _tapFilter;
        private EcsFilter<Pop, Cooking>.Exclude<Done> _filter;

        public void Run()
        {
            foreach (var tap in _tapFilter)
            {
                foreach (var idx in _filter)
                {
                    ref EcsEntity entity = ref _filter.GetEntity(idx);

                    entity.Del<Cooking>();
                    entity.Get<Done>();
                    entity.Get<PopCookingDoneEvent>();
                    entity.Get<ChangePopViewRequest>().PopBody = StaticData.PopBody.Popcorn;
                    entity.Del<CookingTimer>();
                    entity.Get<Poping>();
                    break;
                }
            }
        }
    }
}