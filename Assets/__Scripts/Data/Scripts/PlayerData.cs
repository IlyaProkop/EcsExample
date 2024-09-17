using System;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using RotaryHeart.Lib.SerializableDictionary;

[CreateAssetMenu(menuName = "GameData/PlayerData", fileName = "PlayerData")]
public class PlayerData : BaseDataSO
{
    [Header("GameSettings")]
    public bool IsGameLaunchedBefore;
    public bool IsVibrationOn;
    public bool IsSoundOn;

    [Header("Tutorials")]
    public List<bool> TutrorialStates;

    [Header("Money & Popcorn")]
    public double Money;
    public double MoneyInSec;
    public double GoldPopcornAmount;
    public double PopcornAmount;
    public int BagSold;

    [Header("Level")]
    public int CurrentLevelIndex;
    public float CurrentLevelProgress;

    [Header("Level Upgardes")]
    public UpgradeLevelData UpgradeLevels;

    //[Header("Boosters")] // save it for reload

    public List<bool> ConveyorBuyed;
    public List<bool> ProductLineBuyed;

    public override void ResetData()
    {
        Money = 0;
        MoneyInSec = 0;
        GoldPopcornAmount = 0;
        PopcornAmount = 0;

        CurrentLevelIndex = 0;
        CurrentLevelProgress = 0.0f;

        for (int i = 1; i < ConveyorBuyed.Count; i++)
            ConveyorBuyed[i] = false;

        for (int i = 1; i < ProductLineBuyed.Count; i++)
            ConveyorBuyed[i] = false;

        for (int i = 0; i < UpgradeLevels.Count; i++)
            UpgradeLevels[(StaticData.UpgradeType)i] = 0;
    }

    public void Init()
    {
        Money = 0;
        MoneyInSec = 0;
        GoldPopcornAmount = 0;
        PopcornAmount = 0;

        CurrentLevelIndex = 0;
        CurrentLevelProgress = 0.0f;

        IsVibrationOn = true;
        IsSoundOn = true;
        
        for (int i = 0; i < UpgradeLevels.Count; i++)
            UpgradeLevels[(StaticData.UpgradeType)i] = 0;

        for (int i = 0; i < 4; i++)
            TutrorialStates[i] = false;

        for (int i = 0; i < 5; i++)
            ConveyorBuyed[i] = false;
        ConveyorBuyed[0] = true;

        for (int i = 0; i < 3; i++)
            ProductLineBuyed[i] = false;
        ProductLineBuyed[0] = true;
    }
}

[Serializable]
public class UpgradeLevelData : SerializableDictionaryBase<StaticData.UpgradeType, int> { }