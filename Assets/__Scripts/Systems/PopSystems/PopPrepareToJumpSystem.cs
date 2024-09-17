using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class PopPrepareToJumpSystem : IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<Pop, GoToJump, MovingCompleteEvent> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);
                ref Pop pop = ref entity.Get<Pop>();
                ref GameObjectProvider entityGo = ref entity.Get<GameObjectProvider>();

                Transform point = pop.Conveyor.JumpPoints[Random.Range(0, pop.Conveyor.JumpPoints.Count)];
                entity.Get<LookingAt>().Target = new Vector3(point.position.x, entityGo.Value.transform.position.y, point.position.z);

                entity.Get<ReadyToJump>();
                entity.Get<ChangePopAnimationRequest>().Animation = StaticData.PopAnimations.IsPrepareToJump;
                entity.Get<Timer<TimerPrepareToJump>>().Value = 0.1f;
                entity.Del<GoToJump>();
            }
        }
    }
}