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
            _notifier.GravityEnabled += OnGravityEnabled;
            _notifier.Reseted += OnBoostersReseted;
        }

        private void OnDisable()
        {
            _notifier.GravityEnabled -= OnGravityEnabled;
            _notifier.Reseted -= OnBoostersReseted;
        }

        private void SwitchState(bool isEnabled)
        {
            foreach (var attractor in _attractors)
            {
                attractor.enabled = isEnabled;
            }
        }

        private IEnumerator AtractorEnabling()
        {
            SwitchState(true);
            yield return PlayerUtilities.BaseBoostersDelay;
            SwitchState(false);
        }

        private void OnBoostersReseted()
        {
            PlayerUtilities.CheckCoroutine(_gravityCoroutine, this);
            SwitchState(false);
        }

        private void OnGravityEnabled()
        {
            PlayerUtilities.CheckCoroutine(_gravityCoroutine, this);
            _gravityCoroutine = StartCoroutine(AtractorEnabling());
        }
    }
}