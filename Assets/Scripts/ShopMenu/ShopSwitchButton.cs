using System;
using Base;
using Data;
using UnityEngine;

namespace ShopMenu
{
    public class ShopSwitchButton : GameButton
    {
        [SerializeField] private SectionView _section;
        [SerializeField] private SectionIcon _selectedIcon;
        [SerializeField] private ShopButtonsEnum _buttonEnum;

        public event Action<SectionView, SectionIcon, ShopButtonsEnum> Changed;

        protected override void OnButtonClick()
        {
            Changed?.Invoke(_section, _selectedIcon, _buttonEnum);
        }
    }
}