using Base;
using Data;
using UnityEngine.SceneManagement;

namespace ShopMenu
{
    public class LoadMainMenuButton : GameButton
    {
        protected override void OnButtonClick()
        {
            SceneManager.LoadScene((int)LevelsEnum.MainMenu);
        }
    }
}