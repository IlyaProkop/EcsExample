using Client;
using DG.Tweening;
using Leopotam.Ecs;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameScreen : BaseScreen
{
    [Header("Top Panel")]
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI moneyInSecText;

    [SerializeField] private TextMeshProUGUI popcornAmountText;
    [SerializeField] private TextMeshProUGUI goldPopcornAmountText;

    [SerializeField] private TextMeshProUGUI temperatureText;
    [SerializeField] private Image temperatureProgressBarFillImage;

    [SerializeField] private Image levelProgressBarFill;

    [SerializeField] private Image goldPopImage;
    [SerializeField] private Transform goldPopPanel;

    [Header("Pop Button")]
    [SerializeField] private ActionButton popButton;

    [Header("Show Screens Button")]
    [SerializeField] private ActionButton showUpgradeScreenButton;
    [SerializeField] private ActionButton showSettingScreenButton;
    [SerializeField] private ActionButton showCheatScreenButton;

    [SerializeField] private GameObject CanBuyUpgrade;

    protected override void ManualStart()
    {
        popButton.OnClickEvent.AddListener( () =>
        {
            EcsWorld.NewEntity().Get<TapPopButtonEvent>();
        });

        showUpgradeScreenButton.OnClickEvent.AddListener(() => EcsWorld.NewEntity().Get<ShowLabUpgradeScreenRequest>());
        showSettingScreenButton.OnClickEvent.AddListener(() => EcsWorld.NewEntity().Get<ShowSettingScreenRequest>());
        showCheatScreenButton.OnClickEvent.AddListener(() => EcsWorld.NewEntity().Get<ShowCheatScreenRequest>());
    }

    public void UpdateLevelText(int _level) => levelText.text = $"WAVE {_level}";
    public void UpdateMoneyText(double _moneyCount) => moneyText.text = $"<sprite=0> {Utility.FormatMoney(_moneyCount)}"; // money sprite
    public void UpdateMoneyInSecText(double _moneyInSecCount) => moneyInSecText.text = $"{Utility.FormatMoney(_moneyInSecCount)}/SEC";
    public void UpdateProgressBar(float _progress) => levelProgressBarFill.fillAmount = _progress;
    public void UpdatePopcornAmountText(double _popcornAmount) => popcornAmountText.text = $"{Utility.FormatMoney(_popcornAmount)}";
    public void UpdateGoldPopcornAmountText(double _goldPopcornAmount) => goldPopcornAmountText.text = $"{Utility.FormatMoney(_goldPopcornAmount)}";
    public void UpdateTemperatureText(float _currentTemperature) => temperatureText.text = $"{179 + _currentTemperature:0}";
    public void UpdateTemperatureProgressBar(float _temperature) => temperatureProgressBarFillImage.fillAmount = _temperature;
    public Vector3 GetGoldPopPosition() => goldPopImage.transform.position;
    public void BounceGoldPopcron() { goldPopPanel.transform.DORewind(); goldPopPanel.transform.DOPunchScale(Vector3.one * 0.11f, 0.1f, 1, 0.5f); }
    public void SetCanBuyUpgradeIndicator(bool _on) => CanBuyUpgrade.SetActive(_on);
}
