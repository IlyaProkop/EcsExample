using Client;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;

public class SettingScreen : BaseScreen
{
    [Header("Vibration")]
    [SerializeField] private ActionButton vibrationButton;
    [SerializeField] private Sprite vibrationOnSprite;
    [SerializeField] private Sprite vibrationOffSprite;
    [SerializeField] private Image vibrationStateImage;

    [Header("Sound")]
    [SerializeField] private ActionButton soundButton;
    [SerializeField] private Sprite soundOnSprite;
    [SerializeField] private Sprite soundOffSprite;
    [SerializeField] private Image soundStateImage;

    protected override void ManualStart()
    {
        vibrationStateImage.sprite = GameData.Instance.PlayerData.IsVibrationOn ? vibrationOnSprite : vibrationOffSprite;
        soundStateImage.sprite = GameData.Instance.PlayerData.IsSoundOn ? soundOnSprite : soundOffSprite;

        vibrationButton.OnClickEvent.AddListener(() =>
        EcsWorld.NewEntity()
        .Get<SetVibrationStateRequest>());

        vibrationButton.OnClickEvent.AddListener(UpdateVibrationButtonView);

        soundButton.OnClickEvent.AddListener(() =>
        EcsWorld.NewEntity()
        .Get<SetSoundStateRequest>());

        soundButton.OnClickEvent.AddListener(UpdateSoundButtonView);
    }

    private void UpdateVibrationButtonView()
    {
        vibrationStateImage.sprite = GameData.Instance.PlayerData.IsVibrationOn ? vibrationOffSprite : vibrationOnSprite;
        SetShowState(false);
    }

    private void UpdateSoundButtonView()
    {
        soundStateImage.sprite = GameData.Instance.PlayerData.IsSoundOn ? soundOffSprite : soundOnSprite;
        SetShowState(false);
    }
}