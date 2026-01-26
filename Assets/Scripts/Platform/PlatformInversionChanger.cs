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
            _notifier.Activated += OnBoosterActivated;
        }

        private void OnDisable()
        {
            _notifier.Activated -= OnBoosterActivated;
        }

        private void EnableInversion()
        {
            PlayerUtilities.CheckCoroutine(_inversionCoroutine, this);
            _inversionCoroutine = StartCoroutine(InversionEnabling());
        }

        private void ResetBoosters()
        {
            PlayerUtilities.CheckCoroutine(_inversionCoroutine, this);
            InversionModifier = DefaultInversionModifier;
        }

        private IEnumerator InversionEnabling()
        {
            InversionModifier = ChangedInversionModifier;
            yield return PlayerUtilities.BaseBoostersDelay;
            InversionModifier = DefaultInversionModifier;
        }

        private void OnBoosterActivated(BoostersEnum type)
        {
            switch (type)
            {
                case BoostersEnum.InversionEnabled:
                    EnableInversion();
                    break;
                case BoostersEnum.Reseted:
                    ResetBoosters();
                    break;
            }
        }
    }
}