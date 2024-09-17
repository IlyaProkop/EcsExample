using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeScreen : BaseScreen
{
    [SerializeField] private List<UpgradeButtonView> upgradeButtons;
    [SerializeField] private List<UpgradeData> upgradeDatas;

    protected override void ManualStart()
    {
        for (int i = 0; i < upgradeButtons.Count; i++)
            upgradeButtons[i].InitData(upgradeDatas[i], EcsWorld, GameData);
    }

    private void Update()
    {
        if (IsScreenShow)
            for (int i = 0; i < upgradeButtons.Count; i++)
                upgradeButtons[i].UpdateInfo(upgradeDatas[i]);
    }
}