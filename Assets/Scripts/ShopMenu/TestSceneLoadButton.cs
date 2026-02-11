using Base;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShopMenu
{
    public class TestSceneLoadButton : GameButton
    {
        [SerializeField] private int _sceneNumber;

        protected override void OnButtonClick()
        {
            SceneManager.LoadScene(_sceneNumber);
        }
    }
}