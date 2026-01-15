using System.Collections;
using Base;
using Boosters;
using UnityEngine;

namespace Controller
{
    public class PlatformInversionChanger : MonoBehaviour
    {
        private const float DefaultInversionModifier = 1f;
        private const float ChangedInversionModifier = -1f;

        [SerializeField] private BoostersNotifier _notifier;

        private WaitForSeconds _delay;
        private Coroutine _inversionCoroutine;

        public float CurrentInversionModifier { get; private set; }

        private void Awake()
        {
            _delay = new WaitForSeconds(PlayerUtilities.BaseBoosterDuration);
            CurrentInversionModifier = DefaultInversionModifier;
        }

        private void OnEnable()
        {
            _notifier.InversionEnabled += OnInversionEnabled;
            _notifier.Reseted += OnBoostersRestarted;
        }

        private void OnDisable()
        {
            _notifier.InversionEnabled -= OnInversionEnabled;
            _notifier.Reseted -= OnBoostersRestarted;
        }

        private IEnumerator InversionEnabling()
        {
            CurrentInversionModifier = ChangedInversionModifier;
            yield return _delay;
            CurrentInversionModifier = DefaultInversionModifier;
        }

        private void OnInversionEnabled()
        {
            PlayerUtilities.CheckCoroutine(_inversionCoroutine, this);
            _inversionCoroutine = StartCoroutine(InversionEnabling());
        }

        private void OnBoostersRestarted()
        {
            PlayerUtilities.CheckCoroutine(_inversionCoroutine, this);
            CurrentInversionModifier = DefaultInversionModifier;
        }
    }
}