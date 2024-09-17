using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EpicUpgradeScreen : BaseScreen
{
    [SerializeField] private List<UpgradeButtonView> labUpgradeButtons;
    [SerializeField] private List<UpgradeData> labUpgradeDatas;
    [SerializeField] private ScrollRect epicScrollRect;
    [SerializeField] private ActionButton epicHideScreenButton;

    protected override void ManualStart()
    {
        for (int i = 0; i < labUpgradeButtons.Count; i++)
            labUpgradeButtons[i].InitData(labUpgradeDatas[i], EcsWorld, GameData);

        epicHideScreenButton.OnClickEvent.AddListener(() => SetShowState(false));

        OnShowScreen.AddListener(() => UpdateEpicTab());
    }

    private void UpdateEpicTab()
    {
        Utility.ScrollToTop(epicScrollRect);
        for (int i = 0; i < labUpgradeButtons.Count; i++)
            labUpgradeButtons[i].UpdateInfo(labUpgradeDatas[i]);
    }

    private void Update()
    {
        if (IsScreenShow)
            for (int i = 0; i < labUpgradeButtons.Count; i++)
                labUpgradeButtons[i].UpdateInfo(labUpgradeDatas[i]);
    }
}