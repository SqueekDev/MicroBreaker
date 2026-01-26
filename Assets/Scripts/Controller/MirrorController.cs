using System.Collections;
using Base;
using Boosters;
using Platform;
using UnityEngine;

namespace Controller
{
    public class MirrorController : MonoBehaviour
    {
        [SerializeField] private BoostersNotifier _notifier;
        [SerializeField] private PlatformMover _platform;

        private Coroutine _enablingCoroutine;

        private void OnEnable()
        {
            _notifier.Activated += OnBoosterActivated;
        }

        private void OnDisable()
        {
            _notifier.Activated -= OnBoosterActivated;
        }

        private void EnableMirror()
        {
            PlayerUtilities.CheckCoroutine(_enablingCoroutine, this);
            _enablingCoroutine = StartCoroutine(Enabling());
        }

        private void ResetBoosters()
        {
            PlayerUtilities.CheckCoroutine(_enablingCoroutine, this);
            _platform.gameObject.SetActive(false);
        }

        private IEnumerator Enabling()
        {
            _platform.gameObject.SetActive(true);
            yield return PlayerUtilities.BaseBoostersDelay;
            _platform.gameObject.SetActive(false);
        }

        private void OnBoosterActivated(BoostersEnum type)
        {
            switch (type)
            {
                case BoostersEnum.MirrorEnabled:
                    EnableMirror();
                    break;
                case BoostersEnum.Reseted:
                    ResetBoosters();
                    break;
            }
        }
    }
}