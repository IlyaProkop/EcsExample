using Leopotam.Ecs;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyConveyorScreen : BaseScreen
{
    [SerializeField] private int conveyorId;
    [SerializeField] private int productLineId;

    [SerializeField] private ActionButton buyButton;

    [SerializeField] private TextMeshProUGUI priceText;

    protected override void ManualStart()
    {
        buyButton.OnClickEvent.AddListener(() =>
            EcsWorld.NewEntity()
                .Get<BuyConveyorRequest>().ConveyorId = conveyorId);

        UpdateButtonView();
    }

    public void UpdateButtonView()
    {
        if(GameData.Instance.PlayerData.ConveyorBuyed[conveyorId] || !GameData.Instance.PlayerData.ConveyorBuyed[conveyorId - 1])
            gameObject.SetActive(false);
        else
        {
            gameObject.SetActive(true);
            double price = GameData.Instance.BalanceData.BaseConveyorPrice * Mathf.Pow(GameData.Instance.BalanceData.ConveyorPriceMultiplierForNumber, conveyorId);
            bool isCanBuy = GameData.Instance.PlayerData.Money >= price;
            priceText.text = $"<sprite=0> {Utility.FormatMoney(price)}"; // money sprite
            buyButton.SetInteractable(isCanBuy);
        }
    }
}