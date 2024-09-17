using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Singleton Init
    private static AudioManager _instance;

    void Awake() // Init in order
    {
        if (_instance == null)
            Init();
        else if (_instance != this)
            Destroy(gameObject);
    }

    public static AudioManager Instance // Init not in order
    {
        get
        {
            if (_instance == null)
                Init();
            return _instance;
        }
        private set { _instance = value; }
    }

    static void Init() // Init script
    {
        _instance = FindObjectOfType<AudioManager>();
        _instance.Initialize();
    }
    #endregion

    public AudioMixer masterMixer;

    public Sound[] sounds;
    private bool checkMusic;
    private bool checkSFX;

    [HideInInspector]
    public AudioSource musicTrack;

    void Initialize()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;

            s.source.outputAudioMixerGroup = s.mixer;
        }
    }

    public void Play(string sound)
    {
        if (!GameData.Instance.PlayerData.IsSoundOn)
            return;

        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

        s.source.Play();
    }

    public void Stop(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
    }

    public void AllStop()
    {
        foreach (Sound s in sounds)
        {
            if (s.source != null)
                s.source.Stop();
        }
    }

    public void ToggleAudio(bool _on)
    {
        if (_on)
            masterMixer.SetFloat("AudioVolume", -5f);
        else
            masterMixer.SetFloat("AudioVolume", -80f);
    }
}

