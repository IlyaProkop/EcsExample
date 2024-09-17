using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class LookingAtSystem : IEcsRunSystem
    {
        private EcsFilter<LookingAt> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);
                ref GameObjectProvider entityGo = ref entity.Get<GameObjectProvider>();
                ref RigidbodyProvider entityRb = ref entity.Get<RigidbodyProvider>();

                ref LookingAt lookingAt = ref entity.Get<LookingAt>();

                lookingAt.Speed = lookingAt.Speed == 0 ? 1 : lookingAt.Speed;
                lookingAt.Accuracy = lookingAt.Accuracy == 0 ? 0.05f : lookingAt.Accuracy;

                var targetRotation = Quaternion.LookRotation((lookingAt.Target - entityGo.Value.transform.position)).normalized;

                entityGo.Value.transform.rotation = Quaternion.Slerp(entityGo.Value.transform.rotation, targetRotation, lookingAt.Speed);
                
                if (ApproximatelyQuaternions(entityGo.Value.transform.rotation, targetRotation, lookingAt.Accuracy))
                    entity.Del<LookingAt>();
            }
        }

        private bool ApproximatelyQuaternions(Quaternion quatA, Quaternion value, float acceptableRange)
        {
            return 1 - Mathf.Abs(Quaternion.Dot(quatA, value)) < acceptableRange;
        }
    }
}