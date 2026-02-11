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
    }
}