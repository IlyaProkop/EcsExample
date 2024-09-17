using Leopotam.Ecs;
using MoreMountains.NiceVibrations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class BaseScreen : UIElement
{
    protected bool IsScreenShow;

    [HideInInspector] public EcsWorld EcsWorld;
    [HideInInspector] public GameData GameData;

    [HideInInspector] public UnityEvent OnHideScreen;
    [HideInInspector] public UnityEvent OnShowScreen;

    protected abstract void ManualStart();
    
    public void InjectEcsWorld(EcsWorld ecsWorld)
    {
        EcsWorld = ecsWorld;
    }

    public void Init(GameData gameData)
    {
        GameData = gameData;
        ManualStart();
        IsScreenShow = false;
    }

    public override void SetShowState(bool _isShow) // [System.Runtime.CompilerServices.CallerMemberName] string memberName = "" - WHO
    {
        base.SetShowState(_isShow);
        if (_isShow)
        {
            OnShowScreen?.Invoke();
            IsScreenShow = true;
        }
        else
        {
            OnHideScreen?.Invoke();
            IsScreenShow = false;
        }
    }
}
