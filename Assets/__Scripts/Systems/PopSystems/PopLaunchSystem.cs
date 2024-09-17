using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class PopLaunchSystem : IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<GameObjectProvider, ReadyToLaunch> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);
                ref GameObjectProvider entityGo = ref entity.Get<GameObjectProvider>();
                ref RigidbodyProvider entityRb = ref entity.Get<RigidbodyProvider>();
                ref PopcornViewProvider entityView = ref entity.Get<PopcornViewProvider>();

                entityView.Bodies[(int)StaticData.PopBody.RawCorn].transform.Rotate(Vector3.up, Random.Range(0, 360.0f));

                Utility.ResetRigibodyVelocity(entityRb.Value);
                entity.Get<AddingForce>().Direction = entityGo.Value.transform.up * Random.Range(_gameData.BalanceData.LaunchPopcornForce.x, _gameData.BalanceData.LaunchPopcornForce.y);
                entity.Get<AddingForce>().ForceMode = ForceMode.Impulse;
                entity.Get<Landing>();

                entity.Del<ReadyToLaunch>();
                _world.NewEntity().Get<PlaySoundRequest>().SoundName = StaticData.AudioSound.ShootSound;
            }
        }
    }
}