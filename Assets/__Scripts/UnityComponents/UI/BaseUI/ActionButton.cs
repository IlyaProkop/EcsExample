using MoreMountains.NiceVibrations;
using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ActionButton : UIElement
{
    [HideInInspector] public UnityEvent OnClickEvent;

    private Button unityButton;

    private const string onClickTriggerName = "OnClick";

    private void Awake()
    {
        unityButton = GetComponent<Button>();
        unityButton.onClick.AddListener(() => OnClickEvent.Invoke());
        OnClickEvent.AddListener(OnClickEventReaction);
    }
    [Button()]
    public void ManualClick()
    {
        OnClickEvent.Invoke();
        OnClickEventReaction();
    }

    private void OnClickEventReaction()
    {
        MMVibrationManager.Haptic(HapticTypes.MediumImpact);
        animator.SetTrigger(onClickTriggerName);
    }

    private void OnDestroy()
    {
        unityButton?.onClick.RemoveAllListeners();
        OnClickEvent.RemoveAllListeners();
    }

    public void SetInteractable(bool flag)
    {
        if (!unityButton)
            unityButton = GetComponent<Button>();

        unityButton.interactable = flag;
    }
}