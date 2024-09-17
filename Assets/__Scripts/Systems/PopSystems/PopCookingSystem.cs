using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class PopCookingSystem : IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<Pop, Cooking>.Exclude<Done> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);
                ref CookingTimer cookingTimer = ref entity.Get<CookingTimer>();
                cookingTimer.Value += Time.deltaTime * _gameData.RuntimeData.Temperature * 0.1f;

                if (cookingTimer.Value > _gameData.RuntimeData.GetPopCookingTime())
                {
                    entity.Del<Cooking>();
                    entity.Get<Done>();
                    entity.Get<PopCookingDoneEvent>();
                    entity.Get<ChangePopViewRequest>().PopBody = StaticData.PopBody.Popcorn;
                    entity.Del<CookingTimer>();
                    entity.Get<Poping>();
                }
            }
        }
    }
}