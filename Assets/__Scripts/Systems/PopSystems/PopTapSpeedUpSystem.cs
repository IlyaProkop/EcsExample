using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class PopTapSpeedUpSystem : IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<RaycastEvent> _tapFilter;
        private EcsFilter<Pop, GoToJump, TransformMoving>.Exclude<IsSpeedUpMarker, Timer<TimerSpeedUp>> _filter;
        private EcsFilter<Pop, IsSpeedUpMarker, TimerDoneEvent<TimerSpeedUp>> _timerDoneFilter;

        public void Run()
        {
            foreach (var tap in _tapFilter)
            {
                ref EcsEntity tapEntity = ref _tapFilter.GetEntity(tap);
                ref RaycastEvent raycast = ref tapEntity.Get<RaycastEvent>();

                if (raycast.GameObject.CompareTag(_gameData.StaticData.ConveyorTag))
                {
                    _gameData.RuntimeData.IsTapSpeedUpWorking = true;
                    foreach (var idx in _filter)
                    {
                        ref EcsEntity entity = ref _filter.GetEntity(idx);
                        ref Pop pop = ref entity.Get<Pop>();
                        ref TransformMoving moving = ref entity.Get<TransformMoving>();
                        ref PopcornViewProvider popView = ref entity.Get<PopcornViewProvider>();

                        popView.SpeedUpTrail.SetActive(true);
                        Vector3 position = moving.Target;
                        TransformMoving transformMoving = new TransformMoving
                        {
                            Target = position,
                            Accuracy = 0.1f,
                            Speed = _gameData.RuntimeData.GetPopSpeed()
                        };
                        entity.Replace(transformMoving);

                        entity.Get<IsSpeedUpMarker>();
                        entity.Get<Timer<TimerSpeedUp>>().Value = _gameData.BalanceData.SpeedUpTime;
                        entity.Get<ChangePopAnimationRequest>().Animation = StaticData.PopAnimations.IsRunning;

                        if (!_gameData.PlayerData.TutrorialStates[(int)StaticData.Tutorials.SpeedUp])
                            _world.NewEntity().Get<CompleteTutorialEvent>().Tutorial = StaticData.Tutorials.SpeedUp;
                    }
                }
            }

            foreach (var idx in _timerDoneFilter)
            {
                ref EcsEntity entity = ref _timerDoneFilter.GetEntity(idx);
                ref TransformMoving moving = ref entity.Get<TransformMoving>();
                ref PopcornViewProvider popView = ref entity.Get<PopcornViewProvider>();

                popView.SpeedUpTrail.SetActive(false);
                _gameData.RuntimeData.IsTapSpeedUpWorking = false;

                Vector3 position = moving.Target;
                TransformMoving transformMoving = new TransformMoving
                {
                    Target = position,
                    Accuracy = 0.1f,
                    Speed = _gameData.RuntimeData.GetPopSpeed()
                };
                entity.Replace(transformMoving);

                entity.Get<ChangePopAnimationRequest>().Animation = StaticData.PopAnimations.IsWalking;
                entity.Del<TimerDoneEvent<TimerSpeedUp>>();
                entity.Del<IsSpeedUpMarker>();
            }
        }
    }
}