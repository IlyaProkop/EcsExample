using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SpawnPrefab
{
    public GameObject Prefab;
    public Vector3 Position;
    public Quaternion Rotation;
    public Transform Parent;
    public EcsEntity Entity;
}
