using System;
using Base;
using UnityEngine;

namespace ShopMenu
{
    public class CloseShopButton : GameButton
    {
        [SerializeField] private ShopPanel _shopPanel;

        public Action Clicked;

        protected override void OnButtonClick()
        {
            Clicked?.Invoke();
            _shopPanel.gameObject.SetActive(false);
        }
    }
}