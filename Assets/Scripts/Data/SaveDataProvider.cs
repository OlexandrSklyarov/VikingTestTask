using UnityEngine;

namespace Data
{
    public static class SaveDataProvider
    {
        public static bool IsGameRestarted => 
                PlayerPrefs.GetInt(ConstPrm.PrefsData.GAME_RESTART_FLAG, 0) > 0;

        
        public static void SetGameRestarted(bool isRestarted) =>
            PlayerPrefs.SetInt(ConstPrm.PrefsData.GAME_RESTART_FLAG, isRestarted ? 1 : 0);
    }
}