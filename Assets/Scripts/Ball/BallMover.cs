using Controller;
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
        [SerializeField] private BallBaseSpeedController _speedController;

        private Rigidbody _rigidbody;
        private float _currentSpeed;
        private bool _isReleased = false;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            _ballBeater.Released += OnReleased;
            _ballBeater.Restarted += OnLevelRestarted;
        }

        private void Start()
        {
            _currentSpeed = _speedController.CurrentBaseSpeed;
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
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.TryGetComponent(out PlatformMover _))
            {
                if (collision.relativeVelocity.z > _currentSpeed * FirstSpeedBorderModifier
                || Mathf.Abs(collision.relativeVelocity.x) > _speedController.CurrentBaseSpeed * FirstSpeedBorderModifier)
                {
                    if (collision.relativeVelocity.z > _currentSpeed * SecondSpeedBorderModifier && _currentSpeed < _speedController.MaxSpeed)
                    {
                        _currentSpeed = _speedController.CurrentBaseSpeed * SpeedModifier;
                        Debug.Log(_currentSpeed);
                    }

                    _rigidbody.velocity = collision.rigidbody.velocity.normalized * _currentSpeed;
                }
                else
                {
                    Collider platform = collision.collider;
                    Vector3 direction = _rigidbody.velocity.normalized;
                    Vector3 contactDistance = platform.bounds.center - transform.position;
                    float contactDistanceRatio = Mathf.Abs(contactDistance.x / (platform.bounds.center.x - platform.bounds.min.x));

                    if (contactDistanceRatio >= MinContactDistanceRatio)
                    {
                        float bounceAngle = (contactDistance.x / platform.bounds.size.x) * MaxBounceAngle;
                        direction = Quaternion.AngleAxis(bounceAngle, Vector3.down) * direction;
                        _rigidbody.velocity = direction * _rigidbody.velocity.magnitude;
                    }
                }
            }
            else if (_currentSpeed > _speedController.CurrentBaseSpeed)
            {
                _currentSpeed = _speedController.CurrentBaseSpeed;
            }
        }

        private void OnLevelRestarted()
        {
            _isReleased = false;
            _rigidbody.isKinematic = true;
            _rigidbody.velocity = Vector3.zero;
        }

        private void OnReleased()
        {
            _rigidbody.isKinematic = false;
            _rigidbody.AddForce(Vector3.forward * _currentSpeed, ForceMode.Impulse);
            _isReleased = true;
        }
    }
}