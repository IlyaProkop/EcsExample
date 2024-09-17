//using AppsFlyerSDK;
//using Firebase.Analytics;
//using GameAnalyticsSDK;
using System.Collections;
using System.Collections.Generic;
using __Scripts.UnityComponents.REDO.SingletonAccess;
using UnityEngine;

public class AnalyticManager : MonoBehaviourSingleton<AnalyticManager>
{
    private void Initialize()
    {
        //GameAnalytics.Initialize();
    }

    public void LogEvent(string _event)
    {
        MyFacebook.Instance.LogEvent(_event);
        //GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, _event);
        //FirebaseAnalytics.LogEvent(_event, "level", Stats.getInstanse.lvl);
        //AppsFlyer.sendEvent(_event, _params);
        //MyTenjin.Instance.instance.SendEvent(_event);
    }

    public void LogEventWithAllData(string _event)
    {
        Dictionary<string, object> _params = new Dictionary<string, object>();
        //_params.Add("level_number", DataManager.Instance.MainData.LevelNumber);
        //_params.Add("time", GameDatabase.Instance.gameData.CurrentPicture.DonePaintTimeInSeconds);


        MyFacebook.Instance.LogEvent(_event, _params);
        //GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, _event, _params);
        //FirebaseAnalytics.LogEvent(_event, "level", Stats.getInstanse.lvl);
        //AppsFlyer.sendEvent(_event, _params);
        //MyTenjin.Instance.instance.SendEvent(_event);
    }

    public void LogEvent_OnLevelStart()
    {
        Dictionary<string, object> _params = new Dictionary<string, object>();
        //_params.Add("level_number", DataManager.Instance.MainData.LevelNumber);

        MyFacebook.Instance.LogEvent("level_start", _params);
        //GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "level_start", GameData.Instance.mainData.LevelNumber);
        //AppMetrica.Instance.ReportEvent("level_start", _params);
        //AppMetrica.Instance.SendEventsBuffer();
    }

    public void LogEvent_OnLevelFinish()
    {
        Dictionary<string, object> _params = new Dictionary<string, object>();
        //_params.Add("level_number", DataManager.Instance.MainData.LevelNumber);

        MyFacebook.Instance.LogEvent("level_finish", _params);
        //GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "level_finish", GameData.Instance.mainData.LevelNumber);
        //AppMetrica.Instance.ReportEvent("level_finish", _params);
        //AppMetrica.Instance.SendEventsBuffer();
    }

    public void LogEvent(string _event, Dictionary<string, object> _params)
    {

    }

    private void OnApplicationQuit()
    {
        LogEventWithAllData("game_quit");
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
            LogEventWithAllData("game_quit");
    }
}
