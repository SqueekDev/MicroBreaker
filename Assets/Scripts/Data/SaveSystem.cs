using Base;
using Newtonsoft.Json;
using UnityEngine;

namespace Data
{
    public static class SaveSystem
    {
        public static void SaveShopState(ShopState shopState)
        {
            var json = JsonConvert.SerializeObject(shopState, Formatting.Indented);
            PlayerPrefs.SetString(PlayerPrefsKeys.ShopState, json);
        }

        public static ShopState LoadShopState()
        {
            ShopState shopState = JsonConvert.DeserializeObject<ShopState>(PlayerPrefs.GetString(PlayerPrefsKeys.ShopState));
            return shopState;
        }

        public static void SaveLevelStates(LevelStates levels)
        {
            var json = JsonConvert.SerializeObject(levels, Formatting.Indented);
            PlayerPrefs.SetString(PlayerPrefsKeys.LevelStates, json);
        }

        public static LevelStates LoadLevelStates()
        {
            LevelStates levels = JsonConvert.DeserializeObject<LevelStates>(PlayerPrefs.GetString(PlayerPrefsKeys.LevelStates));
            return levels;
        }
    }
}