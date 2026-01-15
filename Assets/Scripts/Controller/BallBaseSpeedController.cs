using Base;
using Boosters;
using UnityEngine;

namespace Controller
{
    public class BallBaseSpeedController : MonoBehaviour
    {
        [SerializeField] private BoostersNotifier _notifier;
        [SerializeField] private float _startSpeed;
        [SerializeField] private float _maxSpeed;

        public float CurrentBaseSpeed { get; private set; }
        public float MaxSpeed => _maxSpeed;

        private float _timer;

        private void Awake()
        {
            CurrentBaseSpeed = _startSpeed;
        }

        private void OnEnable()
        {
            _notifier.BallSpeedIncreased += OnSpeedIncreased;
            _notifier.Reseted += OnBoostersRestarted;
        }

        private void Update()
        {
            if (_timer > 0)
            {
                _timer -= Time.deltaTime;
                return;
            }

            if (CurrentBaseSpeed != _startSpeed)
            {
                CurrentBaseSpeed = _startSpeed;
            }
        }

        private void OnDisable()
        {
            _notifier.BallSpeedIncreased -= OnSpeedIncreased;
            _notifier.Reseted -= OnBoostersRestarted;            
        }

        private void OnSpeedIncreased()
        {
            CurrentBaseSpeed = _maxSpeed;
            _timer = PlayerUtilities.BaseBoosterDuration;
        }

        private void OnBoostersRestarted()
        {
            CurrentBaseSpeed = _startSpeed;
            _timer = 0;
        }
    }
}