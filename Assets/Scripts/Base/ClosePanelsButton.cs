using System.Collections.Generic;
using UnityEngine;

namespace Base
{
    public class ClosePanelsButton : GameButton
    {
        [SerializeField] private List<GamePanel> _panels;

        protected override void OnButtonClick()
        {
            foreach (var item in _panels)
            {
                item.gameObject.SetActive(false);
            }
        }
    }
}