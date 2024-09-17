using Leopotam.Ecs;
using System.Collections;
using Client.Analytics.AnalyticManager;
using UnityEngine;

namespace Client
{
    sealed class Game : MonoBehaviour
    {
        private EcsWorld _ecsWorld;
        private EcsSystems _updateSystems;
        private EcsSystems _fixedUpdateSystems;

        [Header("Data")]
        [SerializeField] private GameData _gameData;

        [Header("UI")]
        [SerializeField] private GameUI _gameUi;
        [SerializeField] private WorldGameUI _worldGameUi;

        [Header("Audio")]
        [SerializeField] private AudioManager _audioManager;
        [SerializeField] private AnalyticService _analyticService;

        private IEnumerator Start()
        {
            _ecsWorld = new EcsWorld();
            _updateSystems = new EcsSystems(_ecsWorld, " - UPDATE");
            _fixedUpdateSystems = new EcsSystems(_ecsWorld, " - FIXED UPDATE");

#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_ecsWorld);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_updateSystems);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_fixedUpdateSystems);
#endif

            SetTargetFrameRate();
            _gameUi.InjectEcsWorld(_ecsWorld, _gameData);
            _worldGameUi.InjectEcsWorld(_ecsWorld, _gameData);
            ProvideMonoEntitiesFromScene();
            _analyticService = new AnalyticService();
            
            EcsSystems inputSystems = InputSystems();
            EcsSystems spawnSystems = SpawnSystems();
            EcsSystems moveSystems = MoveSystems();
            EcsSystems popSystems = PopSystems();
            EcsSystems timerSystems = TimerSystems();

            //.Add(characterSystems)
            _updateSystems
                .Add(new InitGameSystem())
                .Add(new ShortcutCheatSystem())
                //.Add(new DebugSystem())
                .Add(timerSystems)
                .Add(new RaycastSystem())

                .Add(new ConveyorSystem())
                .Add(new ConveyorBeltViewSystem())
                .Add(new HeatingSystem())
                .Add(new HeatingViewSystem())
                .Add(new PopTapSystem())
                .Add(spawnSystems)
                .Add(inputSystems)
                .Add(popSystems)
                .Add(new HandSystem())
                .Add(new HandAnimationSystem())
                
                .Add(new GoldPopTapSystem())

                .Add(new EarningViewSystem())
                .Add(new LevelProgressSystem())
                .Add(new VibrationSystem())
                .Add(new CheckUpgradePossiblitySystem())
                .Add(new MoneySystem())
                .Add(new EarningMoneyInSecSystem())
                .Add(new UpgradeSystem())
                .Add(new CameraSystem())
                .Add(new AudioSystem())
                .Add(new SettingScreenSystem())
                .Add(new CheatScreenSystem())
                .Add(new UpgradeScreenSystem())
                .Add(new OfflineBonusSystem())
                .Add(new TutorialSystem())

                .OneFrame<MovingCompleteEvent>()
                .OneFrame<ChangeGameStateRequest>()
                .OneFrame<PopCookingDoneEvent>()
                .OneFrame<TapPopButtonEvent>()

                .Inject(_gameData)
                .Inject(_gameUi)
                .Inject(_worldGameUi)
                .Inject(_audioManager)
                .Inject(_analyticService)

                .Init();

            _fixedUpdateSystems
                .Add(new PopTriggerSystem())
                .Add(moveSystems)

                .OneFrame<OnCollisionEnterEvent>()
                .OneFrame<OnTriggerEnterEvent>()
                .OneFrame<OnTriggerExitEvent>()
                .OneFrame<RaycastEvent>()

                .Inject(_gameData)

                .Init();

            yield return null;
        }

        void Update()
        {
            _updateSystems?.Run();
        }

        void FixedUpdate()
        {
            _fixedUpdateSystems?.Run();
        }

