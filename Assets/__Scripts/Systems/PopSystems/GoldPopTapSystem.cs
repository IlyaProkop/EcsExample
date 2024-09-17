using DG.Tweening;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class GoldPopTapSystem : IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<RaycastEvent> _tapFilter;
        private EcsFilter<GoldPop>.Exclude<IsThisGoldTakenMarker, Timer<TimerGoldTaken>> _filter;
        private EcsFilter<GoldPop, TimerDoneEvent<TimerGoldTaken>> _timerDoneFilter;

        public void Run()
        {
            foreach (var tap in _tapFilter)
            {
                ref EcsEntity tapEntity = ref _tapFilter.GetEntity(tap);
                ref RaycastEvent raycast = ref tapEntity.Get<RaycastEvent>();

                if (raycast.GameObject.CompareTag(_gameData.StaticData.GoldPopcornTag))
                {
                    foreach (var idx in _filter)
                    {
                        ref EcsEntity entity = ref _filter.GetEntity(idx);
                        ref Pop pop = ref entity.Get<Pop>();
                        ref PopcornViewProvider popView = ref entity.Get<PopcornViewProvider>();
                        ref GameObjectProvider popGo = ref entity.Get<GameObjectProvider>();

                        if (popGo.Value == raycast.GameObject)
                        {
                            popView.SpeedUpTrail.SetActive(true);

                            popGo.Value.transform.DOMove(_gameData.SceneData.CameraController.GetComponent<Camera>().ScreenToWorldPoint(_gameUi.GameScreen.GetGoldPopPosition()), _gameData.BalanceData.BaseGetGoldPopTime).SetEase(Ease.InCirc);
                            popGo.Value.transform.DOScale(Vector3.zero, _gameData.BalanceData.BaseGetGoldPopTime * 3f);
                            entity.Get<IsThisGoldTakenMarker>();
                            entity.Get<Timer<TimerGoldTaken>>().Value = _gameData.BalanceData.BaseGetGoldPopTime;
                            entity.Get<ChangePopAnimationRequest>().Animation = StaticData.PopAnimations.IsGoldTaken;
                            entity.Get<ChangePopEmotionRequest>().Emotion = StaticData.PopEmotions.Happy;

                            if (!_gameData.PlayerData.TutrorialStates[(int)StaticData.Tutorials.GoldTap])
                                _world.NewEntity().Get<CompleteTutorialEvent>().Tutorial = StaticData.Tutorials.GoldTap;
                        }
                    }
                }
            }

            foreach (var idx in _timerDoneFilter)
            {
                ref EcsEntity entity = ref _timerDoneFilter.GetEntity(idx);
                ref TransformMoving moving = ref entity.Get<TransformMoving>();
                ref PopcornViewProvider popView = ref entity.Get<PopcornViewProvider>();

                popView.SpeedUpTrail.SetActive(false);

                entity.Get<StopAllMovingRequest>();
                entity.Get<PrepareToDespawnRequest>();
                entity.Del<TimerDoneEvent<TimerGoldTaken>>();
                entity.Del<IsThisGoldTakenMarker>();

                _world.NewEntity().Get<AddGoldPopEvent>();
            }
        }
    }
}