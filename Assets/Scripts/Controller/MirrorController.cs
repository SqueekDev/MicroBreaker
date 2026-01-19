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
            _notifier.MirrorEnabled += OnMirrorEnabled;
            _notifier.Reseted += OnBoostersReseted;
        }

        private void OnDisable()
        {
            _notifier.MirrorEnabled -= OnMirrorEnabled;
            _notifier.Reseted -= OnBoostersReseted;
        }

        private void OnMirrorEnabled()
        {
            PlayerUtilities.CheckCoroutine(_enablingCoroutine, this);
            _enablingCoroutine = StartCoroutine(Enabling());
        }

        private void OnBoostersReseted()
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
    }
}