using FlatKit;
using Leopotam.Ecs;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ConveyorProvider
{
    public int Id;
    public int ProductLineId;

    [HideInInspector] public bool IsBuyed;

    public BlendMaterialController StoveMaterial;
    public GameObject HeatSteam;
    public GameObject AirHeatDistortion;
    public UvScroller BeltScroller;

    public ParticleSystem BuildDustPS;

    public List<Transform> SpawnPoints;
    public List<Transform> PrepareJumpPoints;
    public List<Transform> JumpPoints;
}