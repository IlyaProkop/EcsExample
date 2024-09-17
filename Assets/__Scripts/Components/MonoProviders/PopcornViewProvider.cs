using Leopotam.Ecs;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct PopcornViewProvider
{
    public List<GameObject> Bodies;
    public List<GameObject> Emotions;
    public List<GameObject> Additions;

    [Header("VFXs")]
    public GameObject SpeedUpTrail;
}