using Base;
using TMPro;
using UnityEngine;

namespace MainMenu
{
    public class BricksSmashedView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _bricks;

        private void Start()
        {
            _bricks.text = PlayerPrefs.GetInt(PlayerPrefsKeys.BricksSmashed, 0).ToString();
        }
    }
}