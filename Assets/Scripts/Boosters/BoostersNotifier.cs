using System;
using System.Collections;
using System.Collections.Generic;
using Controller;
using Platform;
using UnityEngine;

namespace Boosters
{
    public class BoostersNotifier : MonoBehaviour
    {
        private const int RandomBoostersCount = 20;

        [SerializeField] private TempLevelController _levelController;
        [SerializeField] private List<BoostersCollector> _collectors;
        [SerializeField] private BoostersEnum _type;
        
        public Action<BoostersEnum> Activated;

        private void OnEnable()
        {
            foreach (var collector in _collectors)
            {
                collector.Picked += OnBoosterPicked;
            }

            _levelController.Ended += OnLevelEnded;
        }

        private void Start()
        {
            StartCoroutine(TestCoroutine());
        }

        private void OnDisable()
        {
            foreach (var collector in _collectors)
            {
                collector.Picked -= OnBoosterPicked;
            }

            _levelController.Ended -= OnLevelEnded;
        }

        private IEnumerator TestCoroutine()
        {
            yield return new WaitForSeconds(2f);
            Activated?.Invoke(_type);
        }

        private BoostersEnum GetRandomBooster()
        {
            int boosterNumber = UnityEngine.Random.Range(0, RandomBoostersCount);
            BoostersEnum randomType = (BoostersEnum)boosterNumber;
            return randomType;
        }

        private void OnBoosterPicked(BoostersEnum type)
        {
            if (type == BoostersEnum.Random)
            {
                type = GetRandomBooster();
            }

            Activated?.Invoke(type);
        }

        private void OnLevelEnded()
        {
            Activated?.Invoke(BoostersEnum.Reseted);
        }
    }
}