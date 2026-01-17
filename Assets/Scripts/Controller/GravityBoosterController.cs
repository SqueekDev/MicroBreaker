using System.Collections;
using Ball;
using Base;
using Boosters;
using UnityEngine;

namespace Controller
{
    public class GravityBoosterController : MonoBehaviour
    {
        [SerializeField] private BoostersNotifier _notifier;
        [SerializeField] private BallAttractor _attractor;

        private Coroutine _gravityCoroutine;

        private void Awake()
        {
            _attractor.enabled = false;
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

        private IEnumerator AtractorEnabling()
        {
            _attractor.enabled = true;
            yield return PlayerUtilities.BaseBoostersDelay;
            _attractor.enabled = false;
        }

        private void OnBoostersReseted()
        {
            PlayerUtilities.CheckCoroutine(_gravityCoroutine, this);
            _attractor.enabled = false;
        }

        private void OnGravityEnabled()
        {
            PlayerUtilities.CheckCoroutine(_gravityCoroutine, this);
            _gravityCoroutine = StartCoroutine(AtractorEnabling());
        }
    }
}