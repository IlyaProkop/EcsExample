using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class PopCookingShakeSystem : IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<Pop, Cooking>.Exclude<Done, Timer<TimerShakeInterval>> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);
                ref GameObjectProvider entityGo = ref entity.Get<GameObjectProvider>();

                entity.Get<AddingForce>() = new AddingForce
                {
                    Direction = Vector3.up * Random.Range(_gameData.BalanceData.CookingShakePopcornForce.x, _gameData.BalanceData.CookingShakePopcornForce.y),
                    ForceMode = ForceMode.Impulse
                };

                entity.Get<Timer<TimerShakeInterval>>().Value = 0.2f;
            }
        }
    }
}