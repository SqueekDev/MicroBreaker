using Controller;
using Field;
using UnityEngine;

namespace Platform
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public class PlatformMover : MonoBehaviour
    {
        [SerializeField] private PlatformSpeedChanger _speedChanger;
        [SerializeField] private PlatformInversionChanger _inversionChanger;
        [SerializeField] private InputDetector _inputDetector;
        [SerializeField] private DeadZone _deadZone;
        [SerializeField] private StartPlatformPosition _startPosition;
        [SerializeField] private PlatformMoveBorder _topBorder;
        [SerializeField] private PlatformMoveBorder _bottomBorder;
        [SerializeField] private PlatformMoveBorder _leftBorder;
        [SerializeField] private PlatformMoveBorder _rightBorder;
        [SerializeField] private float _baseInversion;
        [SerializeField] private LayerMask _inputMask;
        [SerializeField] private float _zOffset;

        private float _rayDistance = Mathf.Infinity;
        private Rigidbody _rigidbody;
        private Collider _collider;
        private Camera _camera;
        private Vector3 _offset;
        private Vector3 _currentTarget;
        private float _verticalOffset;
        private float _currentHorizontalOffset;

        private float _currentSpeedModifier => _speedChanger.CurrentSpeedModifier;
        private float _speed => _speedChanger.CurrentBaseSpeed;
        private float _currentInversionModifier => _inversionChanger.CurrentInversionModifier;

        private void Awake()
        {
            _camera = Camera.main;
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
            _currentTarget = _startPosition.transform.position;
            _offset = new Vector3(0, 0, _zOffset);
            _verticalOffset = _collider.bounds.min.z - transform.position.z;
            _currentHorizontalOffset = _collider.bounds.max.x - transform.position.x;
        }

        private void OnEnable()
        {
            _inputDetector.Detected += OnDetected;
            _inputDetector.Ended += OnTouchEnded;
            _deadZone.Activated += OnLevelStarted;
        }

        private void FixedUpdate()
        {
            Vector3 dirrection = _currentTarget - transform.position;
            Vector3 nextPosition = transform.position + dirrection * _speed * _currentSpeedModifier * Time.fixedDeltaTime;
            _rigidbody.MovePosition(nextPosition);
        }

        private void OnDisable()
        {
            _inputDetector.Detected -= OnDetected;
            _inputDetector.Ended -= OnTouchEnded;
            _deadZone.Activated -= OnLevelStarted;
        }

        private void CheckBorders()
        {
            _currentTarget.x = Mathf.Clamp(_currentTarget.x, _leftBorder.transform.position.x + _currentHorizontalOffset, _rightBorder.transform.position.x - _currentHorizontalOffset);
            _currentTarget.z = Mathf.Clamp(_currentTarget.z, _bottomBorder.transform.position.z + _verticalOffset, _topBorder.transform.position.z - _verticalOffset);
        }

        private void OnDetected(Touch touch)
        {
            Ray ray = _camera.ScreenPointToRay(touch.position);

            if (Physics.Raycast(ray, out RaycastHit hit, _rayDistance, _inputMask))
            {
                _currentTarget = (hit.point + _offset) * _baseInversion * _currentInversionModifier;
                CheckBorders();
            }
        }

        private void OnTouchEnded()
        {
            _currentTarget = new Vector3(transform.position.x, transform.position.y, _startPosition.transform.position.z);
            CheckBorders();
        }

        private void OnLevelStarted()
        {
            _currentTarget = _startPosition.transform.position;
        }
    }
}