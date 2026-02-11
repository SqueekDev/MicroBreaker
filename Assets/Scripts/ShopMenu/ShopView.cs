using System.Collections.Generic;
using System.Linq;
using Data;
using UnityEngine;

namespace ShopMenu
{
    public class ShopView : MonoBehaviour
    {
        [SerializeField] private Shop _shop;
        [SerializeField] private ShopContent _shopContent;
        [SerializeField] private List<BallItemView> _ballItemViews;
        [SerializeField] private List<PlatformItemView> _platformItemViews;

        private void OnEnable()
        {
            _shop.Changed += OnShopChanged;
        }

        private void OnDisable()
        {
            _shop.Changed -= OnShopChanged;
        }

        private void InitState(ShopState shopState)
        {
            foreach (var item in _ballItemViews)
            {
                BallState ballState = shopState.BallStates.First(state => state.Type == item.Type);
                item.SetLockState(ballState.IsUnlocked);
                item.SetSelectionState(false);
            }

            foreach (var item in _platformItemViews)
            {
                PlatformState ballState = shopState.PlatformStates.First(state => state.Type == item.Type);
                item.SetLockState(ballState.IsUnlocked);
                item.SetSelectionState(false);
            }
        }

        private void SetCurrentBall(ShopState shopState)
        {
            BallItemView ballItemView = _ballItemViews.First(view => view.Type == shopState.CurrentBall);
            ballItemView.SetSelectionState(true);
        }

        private void SetCurrentPlatform(ShopState shopState)
        {
            PlatformItemView platformItemView = _platformItemViews.First(view => view.Type == shopState.CurrentPlatform);
            platformItemView.SetSelectionState(true);
        }

        private void OnShopChanged(ShopState shopState)
        {
            InitState(shopState);
            SetCurrentBall(shopState);
            SetCurrentPlatform(shopState);
        }
    }
}