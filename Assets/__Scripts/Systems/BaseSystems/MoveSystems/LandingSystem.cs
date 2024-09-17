using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class LandingSystem : IEcsRunSystem
    {
        private GameData _gameData;

        private EcsFilter<OnCollisionEnterEvent, Landing> _landingFilter;
        private EcsFilter<OnTriggerEnterEvent, Landing> _landingTriggerFilter;

        public void Run()
        {
            foreach (var landing in _landingFilter)
            {
                ref EcsEntity landingEntity = ref _landingFilter.GetEntity(landing);
                ref ParticleSystemProvider landingPs = ref landingEntity.Get<ParticleSystemProvider>();
                ref OnCollisionEnterEvent landingCollision = ref landingEntity.Get<OnCollisionEnterEvent>();

                if (landingCollision.Collision != null)
                    if (landingCollision.Collision.gameObject.CompareTag(_gameData.StaticData.GroundTag) || landingCollision.Collision.gameObject.CompareTag(_gameData.StaticData.ConveyorTag))
                    {
                        //foreach (var ps in landingPs.ParticleSystems)
                        //    if (ps.ParticleSystemName == "LandingPS")
                        //        ps.ParticleSystem.Play();
                        _gameData.SceneData.CameraController.Shake();
                        landingEntity.Del<Landing>();
                    }
            }

            foreach (var landing in _landingTriggerFilter)
            {
                ref EcsEntity landingEntity = ref _landingTriggerFilter.GetEntity(landing);
                ref ParticleSystemProvider landingPs = ref landingEntity.Get<ParticleSystemProvider>();
                ref OnTriggerEnterEvent landingCollision = ref landingEntity.Get<OnTriggerEnterEvent>();

                if (landingCollision.Collider)
                {
                    if (landingCollision.Collider.CompareTag(_gameData.StaticData.GroundTag) || landingCollision.Collider.CompareTag(_gameData.StaticData.ConveyorTag))
                    {
                        //foreach (var ps in landingPs.ParticleSystems)
                        //    if (ps.ParticleSystemName == "LandingPS")
                        //        ps.ParticleSystem.Play();

                        landingEntity.Del<Landing>();
                    }
                }
            }
        }
    }
}