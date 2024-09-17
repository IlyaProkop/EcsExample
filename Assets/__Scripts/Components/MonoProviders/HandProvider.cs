using Leopotam.Ecs;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public struct HandProvider
{
    public int ProductLineId;
    public List<GameObject> Bags;
    public ParticleSystem EarnMoneyPS;
    public TextMeshProUGUI PopCounterText;
    public Transform EarnMoneyPoint;
}