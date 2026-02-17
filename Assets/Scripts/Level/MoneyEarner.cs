using System.Collections.Generic;
using Field;
using Platform;
using ShopMenu;
using UnityEngine;

namespace Level
{
    public class MoneyEarner : MonoBehaviour
    {
        [SerializeField] private Wallet _wallet;
        [SerializeField] private LevelFinisher _levelFinisher;
        [SerializeField] private List<FragmentsCollector> _collectors;
        [SerializeField] private List<BonusBrick> _bonusBricks;

        public int EarnedMoney { get; private set; }

        private void OnEnable()
        {
            foreach (var item in _collectors)
            {
                item.Picked += OnFragmentPicked;
            }

            foreach (var item in _bonusBricks)
            {
                item.Destroyed += OnBonusBrickDestroyed;
            }

            _levelFinisher.Finished += OnLevelFinished;
        }

        private void OnDisable()
        {
            foreach (var item in _collectors)
            {
                item.Picked -= OnFragmentPicked;
            }

            foreach (var item in _bonusBricks)
            {
                item.Destroyed -= OnBonusBrickDestroyed;
            }

            _levelFinisher.Finished -= OnLevelFinished;
        }

        private void OnFragmentPicked(Fragment fragment)
        {
            EarnedMoney += fragment.Money;
        }

        private void OnBonusBrickDestroyed(int score, int money)
        {
            EarnedMoney += money;
        }

        private void OnLevelFinished()
        {
            _wallet.AddMoney(EarnedMoney);
        }
    }
}