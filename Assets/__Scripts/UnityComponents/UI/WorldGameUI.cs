using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGameUI : MonoBehaviour
{
    [SerializeField] public List<BuyConveyorScreen> BuyConveyorScreens;

    private List<BaseScreen> screens;
    private bool showAllScreenState;
    
    public void InjectEcsWorld(EcsWorld ecsWorld, GameData gameData)
    {
        screens = new List<BaseScreen>();
        screens.AddRange(GetComponentsInChildren<BaseScreen>(true));
        foreach (var screen in screens)
        {
            screen.gameObject.SetActive(true);
            screen.InjectEcsWorld(ecsWorld);
            screen.Init(gameData);
            screen.gameObject.SetActive(false);
        }
    }
    
    public void TriggerShowStateAllScreen()
    {
        showAllScreenState = !showAllScreenState;
        foreach (var screen in screens)
            screen.gameObject.SetActive(showAllScreenState);
    }

    public void UpdateBuyConveyorScreens()
    {
        foreach (var item in BuyConveyorScreens)
            item.UpdateButtonView();
    }


}
