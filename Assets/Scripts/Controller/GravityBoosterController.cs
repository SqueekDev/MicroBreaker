using System.Collections;
using System.Collections.Generic;
using Ball;
using Base;
using Boosters;
using UnityEngine;

namespace Controller
{
    public class GravityBoosterController : MonoBehaviour
    {
        [SerializeField] private BoostersNotifier _notifier;
        [SerializeField] private List<BallAttractor> _attractors;

        private Coroutine _gravityCoroutine;

        private void Awake()
        {
            SwitchState(false);
        }

        private void OnEnable()
        {
            _notifier.Activated += OnBoosterActivated;
        }

        private void OnDisable()
        {
            _notifier.Activated -= OnBoosterActivated;
        }

        private void SwitchState(bool isEnabled)
        {
            foreach (var attractor in _attractors)
            {
                attractor.enabled = isEnabled;
            }
        }

        private void ResetBoosters()
        {
            PlayerUtilities.CheckCoroutine(_gravityCoroutine, this);
            SwitchState(false);
        }

        private void EnableGravity()
        {
            PlayerUtilities.CheckCoroutine(_gravityCoroutine, this);
            _gravityCoroutine = StartCoroutine(AtractorEnabling());
        }

        private IEnumerator AtractorEnabling()
        {
            SwitchState(true);
            yield return PlayerUtilities.BaseBoostersDelay;
            SwitchState(false);
        }

        private void OnBoosterActivated(BoostersEnum type)
        {
            switch (type)
            {
                case BoostersEnum.GravityEnabled:
                    EnableGravity();
                    break;
                case BoostersEnum.Reseted:
                    ResetBoosters();
                    break;
            }
        }
    }
}