        void OnDestroy()
        {
            if (_updateSystems != null)
            {
                _updateSystems.Destroy();
                _updateSystems = null;

                _fixedUpdateSystems.Destroy();
                _fixedUpdateSystems = null;

                _ecsWorld.Destroy();
                _ecsWorld = null;
            }
        }

        //------------------SYSTEM GROUPS---------------

        private EcsSystems SpawnSystems()
        {
            return new EcsSystems(_ecsWorld, "SpawnSystems")
            .Add(new DestroyGameObjectSystem())

            .Add(new PopSpawnSystem())
            .Add(new ObjectPoolSystem())
            .Add(new DespawnSystem())

            .Add(new SpawnSystem());
        }


        private EcsSystems InputSystems()
        {
            return new EcsSystems(_ecsWorld, "InputSystems")
            .Add(new InputJoystickSystem());
        }

        private EcsSystems MoveSystems()
        {
            return new EcsSystems(_ecsWorld, "MovableSystems")
            .Add(new VelocityMovingSystem())
            .Add(new TransformMovingSystem())
            .Add(new LookingAtSystem())
            .Add(new PhysicForceAddSystem())
            .Add(new LandingSystem());
        }

        private EcsSystems TimerSystems()
        {
            return new EcsSystems(_ecsWorld, "TimerSystems")
            .Add(new TimerSystem<TimerBeforeGoToJump>())
            .Add(new TimerSystem<TimerEarningView>())
            .Add(new TimerSystem<TimerForPopClean>())
            .Add(new TimerSystem<TimerIntervalSpawnPop>())
            .Add(new TimerSystem<TimerPrepareToJump>())
            .Add(new TimerSystem<TimerSpeedUp>())
            .Add(new TimerSystem<TimerToSellState>())
            .Add(new TimerSystem<TimerUpdateMoneyInSec>())
            .Add(new TimerSystem<TimerHeatingViewUpdate>())
            .Add(new TimerSystem<TimerGoldTaken>())
            .Add(new TimerSystem<TimerShakeInterval>());
        }

        private EcsSystems PopSystems()
        {
            return new EcsSystems(_ecsWorld, "PopSystems")

                .Add(new PopInitSystem())
                .Add(new PopLaunchSystem())
                .Add(new PopStopAllMovingSystem())
                .Add(new PopPreapreToDespawmSystem())
                .Add(new PopCookingSystem())
                //.Add(new PopCookingShakeSystem())
                .Add(new PopPopingSystem())
                .Add(new PopTapSpeedUpSystem())
                .Add(new PopGoToJumpSystem())
                .Add(new PopPrepareToJumpSystem())
                .Add(new PopJumpSystem())
                .Add(new PopPrepareToSellSystem())
                .Add(new PopSellSystem())
                .Add(new PopCleanSystem())

                .Add(new PopAdditionSystem())
                .Add(new PopEmotionSystem())
                .Add(new PopViewSystem())
                .Add(new PopAnimationSystem())

                .Add(new PopCounterSystem())
                .Add(new PopEarningSystem());
                
        }

        private EcsSystems CoreGameplaySystems()
        {
            return new EcsSystems(_ecsWorld);
            //.OneFrame<OnObstacleCollisionEvent>()
            //.Add(new ObstacleCollisionCheckerSystem())
            //.OneFrame<DeadEvent>()
            //.Add(new DeadByObstacleCollisionSystem())
            //.Add(new DeadCheckerGameplaySystem());
        }

        private static void SetTargetFrameRate() => Application.targetFrameRate = 60;

        private void ProvideMonoEntitiesFromScene()
        {
            foreach (var monoEntity in _gameData.SceneData.MonoEntities)
            {
                EcsEntity ecsEntity = _ecsWorld.NewEntity();
                monoEntity.Provide(ref ecsEntity);
            }

            foreach (var monoEntity in _gameData.SceneData.Conveyors)
            {
                EcsEntity ecsEntity = _ecsWorld.NewEntity();
                monoEntity.Provide(ref ecsEntity);
            }
        }
    }
}
