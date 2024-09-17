using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class TutorialSystem : IEcsInitSystem, IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<StartTutorialRequest> _startFilter;
        private EcsFilter<CompleteTutorialEvent> _completeFilter;

        private EcsFilter<TutorialProvider> _tutorialFilter;
        private EcsFilter<GameObjectProvider, GoldPop, GoToJump> _goldFilter;

        public void Init()
        {
            CheckTutorialStates();
        }

        public void Run()
        {
            foreach (var idx in _startFilter)
            {
                ref EcsEntity entity = ref _startFilter.GetEntity(idx);
                ref StartTutorialRequest startRequest = ref entity.Get<StartTutorialRequest>();

                if (startRequest.Tutorial == StaticData.Tutorials.Heating)
                    _gameUi.SetShowStateHeatingTutorialScreen(true);

                if (startRequest.Tutorial == StaticData.Tutorials.Upgrade)
                    _gameUi.SetShowStateUpgradeTutorialScreen(true);

                if (startRequest.Tutorial == StaticData.Tutorials.SpeedUp)
                    foreach (var item in _tutorialFilter)
                        _tutorialFilter.Get1(item).TutorialHand.SetActive(true);

                if (startRequest.Tutorial == StaticData.Tutorials.GoldTap)
                {
                    GameObject turtHand = null;
                    Transform goldTr = null;

                    foreach (var item in _tutorialFilter)
                       turtHand = _tutorialFilter.Get1(item).TutorialHand;

                    foreach (var item in _goldFilter)
                        goldTr = _goldFilter.Get1(item).Value.transform;

                    turtHand.SetActive(true);

                    turtHand.transform.SetParent(goldTr.GetChild(0));
                    turtHand.transform.localPosition = Vector3.zero;
                    turtHand.transform.localRotation = Quaternion.Euler(Vector3.zero);
                }

                entity.Del<StartTutorialRequest>();
            }

            foreach (var idx in _completeFilter)
            {
                ref EcsEntity entity = ref _completeFilter.GetEntity(idx);
                ref CompleteTutorialEvent completeEvent = ref entity.Get<CompleteTutorialEvent>();

                if (completeEvent.Tutorial == StaticData.Tutorials.SpeedUp)
                    foreach (var item in _tutorialFilter)
                        _tutorialFilter.Get1(item).TutorialHand.SetActive(false);

                if (completeEvent.Tutorial == StaticData.Tutorials.GoldTap)
                    foreach (var item in _tutorialFilter)
                        _tutorialFilter.Get1(item).TutorialHand.SetActive(false);

                _gameData.PlayerData.TutrorialStates[(int)completeEvent.Tutorial] = true;
                entity.Del<CompleteTutorialEvent>();
                CheckTutorialStates();
            }
        }

        private void CheckTutorialStates()
        {
            //for (int i = 0; i < _gameData.PlayerData.TutrorialStates.Count - 2; i++) // without gold tap because needed gold pop at scene
                if (!_gameData.PlayerData.TutrorialStates[0])
                {
                    _world.NewEntity().Get<StartTutorialRequest>().Tutorial = StaticData.Tutorials.Heating;
                }
        }
    }
}