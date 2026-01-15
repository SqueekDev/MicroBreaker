using System.Collections;
using Base;
using Boosters;
using UnityEngine;

namespace Controller
{
    public class PlatformSpeedChanger : MonoBehaviour
    {
        private const float DefaultSpeedModifier = 1f;
        private const float SlowdownModifier = 0.5f;
        private const float StopDuration = 2f;

        [SerializeField] private BoostersNotifier _notifier;
        [SerializeField] private float _startSpeed;

        private Coroutine _slowdownCoroutine;
        private Coroutine _freezeCoroutine;
        private WaitForSeconds _slowdownDelay;
        private WaitForSeconds _stopDelay;

        public float CurrentBaseSpeed { get; private set; }
        public float CurrentSpeedModifier { get; private set; }

        private void Awake()
        {
            CurrentBaseSpeed = _startSpeed;
            CurrentSpeedModifier = DefaultSpeedModifier;
            _slowdownDelay = new WaitForSeconds(PlayerUtilities.BaseBoosterDuration);
            _stopDelay = new WaitForSeconds(StopDuration);
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
            CurrentSpeedModifier = SlowdownModifier;
            yield return _slowdownDelay;
            CurrentSpeedModifier = DefaultSpeedModifier;
        }

        private IEnumerator Freeze()
        {
            CurrentBaseSpeed = 0;
            yield return _stopDelay;
            CurrentBaseSpeed = _startSpeed;
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
            CurrentBaseSpeed = _startSpeed;
            CurrentSpeedModifier = DefaultSpeedModifier;
        }
    }
}