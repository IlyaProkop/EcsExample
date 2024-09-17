using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/BalanceData", fileName = "BalanceData")]
public class BalanceData : BaseDataSO
{
    [Header("Base Balance")]
    public float BasePopcornSpeed;
    public float BaseConveyorBeltSpeed;
    public float TapSpeedUpSpeedCoef;

    public float BaseHeatingSpeed;
    public float BaseColdingSpeed;
    [MinMaxSlider(1.0f, 75.0f)]
    public Vector2 CurrentTemperatureCap;
    public float MaxTemperature;

    public int BasePopcornsInBigBag;
    public float GlodPopcornsChance;

    [Header("Time")]
    public float SpeedUpTime;
    public float CleanTime;
    public float BaseSpawnPopTime;
    public float BaseCookingTime;
    public float BaseGetGoldPopTime;

    [Header("OfflineBonus")]
    [MinMaxSlider(1.0f, 8.0f)]
    public Vector2 OfflineTimeHourCap;

    [Header("Money Balance")]
    public double StartMoney;

    public double BasePopSellReward;
    public double PopSellRewardMultiplier;

    public double BaseBagSellReward;
    public double BagSellRewardMultiplier;

    public double BaseConveyorPrice;
    public float ConveyorPriceMultiplierForNumber;

    public float ChocoPopMultiplier;

    [Header("Physic Balance")]
    [MinMaxSlider(1.0f, 10.0f)]
    public Vector2 LaunchPopcornForce;
    [MinMaxSlider(1.0f, 10.0f)]
    public Vector2 PopingPopcornForce;
    [MinMaxSlider(1.0f, 10.0f)]
    public Vector2 JumpPopcornForce;
    [MinMaxSlider(0.0f, 3.0f)]
    public Vector2 CookingShakePopcornForce;

    [Header("Upgrade Data")]
    public List<UpgradeData> UpgradeData;

    [Header("Upgrades Balance")]
    public float TemperatureUpgradeStep; // 2.5f //
    public float ConveyerSpeedUpgradeBase; // 0.001f //
    public float SpawnTimePopMultiplierForLevel; // 0.9f // 
    public float CookingTimePopMultiplierForLevel; // 0.9f ?
    public float EarningPopMultiplierForLevel; // 1.16f //
    public float EarningBagMultiplierForLevel; // 1.07f // 
    public float EarningOfflineMultiplierForLevel; // 1.07f //
    public float HeatingSpeedMultiplierForLevel; // 1.07f //

    public void InjectEcsWorld(GameData gameData)
    {
        foreach (var data in UpgradeData)
            data.Inject(gameData);
    }
    public override void ResetData()
    {
    }
}