using System.Collections;
using System.Collections.Generic;
using Base;
using Boosters;
using Data;
using Platform;
using UnityEngine;

namespace Controller
{
    public class AutoPlatformEnabler : MonoBehaviour
    {
        private const float DurationTime = 3f;

        [SerializeField] private BoostersNotifier _notifier;
        [SerializeField] private List<BoostersCollector> _boostersCollectors;
        [SerializeField] private List<FragmentsCollector> _fragmentsCollectors;

        private Coroutine _flagChangingCoroutine;
        private WaitForSeconds _duration = new WaitForSeconds(DurationTime);

        public bool IsAutomatic { get; private set; } = false;

        private void OnEnable()
        {
            _notifier.Activated += OnBoosterActivated;
        }

        private void OnDisable()
        {
            _notifier.Activated -= OnBoosterActivated;
        }

        private void EnableAutoPlatform()
        {
            PlayerUtilities.CheckCoroutine(_flagChangingCoroutine, this);
            _flagChangingCoroutine = StartCoroutine(FlagChanging());
        }

        private void ResetBoosters()
        {
            PlayerUtilities.CheckCoroutine(_flagChangingCoroutine, this);
            IsAutomatic = false;
            SetCollectorsState(true);
        }

        private void SetCollectorsState(bool state)
        {
            foreach (var item in _boostersCollectors)
            {
                item.enabled = state;
            }

            foreach (var item in _fragmentsCollectors)
            {
                item.enabled = state;
            }
        }

        private IEnumerator FlagChanging()
        {
            IsAutomatic = true;
            SetCollectorsState(false);
            yield return _duration;
            IsAutomatic = false;
            SetCollectorsState(true);
        }

        private void OnBoosterActivated(BoostersEnum type)
        {
            switch (type)
            {
                case BoostersEnum.AutoPlatformEnabled:
                    EnableAutoPlatform();
                    break;
                case BoostersEnum.Reseted:
                    ResetBoosters();
                    break;
            }
        }
    }
}