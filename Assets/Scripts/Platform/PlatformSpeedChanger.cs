using System.Collections;
using Base;
using Boosters;
using UnityEngine;

namespace Platform
{
    public class PlatformSpeedChanger : MonoBehaviour
    {
        private const float DefaultSpeedModifier = 1f;
        private const float SlowdownModifier = 0.1f;
        private const float StopDuration = 2f;

        [SerializeField] private BoostersNotifier _notifier;
        [SerializeField] private float _startSpeed;

        private Coroutine _slowdownCoroutine;
        private Coroutine _freezeCoroutine;
        private WaitForSeconds _freezeDelay;

        public float BaseSpeed { get; private set; }
        public float SpeedModifier { get; private set; }

        private void Awake()
        {
            BaseSpeed = _startSpeed;
            SpeedModifier = DefaultSpeedModifier;
            _freezeDelay = new WaitForSeconds(StopDuration);
        }

        private void OnEnable()
        {
            _notifier.PlatformFrosenEnabled += OnPlatformFrosen;
            _notifier.PlatformSpeedDecreased += OnSpeedDecreased;
            _notifier.Reseted += OnBoostersRestarted;
        }

        private void OnDisable()
        {
            _notifier.PlatformFrosenEnabled -= OnPlatformFrosen;
            _notifier.PlatformSpeedDecreased -= OnSpeedDecreased;
            _notifier.Reseted -= OnBoostersRestarted;
        }

        private IEnumerator Slowdown()
        {
            SpeedModifier = SlowdownModifier;
            yield return PlayerUtilities.BaseBoostersDelay;
            SpeedModifier = DefaultSpeedModifier;
        }

        private IEnumerator Freeze()
        {
            BaseSpeed = 0;
            yield return _freezeDelay;
            BaseSpeed = _startSpeed;
        }

        private void OnPlatformFrosen()
        {
            PlayerUtilities.CheckCoroutine(_freezeCoroutine, this);
            _freezeCoroutine = StartCoroutine(Freeze());
        }

        private void OnSpeedDecreased()
        {
            PlayerUtilities.CheckCoroutine(_slowdownCoroutine, this);
            _slowdownCoroutine = StartCoroutine(Slowdown());
        }

        private void OnBoostersRestarted()
        {
            BaseSpeed = _startSpeed;
            SpeedModifier = DefaultSpeedModifier;
        }
    }
}