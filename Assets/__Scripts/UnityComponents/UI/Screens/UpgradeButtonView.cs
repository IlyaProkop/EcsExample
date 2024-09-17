using Client;
using Leopotam.Ecs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UpgradeButtonView : MonoBehaviour
{
    public ActionButton upgradeButton;
    public TextMeshProUGUI upgradeNameText;
    public TextMeshProUGUI upgradeDescriptionText;
    public TextMeshProUGUI upgradeCounterText;
    public TextMeshProUGUI buyPriceText;
    public TextMeshProUGUI buyText;
    public Image upgradeProgressBarFill;
    public Image upgradeImage;
    public Image currencyImage;
    public Sprite moneySprite;
    public Sprite goldPopcornSprite;

    private GameData _gameDataService;
    
    public void InitData(UpgradeData upgradeData, EcsWorld _world, GameData _gameData)
    {
        _gameDataService = _gameData;
        upgradeNameText.text = upgradeData.UpgradeName;
        int level = _gameDataService.PlayerData.UpgradeLevels[upgradeData.UpgradeType];
        if (upgradeData.IsEpicUpgrade)
            upgradeDescriptionText.text = string.Format(upgradeData.UpgradeDescription, upgradeData.GetValue());
        else
            upgradeDescriptionText.text = $"LEVEL {level + 1}";
        currencyImage.sprite = upgradeData.IsEpicUpgrade ? goldPopcornSprite : moneySprite;
        buyText.text = "BUY";
        upgradeImage.sprite = upgradeData.UpgradeSprite;

        upgradeButton.OnClickEvent.AddListener(() =>
        {
            double price = upgradeData.BasePrice * Mathf.Pow(upgradeData.PriceProgressionCoef, level);

            if (upgradeData.IsEpicUpgrade)
                _world.NewEntity().Get<SpendGoldPopEvent>().Value = price;
            else
                _world.NewEntity().Get<SpendMoneyEvent>().Value = price;

            UpdateInfo(upgradeData);

            EcsEntity entity = _world.NewEntity();
            entity.Get<UpgradeRequest>().UpgradeType = upgradeData.UpgradeType;
        });

        UpdateInfo(upgradeData);
    }

    public void UpdateInfo(UpgradeData upgradeData)
    {
        int level = _gameDataService.PlayerData.UpgradeLevels[upgradeData.UpgradeType];
        double price = upgradeData.BasePrice * Mathf.Pow(upgradeData.PriceProgressionCoef, level);
        double currency = upgradeData.IsEpicUpgrade
            ? GameData.Instance.PlayerData.GoldPopcornAmount
            : GameData.Instance.PlayerData.Money;
        upgradeCounterText.text = $"{level}/{upgradeData.MaxLevel}";
        if (upgradeData.IsEpicUpgrade)
            upgradeDescriptionText.text = string.Format(upgradeData.UpgradeDescription, upgradeData.GetValue());
        else
            upgradeDescriptionText.text = $"LEVEL {level + 1}";
        buyPriceText.text = $"<sprite=0> {Utility.FormatMoney(price)}"; // money sprite
        upgradeProgressBarFill.fillAmount = (float)level / (float)upgradeData.MaxLevel;
        if (upgradeButton)
            upgradeButton.SetInteractable(currency >= price && level < upgradeData.MaxLevel);

        if (level == upgradeData.MaxLevel)
        {
            buyText.text = "MAX";
            buyPriceText.text = $"";
        }
    }
}