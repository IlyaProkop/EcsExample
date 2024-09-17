using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;

[Serializable]
public struct ObjectPoolProvider
{
    public int PoolDepth;
    public GameObject Prefab;
    public Queue<EcsEntity> Pool;
    //public EcsEntity DefaultEntity;
    //public MonoEntity MonoEntity;
}
