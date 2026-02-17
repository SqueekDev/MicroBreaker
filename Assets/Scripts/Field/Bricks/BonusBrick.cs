using System;
using System.Collections;
using Ball;
using Base;
using Boosters;
using Data;
using UnityEngine;

namespace Field
{
    public class BonusBrick : PoolObject
    {
        [SerializeField] private ObjectPooler _pooler;
        [SerializeField] private BoostersNotifier _notifier;
        [SerializeField] private Collider _collider;
        [SerializeField] private int _score;
        [SerializeField] private int _money;

        private Coroutine _disablingCoroutine;

        public Action<int, int> Destroyed;

        public int Score => _score;
        public int Money => _money;

        private void OnEnable()
        {
            _notifier.Activated += OnBoosterActivated;
            PlayerUtilities.CheckCoroutine(_disablingCoroutine, this);
            _disablingCoroutine = StartCoroutine(Disabling());
        }

        private void OnDisable()
        {
            _notifier.Activated -= OnBoosterActivated;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out BallMover ball))
            {
                PlayerUtilities.CheckCoroutine(_disablingCoroutine, this);
                Destroyed?.Invoke(Score, Money);
                gameObject.SetActive(false);
            }
        }

        private IEnumerator Disabling()
        {
            yield return PlayerUtilities.BaseBoostersDelay;
            gameObject.SetActive(false);
        }

        private void OnBoosterActivated(BoostersEnum boostersEnum)
        {
            switch (boostersEnum)
            {
                case BoostersEnum.Reseted:
                    PlayerUtilities.CheckCoroutine(_disablingCoroutine, this);
                    gameObject.SetActive(false);
                    break;
                case BoostersEnum.ZapBricksEnabled:
                    _collider.isTrigger = true;
                    break;
                case BoostersEnum.SteelBricksEnabled:
                    _collider.isTrigger = false;
                    break;
            }
        }
    }
}