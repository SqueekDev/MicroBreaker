using System.Collections;
using Base;
using Boosters;
using Data;
using UnityEngine;

namespace Field
{
    public class ShieldEnabler : MonoBehaviour
    {
        [SerializeField] private BoostersNotifier _notifier;
        [SerializeField] private FieldBorder _shield;

        private Coroutine _shieldCoroutine;

        private void Awake()
        {
            _shield.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _notifier.Activated += OnBoosterActivated;
        }

        private void OnDisable()
        {
            _notifier.Activated -= OnBoosterActivated;
        }

        private void EnableShield()
        {
            PlayerUtilities.CheckCoroutine(_shieldCoroutine, this);
            _shieldCoroutine = StartCoroutine(ShiledControlling());
        }

        private void ResetBoosters()
        {
            PlayerUtilities.CheckCoroutine(_shieldCoroutine, this);
            _shield.gameObject.SetActive(false);
        }

        private IEnumerator ShiledControlling()
        {
            _shield.gameObject.SetActive(true);
            yield return PlayerUtilities.BaseBoostersDelay;
            _shield.gameObject.SetActive(false);
        }

        private void OnBoosterActivated(BoostersEnum type)
        {
            switch (type)
            {
                case BoostersEnum.ShieldEnabled:
                    EnableShield();
                    break;
                case BoostersEnum.Reseted:
                    ResetBoosters();
                    break;
            }
        }
    }
}