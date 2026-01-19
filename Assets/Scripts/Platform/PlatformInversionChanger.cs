using System.Collections;
using Base;
using Boosters;
using UnityEngine;

namespace Platform
{
    public class PlatformInversionChanger : MonoBehaviour
    {
        private const float DefaultInversionModifier = 1f;
        private const float ChangedInversionModifier = -1f;

        [SerializeField] private BoostersNotifier _notifier;

        private Coroutine _inversionCoroutine;

        public float InversionModifier { get; private set; }

        private void Awake()
        {
            InversionModifier = DefaultInversionModifier;
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
            InversionModifier = ChangedInversionModifier;
            yield return PlayerUtilities.BaseBoostersDelay;
            InversionModifier = DefaultInversionModifier;
        }

        private void OnInversionEnabled()
        {
            PlayerUtilities.CheckCoroutine(_inversionCoroutine, this);
            _inversionCoroutine = StartCoroutine(InversionEnabling());
        }

        private void OnBoostersRestarted()
        {
            PlayerUtilities.CheckCoroutine(_inversionCoroutine, this);
            InversionModifier = DefaultInversionModifier;
        }
    }
}