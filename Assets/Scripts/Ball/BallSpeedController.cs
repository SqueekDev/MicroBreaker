using Base;
using System;
using Boosters;
using UnityEngine;
using System.Collections;

namespace Ball
{
    public class BallSpeedController : MonoBehaviour
    {
        [SerializeField] private BoostersNotifier _notifier;
        [SerializeField] private float _startSpeed;
        [SerializeField] private float _maxSpeed;

        private Coroutine _speedChangingCoroutine;

        public Action Changed;

        public float CurrentBaseSpeed { get; private set; }
        public float MaxSpeed => _maxSpeed;

        private void Awake()
        {
            CurrentBaseSpeed = _startSpeed;
        }

        private void OnEnable()
        {
            _notifier.Activated += OnBoosterActivated;
        }

        private void OnDisable()
        {
            _notifier.Activated -= OnBoosterActivated;
        }

        private void ChangeSpeed(float targetSpeed)
        {
            CurrentBaseSpeed = targetSpeed;
            Changed?.Invoke();
        }

        private void StartChangingSpeed()
        {
            PlayerUtilities.CheckCoroutine(_speedChangingCoroutine, this);
            _speedChangingCoroutine = StartCoroutine(SpeedChanging());
        }

        private void RestartBoosters()
        {
            PlayerUtilities.CheckCoroutine(_speedChangingCoroutine, this);
            ChangeSpeed(_startSpeed);
        }

        private IEnumerator SpeedChanging()
        {
            ChangeSpeed(_maxSpeed);
            yield return PlayerUtilities.BaseBoostersDelay;
            ChangeSpeed(_startSpeed);
        }

        private void OnBoosterActivated(BoostersEnum type)
        {
            switch (type)
            {
                case BoostersEnum.BallSpeedIncreased:
                    StartChangingSpeed();
                    break;
                case BoostersEnum.Reseted:
                    RestartBoosters();
                    break;
            }
        }
    }
}