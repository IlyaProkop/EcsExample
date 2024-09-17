using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/StaticData", fileName = "StaticData")]
public class StaticData : BaseDataSO
{
    [Header("Prefabs")]
    public GameObject GoldPopcornPrefab;
    public GameObject PopcornPrefab;
    public GameObject EarnInfoPrefab;
    public GameObject EmptyPrefab;

    [Header("Tags")]
    [BoxGroup("Tags")] [Tag] public string DespawnTag;
    [BoxGroup("Tags")] [Tag] public string CookingZoneTag;
    [BoxGroup("Tags")] [Tag] public string SellZoneTag;
    [BoxGroup("Tags")] [Tag] public string GroundTag;
    [BoxGroup("Tags")] [Tag] public string ConveyorTag;
    [BoxGroup("Tags")] [Tag] public string ChocolateAdditionTag;
    [BoxGroup("Tags")] [Tag] public string GoldPopcornTag;

    public static class PopAnimations
    {
        public static readonly int IsWalking = Animator.StringToHash("IsWalking");
        public static readonly int IsRunning = Animator.StringToHash("IsRunning");
        public static readonly int IsPop = Animator.StringToHash("IsPop");
        public static readonly int IsPrepareToJump = Animator.StringToHash("IsPrepareToJump");
        public static readonly int IsJump = Animator.StringToHash("IsJump");
        public static readonly int IsMissFalling = Animator.StringToHash("IsMissFalling");
        public static readonly int IsFalling = Animator.StringToHash("IsFalling");
        public static readonly int IsGoldTaken = Animator.StringToHash("IsGoldTaken");
        public static readonly string WalkingIndex = "WalkingIndex";
        public static readonly string JumpIndex = "JumpIndex";
    }

    public static class HandAnimations
    {
        public static readonly int IsTaken = Animator.StringToHash("IsTaken");
        public static readonly int IsPopIn = Animator.StringToHash("IsPopIn");
    }

    public enum PopEmotions
    {
        None = 0,
        Smile = 1,
        Happy = 2,
        Scary = 3
    }
    
    public enum UpgradeType
    {
        Heat = 0,
        Speed = 1,
        Earn = 2,
        HeatPower = 3,
        LuckyBoy = 4,
        Chocolate = 5
    }
    
    public enum PopBody
    {
        RawCorn = 0,
        Popcorn = 1,
        Skeleton = 2,
        PopcornWithoutLimbs = 3
    }

    public enum PopAdditions
    {
        None = 0,
        Chocolate = 1,
        Salt = 2,
        Caramel = 3,
        Wasabi = 4
    }

    public enum Tutorials
    {
        Heating = 0,
        Upgrade = 1,
        SpeedUp = 2,
        GoldTap = 3
    }

    public static class AudioSound
    {
        public static readonly string CashSound = "cash";
        public static readonly string ConveyorSound = "conveyor";
        public static readonly string ExplosionSound = "explosion";
        public static readonly string JumpSound = "jump";
        public static readonly string ShootSound = "shoot";
        public static readonly string MusicSound = "music";
        public static readonly string BuildNewConveyorSound = "buildnewconveyor";
        public static readonly string BuyUpdateSound = "buyupdate";
        public static readonly string SellBagSound = "sellbag";
        public static readonly string Click1Sound = "click1";
        public static readonly string Click2Sound = "click2";
        public static readonly string UiNavigationSound = "uinavigation";
        public static readonly string ChpokSound = "chpok";
    }

    public override void ResetData()
    {
    }

}