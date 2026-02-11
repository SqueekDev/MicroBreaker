using System.Collections;
using Base;
using Boosters;
using Data;
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
            _notifier.Activated += OnBoosterActivated;
        }

        private void OnDisable()
        {
            _notifier.Activated -= OnBoosterActivated;
        }

        private void EnablePowerPlatform()
        {
            PlayerUtilities.CheckCoroutine(_enablingCorotine, this);
            _enablingCorotine = StartCoroutine(Enabling());
        }

        private void ResetBoosters()
        {
            PlayerUtilities.CheckCoroutine(_enablingCorotine, this);
            IsEnabled = false;
        }

        private IEnumerator Enabling()
        {
            IsEnabled = true;
            yield return PlayerUtilities.BaseBoostersDelay;
            IsEnabled = false;
        }

        private void OnBoosterActivated(BoostersEnum type)
        {
            switch (type)
            {
                case BoostersEnum.PowerPlatformEnabled:
                    EnablePowerPlatform();
                    break;
                case BoostersEnum.Reseted:
                    ResetBoosters();
                    break;
            }
        }
    }
}