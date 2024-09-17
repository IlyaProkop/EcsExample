using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;

[Serializable]
public struct ObjectStackProvider
{
    public int Rows;
    public int Columns;
    public int ObjectsInColumn;
    public Vector3 ObjectsOffset;

    public List<EcsEntity> Objects;
    public List<Transform> GridPoints;
    public int Capacity;
}
