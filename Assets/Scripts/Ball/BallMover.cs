using System;
using Controller;
using Field;
using Platform;
using UnityEngine;

namespace Ball
{
    [RequireComponent(typeof(Rigidbody))]
    public class BallMover : MonoBehaviour
    {
        private const float FirstSpeedBorderModifier = 1.5f;
        private const float SecondSpeedBorderModifier = 2.5f;
        private const float SpeedModifier = 1.5f;
        private const float MaxBounceAngle = 30f;
        private const float MinContactDistanceRatio = 0.7f;

        [SerializeField] private BallLauncher _ballBeater;
        [SerializeField] private BallSpeedController _speedController;
        [SerializeField] private PowerPlatform _powerPlatformController;

        private Rigidbody _rigidbody;
        private float _currentSpeed;
        private bool _isReleased = false;

        public Action<BallMover> Destroyed;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            _ballBeater.Released += OnReleased;
            _ballBeater.Restarted += OnLevelRestarted;
            _speedController.Changed += OnSpeedChanged;
            _currentSpeed = _speedController.CurrentBaseSpeed;
            _isReleased = true;
        }

        private void FixedUpdate()
        {
            if (_isReleased && _rigidbody.isKinematic == false)
            {
                _rigidbody.velocity = _rigidbody.velocity.normalized * _currentSpeed;
            }
        }

        private void OnDisable()
        {
            _ballBeater.Released -= OnReleased;
            _ballBeater.Restarted -= OnLevelRestarted;
            _speedController.Changed -= OnSpeedChanged;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.TryGetComponent(out PlatformMover platform))
            {
                if (_powerPlatformController.IsEnabled)
                {
                    _currentSpeed = _speedController.MaxSpeed;
                }

                if (collision.relativeVelocity.z > _currentSpeed * FirstSpeedBorderModifier
                || Mathf.Abs(collision.relativeVelocity.x) > _speedController.CurrentBaseSpeed * FirstSpeedBorderModifier)
                {
                    ChangeBallDirection(collision);
                }
                else
                {
                    PlatformCornersCorrection(collision);
                }
            }
            else if (_currentSpeed > _speedController.CurrentBaseSpeed)
            {
                _currentSpeed = _speedController.CurrentBaseSpeed;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out DeadZone deadZone))
            {
                Destroyed?.Invoke(this);
            }
        }

        private void ChangeBallDirection(Collision collision)
        {
            if (collision.relativeVelocity.z > _currentSpeed * SecondSpeedBorderModifier && _currentSpeed < _speedController.MaxSpeed)
            {
                _currentSpeed = _speedController.CurrentBaseSpeed * SpeedModifier;
            }

            _rigidbody.velocity = collision.rigidbody.velocity.normalized;
        }

        private void PlatformCornersCorrection(Collision collision)
        {
            Collider platform = collision.collider;
            Vector3 direction = _rigidbody.velocity.normalized;
            Vector3 contactDistance = platform.bounds.center - transform.position;
            float contactDistanceRatio = Mathf.Abs(contactDistance.x / (platform.bounds.center.x - platform.bounds.min.x));

            if (contactDistanceRatio >= MinContactDistanceRatio)
            {
                float bounceAngle = (contactDistance.x / platform.bounds.size.x) * MaxBounceAngle;
                _rigidbody.velocity = Quaternion.AngleAxis(bounceAngle, Vector3.down) * direction;
            }
        }

        private void OnSpeedChanged()
        {
            _currentSpeed = _speedController.CurrentBaseSpeed;
        }

        private void OnLevelRestarted()
        {
            _isReleased = false;
        }

        private void OnReleased()
        {
            _isReleased = true;
        }
    }
}