using Base;
using Data;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class LoadLevelButton : GameButton
    {
        private int _number;
        
        public void SetLevelNumber(LevelsEnum number)
        {
            _number = (int)number;
        }

        protected override void OnButtonClick()
        {
            if (SceneManager.sceneCountInBuildSettings > _number)
            {
                SceneManager.LoadScene(_number);
            }
        }
    }
}