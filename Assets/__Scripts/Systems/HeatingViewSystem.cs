using Leopotam.Ecs;

namespace Client
{
    public class HeatingViewSystem : IEcsInitSystem, IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<HeatingViewUpdateTimerContainer>.Exclude<Timer<TimerHeatingViewUpdate>> _filter;
        private EcsFilter<ConveyorProvider> _conveyorFilter;

        public void Init()
        {
            EcsEntity entity = _world.NewEntity();
            entity.Get<HeatingViewUpdateTimerContainer>();
        }

        public void Run()
        {
            foreach (var idx in _filter)
            {
                float temperature = _gameData.RuntimeData.Temperature / _gameData.RuntimeData.GetMaxTemperature();
                float temperatureWithMax = _gameData.RuntimeData.Temperature / _gameData.BalanceData.MaxTemperature;
                //_gameUi.GameScreen.UpdateTemperatureProgressBar(temperature);
                //_gameUi.GameScreen.UpdateTemperatureText(_gameData.RuntimeData.Temperature);

                foreach (var conveyor in _conveyorFilter)
                {
                    _conveyorFilter.Get1(conveyor).StoveMaterial.UpdateEmmision(temperatureWithMax, temperatureWithMax);
                    if(temperature > 50.0f)
                    {
                        if (!_conveyorFilter.Get1(conveyor).AirHeatDistortion.activeInHierarchy)
                            _conveyorFilter.Get1(conveyor).AirHeatDistortion.SetActive(true);
                    }
                    else
                    {
                        if (_conveyorFilter.Get1(conveyor).AirHeatDistortion.activeInHierarchy)
                            _conveyorFilter.Get1(conveyor).AirHeatDistortion.SetActive(false);
                    }

                    if (temperature > 25.0f)
                    {
                        if (!_conveyorFilter.Get1(conveyor).HeatSteam.activeInHierarchy)
                            _conveyorFilter.Get1(conveyor).HeatSteam.SetActive(true);
                    }
                    else
                    {
                        if (_conveyorFilter.Get1(conveyor).HeatSteam.activeInHierarchy)
                            _conveyorFilter.Get1(conveyor).HeatSteam.SetActive(false);
                    }
                }

                _filter.GetEntity(idx).Get<Timer<TimerHeatingViewUpdate>>().Value = 0.02f;
            }
        }
    }
}