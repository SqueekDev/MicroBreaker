using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShopMenu
{
    public class BuyButtonStateChanger : MonoBehaviour
    {
        [SerializeField] private Wallet _wallet;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;
        [SerializeField] private ShopItem _item;

        private Color _startColor;

        private void Awake()
        {
            _startColor = _image.color;
        }

        private void OnEnable()
        {
            _wallet.AmountChanged += OnMoneyAmountChanged;
            ChangeState();
        }

        private void OnDisable()
        {
            _wallet.AmountChanged -= OnMoneyAmountChanged;
            ChangeColors(_startColor);
        }

        private void ChangeState()
        {
            if (_wallet.Money < _item.Price)
            {
                ChangeColors(Color.red);
                _button.interactable = false;
            }
            else
            {
                ChangeColors(_startColor);
                _button.interactable = true;
            }
        }

        private void ChangeColors(Color color)
        {
            _image.color = color;
            _text.color = color;
        }

        private void OnMoneyAmountChanged(int amount)
        {
            ChangeState();
        }
    }
}