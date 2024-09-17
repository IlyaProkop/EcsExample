using Client;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;

public class CheatScreen : BaseScreen
{
    [SerializeField] private ActionButton getMoneyButton;
    [SerializeField] private ActionButton getGoldButton;
    [SerializeField] private ActionButton goToMidGameButton;
    [SerializeField] private ActionButton goToLateGameButton;
    [SerializeField] private ActionButton resetPlayerDataButton;
    [SerializeField] private ActionButton hideScreenButton;

    protected override void ManualStart()
    {
        getMoneyButton.OnClickEvent.AddListener(() => CheatGetMoney());
        getGoldButton.OnClickEvent.AddListener(() => CheatGetGold());
        goToMidGameButton.OnClickEvent.AddListener(() => CheatGoToMidGame());
        goToLateGameButton.OnClickEvent.AddListener(() => CheatGoToLateGame());
        resetPlayerDataButton.OnClickEvent.AddListener(() => CheatResetPlayerData());
        hideScreenButton.OnClickEvent.AddListener(() => SetShowState(false));
    }

    private void CheatGetMoney()
    {
        EcsWorld.NewEntity().Get<EarnMoneyEvent>().Value = 100000;
    }

    private void CheatGetGold()
    {
        for (int i = 0; i < 100; i++)
            EcsWorld.NewEntity().Get<AddGoldPopEvent>();
    }

    private void CheatGoToMidGame()
    {

        for (int i = 1; i < 2; i++)
            EcsWorld.NewEntity().Get<BuyConveyorRequest>().ConveyorId = i;
    }

    private void CheatGoToLateGame()
    {

        for (int i = 1; i < 4; i++)
            EcsWorld.NewEntity().Get<BuyConveyorRequest>().ConveyorId = i;
    }

    private void CheatResetPlayerData()
    {
        GameData.Instance.PlayerData.ResetData();
    }
}