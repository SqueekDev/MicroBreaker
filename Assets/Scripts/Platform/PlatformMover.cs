using Ball;
using Controller;
using Field;
using UnityEngine;

namespace Platform
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public class PlatformMover : MonoBehaviour
    {
        [SerializeField] private PlatformMoveTargetSetter _targetSetter;
        [SerializeField] private AutoPlatformController _autoPlatformController;
        [SerializeField] private MainBallChanger _mainBallChanger;
        [SerializeField] private PlatformSpeedChanger _speedChanger;
        [SerializeField] private PlatformSizeChanger _sizeChanger;
        [SerializeField] private PlatformMoveBorder _topBorder;
        [SerializeField] private PlatformMoveBorder _bottomBorder;
        [SerializeField] private PlatformMoveBorder _leftBorder;
        [SerializeField] private PlatformMoveBorder _rightBorder;

        private BallMover _ball;
        private Rigidbody _rigidbody;
        private Collider _collider;
        private Vector3 _target;
        private float _verticalOffset;
        private float _horisontalOffset;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
            _verticalOffset = _collider.bounds.min.z - transform.position.z;
            OnSizeChanged();
        }

        private void OnEnable()
        {
            _sizeChanger.Changed += OnSizeChanged;
            _targetSetter.Changed += OnTargetChanged;
            _ball = _mainBallChanger.MainBall;
        }

        private void FixedUpdate()
        {
            if (_autoPlatformController.IsAutomatic)
            {
                _target = new Vector3(_ball.transform.position.x * _targetSetter.Inversion, transform.position.y, transform.position.z);
                CheckBorders();
            }

            Vector3 dirrection = _target - transform.position;
            Vector3 nextPosition = transform.position + dirrection * _speedChanger.BaseSpeed * _speedChanger.SpeedModifier * Time.fixedDeltaTime;
            _rigidbody.MovePosition(nextPosition);
        }

        private void OnDisable()
        {
            _sizeChanger.Changed -= OnSizeChanged;
            _targetSetter.Changed -= OnTargetChanged;
        }

        private void CheckBorders()
        {
            _target.x = Mathf.Clamp(_target.x, _leftBorder.transform.position.x + _horisontalOffset, _rightBorder.transform.position.x - _horisontalOffset);
            _target.z = Mathf.Clamp(_target.z, _bottomBorder.transform.position.z + _verticalOffset, _topBorder.transform.position.z - _verticalOffset);
        }

        private void OnTargetChanged(Vector3 currentTarget)
        {
            _target = currentTarget;
            CheckBorders();
        }

        private void OnSizeChanged()
        {
            _horisontalOffset = _collider.bounds.max.x - transform.position.x;
        }
    }
}