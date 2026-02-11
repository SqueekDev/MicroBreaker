using System;
using Data;
using UnityEngine;

namespace ShopMenu
{
    public class PlatformItemView : ShopItemView
    {
        [SerializeField] private PlatformItem _item;

        public event Action<PlatformEnum> PlatformSelectionButtonClicked;
        public event Action<PlatformEnum, int> PlatformBuyButtonClicked;

        public PlatformEnum Type => _item.Type;

        protected override void Awake()
        {
            ContentSprite = _item.Image;
            SetPrices(_item);
            base.Awake();
        }

        protected override void OnBuyButtonClick()
        {
            PlatformBuyButtonClicked?.Invoke(Type, _item.Price);
        }

        protected override void OnSelectButtonClick()
        {
            PlatformSelectionButtonClicked?.Invoke(Type);
        }
    }
}