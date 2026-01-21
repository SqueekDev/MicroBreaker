using System.Collections;
using Base;
using Boosters;
using UnityEngine;

namespace Controller
{
    public class PowerPlatform : MonoBehaviour
    {
        [SerializeField] private BoostersNotifier _notifier;

        private Coroutine _enablingCorotine;

        public bool IsEnabled { get; private set; } = false;

        private void OnEnable()
        {
            _notifier.PowerPlatformEnabled += OnPowerPlatformEnabled;
            _notifier.Reseted += OnBoostersReseted;
        }

        private void OnDisable()
        {
            _notifier.PowerPlatformEnabled -= OnPowerPlatformEnabled;
            _notifier.Reseted -= OnBoostersReseted;
        }

        private IEnumerator Enabling()
        {
            IsEnabled = true;
            yield return PlayerUtilities.BaseBoostersDelay;
            IsEnabled = false;
        }

        private void OnPowerPlatformEnabled()
        {
            PlayerUtilities.CheckCoroutine(_enablingCorotine, this);
            _enablingCorotine = StartCoroutine(Enabling());
        }

        private void OnBoostersReseted()
        {
            PlayerUtilities.CheckCoroutine(_enablingCorotine, this);
            IsEnabled = false;
        }
    }
}