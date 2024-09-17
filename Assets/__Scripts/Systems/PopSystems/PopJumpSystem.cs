using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class PopJumpSystem : IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<Pop, ReadyToJump>.Exclude<Timer<TimerPrepareToJump>> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);
                ref Pop pop = ref entity.Get<Pop>();
                ref GameObjectProvider entityGo = ref entity.Get<GameObjectProvider>();

                Vector3 point = Vector3.zero;
                float distance = 100.0f;
                for (int i = 0; i < pop.Conveyor.JumpPoints.Count; i++)
                    if (Vector3.Distance(entityGo.Value.transform.position, pop.Conveyor.JumpPoints[i].position) < distance)
                        point = pop.Conveyor.JumpPoints[i].position;

                entity.Get<AddingForce>() = new AddingForce
                {
                    Direction = (point - entityGo.Value.transform.position).normalized * Random.Range(_gameData.BalanceData.JumpPopcornForce.x, _gameData.BalanceData.JumpPopcornForce.y),
                    ForceMode = ForceMode.Impulse
                };
                entity.Get<LookingAt>().Target = point;
                entity.Get<InJump>();
                entity.Get<ChangePopEmotionRequest>().Emotion = StaticData.PopEmotions.Happy;
                entity.Del<ReadyToJump>();

                _world.NewEntity().Get<PlaySoundRequest>().SoundName = StaticData.AudioSound.JumpSound;
            }
        }
    }
}