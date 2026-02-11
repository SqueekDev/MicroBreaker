using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShopMenu
{
    public abstract class ShopItemView : MonoBehaviour
    {
        [SerializeField] private Image _highlightBackground;
        [SerializeField] private Image _contentImage;
        [SerializeField] private Image _lockImage;
        [SerializeField] private Button _selectionButton;
        [SerializeField] private Button _buyButton;
        [SerializeField] private List<TMP_Text> _prices;

        protected Sprite ContentSprite;

        protected virtual void Awake()
        {
            _contentImage.sprite = ContentSprite;
            SetLockState(false);
            SetSelectionState(false);
        }

        protected virtual void OnEnable()
        {
            _selectionButton.onClick.AddListener(OnSelectButtonClick);
            _buyButton.onClick.AddListener(OnBuyButtonClick);
        }

        protected virtual void OnDisable()
        {
            _selectionButton.onClick.RemoveListener(OnSelectButtonClick);
            _buyButton.onClick.RemoveListener(OnBuyButtonClick);
        }

        public void SetLockState(bool isUnlocked)
        {
            _selectionButton.interactable = isUnlocked;
            _lockImage.gameObject.SetActive(!isUnlocked);
            _buyButton.gameObject.SetActive(!isUnlocked);
        }

        public void SetSelectionState(bool isSelected)
        {
            _highlightBackground.gameObject.SetActive(isSelected);
        }

        protected void SetPrices(ShopItem shopItem)
        {
            foreach (var item in _prices)
            {
                item.text = shopItem.Price.ToString();
            }
        }

        protected abstract void OnSelectButtonClick();

        protected abstract void OnBuyButtonClick();
    }
}