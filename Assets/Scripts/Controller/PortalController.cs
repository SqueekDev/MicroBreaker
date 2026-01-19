using System.Collections;
using System.Collections.Generic;
using Base;
using Boosters;
using Field;
using UnityEngine;

namespace Controller
{
    public class PortalController : MonoBehaviour
    {
        [SerializeField] private BoostersNotifier _notifier;
        [SerializeField] private List<PortalBorder> _portals;
        [SerializeField] private DeadZone _deadZone;

        private Coroutine _enablingCoroutine;

        private void OnEnable()
        {
            _notifier.PortalEnabled += OnPortalEnabled;
            _notifier.Reseted += OnBoostersRestarted;
        }

        private void OnDisable()
        {
            _notifier.PortalEnabled -= OnPortalEnabled;
            _notifier.Reseted -= OnBoostersRestarted;
        }

        private void ChangePortalsState(bool isEnabled)
        {
            foreach (var mirror in _portals)
            {
                mirror.gameObject.SetActive(isEnabled);
            }

            _deadZone.gameObject.SetActive(!isEnabled);
        }

        private IEnumerator Enabling()
        {
            ChangePortalsState(true);
            yield return PlayerUtilities.BaseBoostersDelay;
            ChangePortalsState(false);
        }

        private void OnPortalEnabled()
        {
            PlayerUtilities.CheckCoroutine(_enablingCoroutine, this);
            _enablingCoroutine = StartCoroutine(Enabling());
        }

        private void OnBoostersRestarted()
        {
            PlayerUtilities.CheckCoroutine(_enablingCoroutine, this);
            ChangePortalsState(false);
        }
    }
}