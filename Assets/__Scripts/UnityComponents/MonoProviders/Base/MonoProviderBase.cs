using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoProviderBase : MonoBehaviour
{
    public abstract void Provide(ref EcsEntity entity);
}