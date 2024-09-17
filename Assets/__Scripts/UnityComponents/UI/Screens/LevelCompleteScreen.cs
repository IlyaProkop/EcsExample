using Leopotam.Ecs;
using TMPro;
using UnityEngine;

public class LevelCompleteScreen : BaseScreen
{
    [SerializeField] private ActionButton getRewardButton;
    [SerializeField] private ActionButton hideScreenButton;
    [SerializeField] private TextMeshProUGUI rewardMoneyText;

    protected override void ManualStart()
    {
    }

    private void Start()
    {
        //getRewardButton.OnClickEvent.AddListener(() =>
        //EcsWorld.NewEntity()
        //.Get<CreateNewLevelRequest>()
        //.IsRestart = false);

        //hideScreenButton.OnClickEvent.AddListener(() =>
        //EcsWorld.NewEntity()
        //.Get<CreateNewLevelRequest>()
        //.IsRestart = false);
    }
}