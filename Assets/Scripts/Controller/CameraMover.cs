using System.Collections;
using Base;
using Boosters;
using DG.Tweening;
using Field;
using Platform;
using UnityEngine;

namespace Controller
{
    public class CameraMover : MonoBehaviour
    {
        private const float TargetPosition = 0.5f;
        private const float TargetRotation = 2f;

        [SerializeField] private BoostersNotifier _notifier;
        [SerializeField] private PlatformMover _platform;
        [SerializeField] private PlatformMoveBorder _rightBorder;
        [SerializeField] private PlatformMoveBorder _leftBorder;
        [SerializeField] private float _smoothTime;
        [SerializeField] private float _targetShakePositionX;
        [SerializeField] private float _targetShakeRotationZ;
        [SerializeField] private float _randomess;
        [SerializeField] private int _vibrato;

        private Coroutine _shakingCoroutine;
        private Camera _camera;
        private Vector3 _shakeRotation;
        private Vector3 _shakePosition;
        private float _minPositionX;
        private float _maxPositionX;
        private float _minRotationZ;
        private float _maxRotationZ;
        private Vector3 _moveVelocity = Vector3.zero;
        private bool _isShaking = false;

        private void Awake()
        {
            _camera = Camera.main;
            _shakeRotation = new Vector3(0, 0, _targetShakeRotationZ);
            _shakePosition = new Vector3(_targetShakePositionX, 0, 0);
            _minPositionX = transform.position.x - TargetPosition;
            _maxPositionX = transform.position.x + TargetPosition;
            _minRotationZ = transform.rotation.z - TargetRotation;
            _maxRotationZ = transform.rotation.z + TargetRotation;
        }

        private void OnEnable()
        {
            _notifier.VisionFailureEnabled += OnVisionFailureEnabled;
            _notifier.Reseted += OnBoostersReseted;
        }

        private void LateUpdate()
        {
            if (_isShaking)
            {
                return;
            }

            Move();
        }

        private void OnDisable()
        {
            _notifier.VisionFailureEnabled -= OnVisionFailureEnabled;
            _notifier.Reseted -= OnBoostersReseted;
            _camera.DOKill();
        }

        private void Move()
        {
            float ratio = (_platform.transform.position.x - _leftBorder.transform.position.x) / (_rightBorder.transform.position.x - _leftBorder.transform.position.x);
            Vector3 moveTarget = new Vector3(Mathf.Lerp(_minPositionX, _maxPositionX, ratio), transform.position.y, transform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, moveTarget, ref _moveVelocity, _smoothTime);
            float rotationRatio = (transform.position.x - _minPositionX) / (_maxPositionX - _minPositionX);
            Vector3 rotation = transform.eulerAngles;
            rotation.z = Mathf.Lerp(_minRotationZ, _maxRotationZ, rotationRatio);
            transform.rotation = Quaternion.Euler(rotation);
        }

        private IEnumerator Shaking()
        {
            _isShaking = true;
            _camera.DOShakePosition(PlayerUtilities.BaseBoosterDurationTime, _shakePosition, _vibrato, _randomess, true, ShakeRandomnessMode.Harmonic);
            _camera.DOShakeRotation(PlayerUtilities.BaseBoosterDurationTime, _shakeRotation, _vibrato, _randomess, true, ShakeRandomnessMode.Harmonic);
            yield return PlayerUtilities.BaseBoostersDelay;
            _isShaking = false;
        }

        private void OnVisionFailureEnabled()
        {
            PlayerUtilities.CheckCoroutine(_shakingCoroutine, this);
            _camera.DOKill();
            _shakingCoroutine = StartCoroutine(Shaking());
        }

        private void OnBoostersReseted()
        {
            PlayerUtilities.CheckCoroutine(_shakingCoroutine, this);
            _camera.DOKill();
            _isShaking = false;
        }
    }
}