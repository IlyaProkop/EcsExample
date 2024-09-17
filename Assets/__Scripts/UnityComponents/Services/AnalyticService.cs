//using AppsFlyerSDK;
//using Firebase.Analytics;
//using GameAnalyticsSDK;

using System.Collections.Generic;


namespace Client.Analytics.AnalyticManager
{
    public class AnalyticService
    {
        private void Initialize()
        {
            //GameAnalytics.Initialize();
        }

        /*public void LogEvent(string _event)
        {
            //MyFacebook.Instance.LogEvent(_event);
            //GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, _event);
            //FirebaseAnalytics.LogEvent(_event, "level", Stats.getInstanse.lvl);
            //AppsFlyer.sendEvent(_event, _params);
            //MyTenjin.Instance.instance.SendEvent(_event);
        }*/

        public void LogEventWithAllData(string _event)
        {
            var _params = new Dictionary<string, object>();
            //_params.Add("level_number", DataManager.Instance.MainData.LevelNumber);
            //_params.Add("time", GameDatabase.Instance.gameData.CurrentPicture.DonePaintTimeInSeconds);


            //MyFacebook.Instance.LogEvent(_event, _params);
            //GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, _event, _params);
            //FirebaseAnalytics.LogEvent(_event, "level", Stats.getInstanse.lvl);
            //AppsFlyer.sendEvent(_event, _params);
            //MyTenjin.Instance.instance.SendEvent(_event);
        }

        public void LogEvent_OnLevelStart()
        {
            var _params = new Dictionary<string, object>();
            //_params.Add("level_number", DataManager.Instance.MainData.LevelNumber);

            //MyFacebook.Instance.LogEvent("level_start", _params);
            //GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "level_start", GameData.Instance.mainData.LevelNumber);
            //AppMetrica.Instance.ReportEvent("level_start", _params);
            //AppMetrica.Instance.SendEventsBuffer();
        }

        public void LogEvent_OnLevelFinish()
        {
            var _params = new Dictionary<string, object>();
            //_params.Add("level_number", DataManager.Instance.MainData.LevelNumber);

            //MyFacebook.Instance.LogEvent("level_finish", _params);
            //GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "level_finish", GameData.Instance.mainData.LevelNumber);
            //AppMetrica.Instance.ReportEvent("level_finish", _params);
            //AppMetrica.Instance.SendEventsBuffer();
        }

        public void LogEvent(string _event, Dictionary<string, object> _params)
        {
            AppMetrica.Instance.ReportEvent(_event, _params);
            /*foreach (var _param in _params)
                FirebaseAnalytics.LogEvent(_event, _param.Key, (int)_param.Value);*/
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
}