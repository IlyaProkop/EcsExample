using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class EcsBaseActionButtonView : UIElement
{
    protected Button button { get; private set; }

    protected const string OnClickTriggerName = "OnClick";

    protected abstract void OnClickAction();
    //protected abstract Action SetWidgetName(string widgetName);
    protected abstract void Init();

    private void OnValidate()
    {
        //var clickAction = GetComponent<Leopotam.Ecs.Ui.Actions.EcsUiClickAction>();
    }

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickAction);
        Init();
    }

    private void OnDestroy()
    {
        button?.onClick.RemoveListener(OnClickAction);
    }
}
