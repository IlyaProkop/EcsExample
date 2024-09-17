using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using __Scripts.UnityComponents.REDO.SingletonAccess;
using UnityEditor;

public class GameData : MonoBehaviourSingleton<GameData>
{
    public StaticData StaticData;
    public BalanceData BalanceData;
    public PlayerData PlayerData;
    public SceneData SceneData;
    public RuntimeData RuntimeData;

    private void Awake()
    {
        Debug.Log(Utility.GetDataPath());

        RuntimeData = new RuntimeData();
        RuntimeData.Init();
        PlayerData.Init();
        BalanceData.InjectEcsWorld(this);
        LoadData();

        PlayerData.IsGameLaunchedBefore = true;
    }

    private void SaveData()
    {
        PlayerData.SaveData();
    }

    private void LoadData()
    {
        PlayerData.LoadData();
    }

    [NaughtyAttributes.Button]
    [ExecuteInEditMode]
    public void ResetData()
    {
        PlayerData.ResetData();
    }

#if UNITY_EDITOR
    [ExecuteInEditMode]
    [MenuItem("Tools/DeleteAllGameData")]
    public static void DeleteAllGameData()
    {
        if (Directory.Exists(Utility.GetDataPath()))
            Directory.Delete(Utility.GetDataPath(), true);
    }
#endif

    private void OnApplicationQuit()
    {
        SaveData();
#if UNITY_EDITOR
        //DeleteAllGameData(); // TODO: REMOVE
#endif
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
            SaveData();
    }
}

