using Leopotam.Ecs;

namespace Client
{
    public class CheckUpgradePossiblitySystem : IEcsInitSystem, IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<SpendGoldPopEvent> _spendGoldFilter;
        private EcsFilter<AddGoldPopEvent> _addGoldFilter;
        private EcsFilter<SpendMoneyEvent> _spendMoneyFilter;
        private EcsFilter<EarnMoneyEvent> _earnMoneyFilter;

        public void Init()
        {
            CheckCanUpgrade();
        }

        public void Run()
        {
            if (_spendGoldFilter.IsEmpty() && _addGoldFilter.IsEmpty() && _spendMoneyFilter.IsEmpty() &&
                _earnMoneyFilter.IsEmpty())
                return;

            CheckCanUpgrade();
        }

        private void CheckCanUpgrade()
        {
            bool canBuyUpgrade = false;

            for (int i = 0; i < GameData.Instance.BalanceData.UpgradeData.Count; i++)
                if ((GameData.Instance.BalanceData.UpgradeData[i].UpgradeType == StaticData.UpgradeType.Chocolate &&
                     GameData.Instance.BalanceData.UpgradeData[i].CanBuyIt())
                    || (GameData.Instance.BalanceData.UpgradeData[i].UpgradeType == StaticData.UpgradeType.HeatPower &&
                        GameData.Instance.BalanceData.UpgradeData[i].CanBuyIt())
                        || (GameData.Instance.BalanceData.UpgradeData[i].UpgradeType ==
                            StaticData.UpgradeType.LuckyBoy && GameData.Instance.BalanceData.UpgradeData[i].CanBuyIt()))
            canBuyUpgrade = true;

            _gameUi.GameScreen.SetCanBuyUpgradeIndicator(canBuyUpgrade);

            if (!_gameData.PlayerData.TutrorialStates[(int)StaticData.Tutorials.Upgrade] && canBuyUpgrade)
                _world.NewEntity().Get<StartTutorialRequest>().Tutorial = StaticData.Tutorials.Upgrade;
        }
    }
}