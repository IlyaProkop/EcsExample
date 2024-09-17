using Leopotam.Ecs;

namespace Client
{
    public class MoneySystem : IEcsInitSystem, IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<EarnMoneyEvent> _earnFilter;
        private EcsFilter<SpendMoneyEvent> _spendfilter;

        public void Init()
        {
            _gameData.PlayerData.Money = _gameData.BalanceData.StartMoney;
            _gameUi.GameScreen.UpdateMoneyText(_gameData.PlayerData.Money);
        }

        public void Run()
        {
            foreach (var idx in _earnFilter)
            {
                ref EcsEntity entity = ref _earnFilter.GetEntity(idx);
                _gameData.PlayerData.Money += _earnFilter.Get1(idx).Value;
                _gameUi.GameScreen.UpdateMoneyText(_gameData.PlayerData.Money);
                entity.Del<EarnMoneyEvent>();
            }

            foreach (var idx in _spendfilter)
            {
                ref EcsEntity entity = ref _spendfilter.GetEntity(idx);
                _gameData.PlayerData.Money -= _spendfilter.Get1(idx).Value;
                if (_gameData.PlayerData.Money < 0)
                    _gameData.PlayerData.Money = 0;
                _gameUi.GameScreen.UpdateMoneyText(_gameData.PlayerData.Money);
                entity.Del<SpendMoneyEvent>();
            }
        }
    }
}