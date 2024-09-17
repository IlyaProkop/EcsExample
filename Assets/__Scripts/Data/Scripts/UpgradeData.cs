using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "GameData/UpgradeData", fileName = "UpgradeData")]
[Serializable]
public class UpgradeData : BaseDataSO
{
    public StaticData.UpgradeType UpgradeType;
    
    [Header("View")]
    public string UpgradeName;
    [NaughtyAttributes.ResizableTextArea]
    public string UpgradeDescription;
    public Sprite UpgradeSprite;

    [Header("Data")]
    public int MaxLevel;
    public bool IsEpicUpgrade;
    public double BasePrice;
    public float PriceProgressionCoef;

    [Header("Progression")]
    public float MultiplierForLevel;
    public float StartValue;
    public float StepValue;

    private GameData _gameDataService;
    public void Inject(GameData gameDataService) => _gameDataService = gameDataService;
    
    public bool CanBuyIt()
    {
        double price = BasePrice * Mathf.Pow(PriceProgressionCoef, _gameDataService.PlayerData.UpgradeLevels[UpgradeType]);
        double currency = IsEpicUpgrade ? GameData.Instance.PlayerData.GoldPopcornAmount : GameData.Instance.PlayerData.Money;

        return currency >= price && _gameDataService.PlayerData.UpgradeLevels[UpgradeType] < MaxLevel;
    }

    public float GetValue()
    {
        float value = 0;

        if (MultiplierForLevel != 0)
            value = StartValue * Mathf.Pow(MultiplierForLevel, _gameDataService.PlayerData.UpgradeLevels[UpgradeType]);
        if(StepValue != 0)
            value = StartValue + StepValue * _gameDataService.PlayerData.UpgradeLevels[UpgradeType];

        return value;
    }

    public override void ResetData()
    {
        throw new NotImplementedException();
    }
}