using System;
using Data;
using UnityEngine;

namespace ShopMenu
{
    public class BallItemView : ShopItemView
    {
        [SerializeField] private BallItem _item;

        public event Action<BallsEnum> BallSelectionButtonClicked;
        public event Action<BallsEnum, int> BallBuyButtonClicked;

        public BallsEnum Type => _item.Type;

        protected override void Awake()
        {
            ContentSprite = _item.Image;
            SetPrices(_item);
            base.Awake();
        }

        protected override void OnBuyButtonClick()
        {
            BallBuyButtonClicked?.Invoke(Type, _item.Price);
        }

        protected override void OnSelectButtonClick()
        {
            BallSelectionButtonClicked?.Invoke(Type);
        }
    }
}