using Client;
using DG.Tweening;
using Leopotam.Ecs;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTutorialScreen : BaseScreen
{
    [SerializeField] private ActionButton completeTutorialButton;

    protected override void ManualStart()
    {
        completeTutorialButton.OnClickEvent.AddListener(() =>
        {
            EcsWorld.NewEntity().Get<CompleteTutorialEvent>().Tutorial = StaticData.Tutorials.Upgrade;
            EcsWorld.NewEntity().Get<ShowLabUpgradeScreenRequest>();
            SetShowState(false);
        });
    }
}
