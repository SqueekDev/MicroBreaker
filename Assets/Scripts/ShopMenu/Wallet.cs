using System;
using Base;
using UnityEngine;

namespace ShopMenu
{
    public class Wallet : MonoBehaviour
    {
        public Action<int> AmountChanged;

        public int Money { get; private set; }

        private void Start()
        {
            Money = PlayerPrefs.GetInt(PlayerPrefsKeys.Money, 0);
            AmountChanged?.Invoke(Money);
        }

        public void AddMoney(int amount)
        {
            if (amount > 0)
            {
                Money += amount;
                SaveChange();
                AmountChanged?.Invoke(Money);
            }
        }

        public void TrySpendMoney(int amount)
        {
            if (amount <= Money)
            {
                Money -= amount;
                SaveChange();
                AmountChanged?.Invoke(Money);
            }
        }

        private void SaveChange()
        {
            PlayerPrefs.SetInt(PlayerPrefsKeys.Money, Money);
        }
    }
}