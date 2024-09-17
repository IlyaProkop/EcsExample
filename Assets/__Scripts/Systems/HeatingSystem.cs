using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class HeatingSystem : IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        public void Run()
        {
            float currentTemperature = _gameData.RuntimeData.Temperature;
            currentTemperature += _gameData.RuntimeData.GetHeatingSpeed() * Time.deltaTime;
            currentTemperature -= _gameData.RuntimeData.GetColdingSpeed() * Time.deltaTime;
            currentTemperature = Mathf.Clamp(currentTemperature, _gameData.RuntimeData.GetMinTemperature(),
                _gameData.RuntimeData.GetMaxTemperature());
            _gameData.RuntimeData.Temperature = currentTemperature;
        }
    }
}