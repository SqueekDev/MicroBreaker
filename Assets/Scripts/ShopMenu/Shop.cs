using System;
using System.Collections.Generic;
using Base;
using Data;
using UnityEngine;

namespace ShopMenu
{
    public class Shop : MonoBehaviour
    {
        private const int StartItemType = 0;

        [SerializeField] private Wallet _wallet;
        [SerializeField] private ShopContent _shopContent;
        [SerializeField] private List<BallItemView> _ballItemViews;
        [SerializeField] private List<PlatformItemView> _PlatformItemViews;

        private BallsEnum _currentBallType;
        private PlatformEnum _currentPlatformType;
        private ShopItemUnlocker _unlocker = new ShopItemUnlocker();
        private List<BallState> _ballStates = new List<BallState>();
        private List<PlatformState> _platformStates = new List<PlatformState>();

        public event Action<ShopState> Changed;

        private void OnEnable()
        {
            foreach (var item in _ballItemViews)
            {
                item.BallSelectionButtonClicked += OnBallItemSelected;
                item.BallBuyButtonClicked += OnBallItemUnlocked;
            }

            foreach (var item in _PlatformItemViews)
            {
                item.PlatformSelectionButtonClicked += OnPlatformItemSelected;
                item.PlatformBuyButtonClicked += OnPlatformItemUnlocked;
            }
        }

        private void Start()
        {
            CheckSave();
        }

        private void OnDisable()
        {
            foreach (var item in _ballItemViews)
            {
                item.BallSelectionButtonClicked -= OnBallItemSelected;
                item.BallBuyButtonClicked -= OnBallItemUnlocked;
            }

            foreach (var item in _PlatformItemViews)
            {
                item.PlatformSelectionButtonClicked -= OnPlatformItemSelected;
                item.PlatformBuyButtonClicked -= OnPlatformItemUnlocked;
            }
        }

        private void CreateState()
        {
            foreach (var item in _shopContent.BallItems)
            {
                _ballStates.Add(new BallState(false, item.Type));
            }

            _currentBallType = _unlocker.UnlockBall(_ballStates, StartItemType);

            foreach (var item in _shopContent.PlatformItems)
            {
                _platformStates.Add(new PlatformState(false, item.Type));
            }

            _currentPlatformType = _unlocker.UnlockPlatform(_platformStates, StartItemType);
            SaveState();
        }

        private void SaveState()
        {
            ShopState shopState = new ShopState(_ballStates, _platformStates, _currentBallType, _currentPlatformType);
            SaveSystem.SaveShopState(shopState);
            Changed?.Invoke(shopState);
        }

        private ShopState LoadState()
        {
            ShopState shopState = SaveSystem.LoadShopState();
            Changed?.Invoke(shopState);
            return shopState;
        }

        private void CheckSave()
        {
            if (PlayerPrefs.HasKey(PlayerPrefsKeys.ShopState))
            {
                ShopState shopState = LoadState();
                _ballStates = shopState.BallStates;
                _platformStates = shopState.PlatformStates;
                _currentBallType = shopState.CurrentBall;
                _currentPlatformType = shopState.CurrentPlatform;
            }
            else
            {
                CreateState();
            }

            SaveState();
        }

        private void OnBallItemSelected(BallsEnum type)
        {
            _currentBallType = type;
            SaveState();
        }

        private void OnPlatformItemSelected(PlatformEnum type)
        {
            _currentPlatformType = type;
            SaveState();
        }

        private void OnBallItemUnlocked(BallsEnum type, int price)
        {
            _unlocker.UnlockBall(_ballStates, type);
            _wallet.TrySpendMoney(price);
            SaveState();
        }

        private void OnPlatformItemUnlocked(PlatformEnum type, int price)
        {
            _unlocker.UnlockPlatform(_platformStates, type);
            _wallet.TrySpendMoney(price);
            SaveState();
        }
    }
}