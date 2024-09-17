using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class VelocityMovingSystem : IEcsRunSystem
    {
        private EcsFilter<VelocityMoving> _movingFilter;

        public void Run()
        {
            if (_movingFilter.IsEmpty())
                return;

            foreach (var movingObject in _movingFilter)
            {
                ref EcsEntity movingEntity = ref _movingFilter.GetEntity(movingObject);
                ref GameObjectProvider movingEntityGo = ref movingEntity.Get<GameObjectProvider>();
                ref RigidbodyProvider movingEntityRb = ref movingEntity.Get<RigidbodyProvider>();

                ref VelocityMoving moving = ref movingEntity.Get<VelocityMoving>();

                moving.Speed = moving.Speed == 0 ? 2 : moving.Speed;
                moving.Accuracy = moving.Accuracy == 0 ? 0.1f : moving.Accuracy;

                movingEntityRb.Value.velocity = (moving.Target - movingEntityGo.Value.transform.position).normalized * moving.Speed;

                if (Vector3.Distance(movingEntityGo.Value.transform.position, moving.Target) < moving.Accuracy)
                {
                    movingEntityRb.Value.velocity = Vector3.zero;
                    movingEntity.Del<VelocityMoving>();
                    //moving.CompleteAction?.Invoke();
                    movingEntity.Get<MovingCompleteEvent>();
                }
            }
        }
    }
}