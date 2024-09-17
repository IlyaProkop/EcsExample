using Leopotam.Ecs;

namespace Client
{
    public class PhysicForceAddSystem : IEcsRunSystem
    {
        private EcsFilter<AddingForce, RigidbodyProvider> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);
                ref GameObjectProvider entityGo = ref entity.Get<GameObjectProvider>();
                ref RigidbodyProvider entityRb = ref entity.Get<RigidbodyProvider>();
                ref AddingForce force = ref entity.Get<AddingForce>();

                entityRb.Value.AddForce(force.Direction, force.ForceMode);

                entity.Get<Landing>();
                entity.Del<AddingForce>();
            }
        }
    }
}
