using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] public LevelCompleteScreen LevelCompleteScreen;
    [SerializeField] public OfflineBonusScreen OfflineBonusScreen;
    [SerializeField] public GameScreen GameScreen;
    [SerializeField] public UpgradeScreen UpgradeScreen;
    [SerializeField] public EpicUpgradeScreen LabUpgradeScreen;
    [SerializeField] public SettingScreen SettingScreen;
    [SerializeField] public CheatScreen CheatScreen;

    [SerializeField] public HeatingTutorialScreen HeatingTutorialScreen;
    [SerializeField] public UpgradeTutorialScreen UpgradeTutorialScreen;

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
        showAllScreenState = true;
    }
    
    public void TriggerShowStateAllScreen()
    {
        showAllScreenState = !showAllScreenState;
        foreach (var screen in screens)
            screen.gameObject.SetActive(showAllScreenState);
    }

    public void SetShowStateLevelCompleteScreen(bool isShow) => LevelCompleteScreen.SetShowState(isShow);
    public void SetShowStateGameScreen(bool isShow) => GameScreen.SetShowState(isShow);
    public void SetShowStateUpgradeScreen(bool isShow) => UpgradeScreen.SetShowState(isShow);
    public void SetShowStateLabUpgradeScreen(bool isShow) => LabUpgradeScreen.SetShowState(isShow);
    public void SetShowStateSettingScreen(bool isShow) => SettingScreen.SetShowState(isShow);
    public void SetShowStateCheatScreen(bool isShow) => CheatScreen.SetShowState(isShow);

    public void SetShowStateHeatingTutorialScreen(bool isShow) => HeatingTutorialScreen.SetShowState(isShow);
    public void SetShowStateUpgradeTutorialScreen(bool isShow) => UpgradeTutorialScreen.SetShowState(isShow);

    public void SetShowStateOfflineBonusScreen(bool isShow, double reward)
    {
        OfflineBonusScreen.SetShowState(isShow);
        OfflineBonusScreen.UpdateRewardText(reward);
    }
}
