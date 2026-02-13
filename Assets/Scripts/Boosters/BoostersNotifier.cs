using System;
using System.Collections.Generic;
using Data;
using Level;
using Platform;
using UnityEngine;

namespace Boosters
{
    public class BoostersNotifier : MonoBehaviour
    {
        private const int RandomBoostersCount = 21;

        [SerializeField] private LevelStarter _levelController;
        [SerializeField] private LevelFinisher _levelFinisher;
        [SerializeField] private List<BoostersCollector> _collectors;
        
        public Action<BoostersEnum> Activated;

        private void OnEnable()
        {
            foreach (var collector in _collectors)
            {
                collector.Picked += OnBoosterPicked;
            }

            _levelFinisher.Finished += OnLevelEnded;
        }

        private void OnDisable()
        {
            foreach (var collector in _collectors)
            {
                collector.Picked -= OnBoosterPicked;
            }

            _levelFinisher.Finished -= OnLevelEnded;
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