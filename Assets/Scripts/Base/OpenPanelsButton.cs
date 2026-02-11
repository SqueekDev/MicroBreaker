using System.Collections.Generic;
using UnityEngine;


namespace Base
{
    public class OpenPanelsButton : GameButton
    {
        [SerializeField] private List<GamePanel> _panels;

        protected override void OnButtonClick()
        {
            foreach (var item in _panels)
            {
                item.gameObject.SetActive(true);
            }
        }
    }
}