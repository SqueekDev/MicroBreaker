using System.Collections;
using System.Collections.Generic;
using Base;
using Boosters;
using Field;
using UnityEngine;

namespace Controller
{
    public class MirrorController : MonoBehaviour
    {
        [SerializeField] private BoostersNotifier _notifier;
        [SerializeField] private List<MirrorBorder> _mirrors;
        [SerializeField] private DeadZone _deadZone;

        private Coroutine _enablingCoroutine;

        private void OnEnable()
        {
            _notifier.MirrorEnabled += OnMirrorEnabled;
            _notifier.Reseted += OnBoostersRestarted;
        }

        private void OnDisable()
        {
            _notifier.MirrorEnabled -= OnMirrorEnabled;
            _notifier.Reseted -= OnBoostersRestarted;
        }

        private void ChangeMirrorsState(bool isEnabled)
        {
            foreach (var mirror in _mirrors)
            {
                mirror.gameObject.SetActive(isEnabled);
            }

            _deadZone.gameObject.SetActive(!isEnabled);
        }

        private IEnumerator Enabling()
        {
            ChangeMirrorsState(true);
            yield return PlayerUtilities.BaseBoostersDelay;
            ChangeMirrorsState(false);
        }

        private void OnMirrorEnabled()
        {
            PlayerUtilities.CheckCoroutine(_enablingCoroutine, this);
            _enablingCoroutine = StartCoroutine(Enabling());
        }

        private void OnBoostersRestarted()
        {
            PlayerUtilities.CheckCoroutine(_enablingCoroutine, this);
            ChangeMirrorsState(false);
        }
    }
}