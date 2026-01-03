using System.Collections;
using UnityEngine;

namespace Controller
{
    
    public class PlarformMover : MonoBehaviour
    {
        private const float DefaultSpeedModifier = 30f;
        private const float DefaultInversionModifier = 1f;

        [SerializeField] private InputDetector _inputDetector;
        [SerializeField] private DeadZone _deadZone;
        [SerializeField] private StartPlatformPosition _startPosition;
        [SerializeField] private PlatformBorder _horizontalBorder;
        [SerializeField] private PlatformBorder _verticalBorder;
        [SerializeField] private PlatformMoveBorder _topBorder;
        [SerializeField] private PlatformMoveBorder _bottomBorder;
        [SerializeField] private PlatformMoveBorder _leftBorder;
        [SerializeField] private PlatformMoveBorder _rightBorder;
        [SerializeField] private LayerMask _inputMask;
        [SerializeField] private float _zOffset;

        private float _rayDistance = Mathf.Infinity;
        private Rigidbody _rigidbody;
        private Camera _camera;
        private Vector3 _offset;
        private float _currentSpeedModifier;
        private float _currentInversionModifier;
        private Vector3 _currentTarget;
        private float _verticalOffset;
        private float _currentHorizontalOffset;

        private void Awake()
        {
            _camera = Camera.main;
            _rigidbody = GetComponent<Rigidbody>();
            _currentTarget = _startPosition.transform.position;
            _currentSpeedModifier = DefaultSpeedModifier;
            _currentInversionModifier = DefaultInversionModifier;
            _offset = new Vector3(0, 0, _zOffset);
            _verticalOffset = _verticalBorder.transform.position.z - transform.position.z;
            _currentHorizontalOffset = _horizontalBorder.transform.position.x - transform.position.x;
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
            Vector3 nextPosition = transform.position + dirrection * _currentSpeedModifier * _currentInversionModifier * Time.fixedDeltaTime;
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
                _currentTarget = hit.point + _offset;
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
            transform.position = _startPosition.transform.position;
        }
    }
}