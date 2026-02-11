using TMPro;
using UnityEngine;

namespace ShopMenu
{
    public class WalletView : MonoBehaviour
    {
        [SerializeField] private Wallet _wallet;
        [SerializeField] private TMP_Text _money;

        private void OnEnable()
        {
            _wallet.AmountChanged += OnValueChanged;
        }

        private void OnDisable()
        {
            _wallet.AmountChanged -= OnValueChanged;
        }

        private void OnValueChanged(int value)
        {
            _money.text = value.ToString();
        }
    }
}