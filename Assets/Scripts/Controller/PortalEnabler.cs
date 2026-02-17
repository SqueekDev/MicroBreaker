using System.Collections;
using System.Collections.Generic;
using Base;
using Boosters;
using Data;
using Field;
using UnityEngine;

namespace Controller
{
    public class PortalEnabler : MonoBehaviour
    {
        [SerializeField] private BoostersNotifier _notifier;
        [SerializeField] private List<PortalBorder> _portals;
        [SerializeField] private DeadZone _deadZone;

        private Coroutine _enablingCoroutine;

        private void OnEnable()
        {
            _notifier.Activated += OnBoosterActivated;
        }

        private void OnDisable()
        {
            _notifier.Activated -= OnBoosterActivated;
        }

        private void ChangePortalsState(bool isEnabled)
        {
            foreach (var mirror in _portals)
            {
                mirror.gameObject.SetActive(isEnabled);
            }

            _deadZone.gameObject.SetActive(!isEnabled);
        }

        private void EnablePortals()
        {
            PlayerUtilities.CheckCoroutine(_enablingCoroutine, this);
            _enablingCoroutine = StartCoroutine(Enabling());
        }

        private void ResetBoosters()
        {
            PlayerUtilities.CheckCoroutine(_enablingCoroutine, this);
            ChangePortalsState(false);
        }

        private IEnumerator Enabling()
        {
            ChangePortalsState(true);
            yield return PlayerUtilities.BaseBoostersDelay;
            ChangePortalsState(false);
        }

        private void OnBoosterActivated(BoostersEnum type)
        {
            switch (type)
            {
                case BoostersEnum.PortalEnabled:
                    EnablePortals();
                    break;
                case BoostersEnum.Reseted:
                    ResetBoosters();
                    break;
            }
        }
    }
}