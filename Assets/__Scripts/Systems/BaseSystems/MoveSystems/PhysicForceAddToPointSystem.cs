using Leopotam.Ecs;

namespace Client
{
    public class PhysicForceAddToPointSystem : IEcsRunSystem
    {
        private EcsFilter<AddingForceAtPoint, RigidbodyProvider> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);
                ref GameObjectProvider entityGo = ref entity.Get<GameObjectProvider>();
                ref RigidbodyProvider entityRb = ref entity.Get<RigidbodyProvider>();
                ref AddingForceAtPoint force = ref entity.Get<AddingForceAtPoint>();

                entityRb.Value.AddForceAtPosition(force.Direction, force.Point, force.ForceMode);

                entity.Get<Landing>();
                entity.Del<AddingForce>();
            }
        }
    }
}
