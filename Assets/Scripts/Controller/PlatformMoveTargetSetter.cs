using System;
using Field;
using Platform;
using UnityEngine;

namespace Controller
{
    public class PlatformMoveTargetSetter : MonoBehaviour
    {
        [SerializeField] private InputDetector _inputDetector;
        [SerializeField] private PlatformInversionChanger _inversionChanger;
        [SerializeField] private TempLevelController _levelController;
        [SerializeField] private StartPlatformPosition _startPosition;
        [SerializeField] private LayerMask _inputMask;
        [SerializeField] private float _zOffset;
        [SerializeField] private float _baseInversion;

        private float _rayDistance = Mathf.Infinity;
        private Camera _camera;
        private Vector3 _offset;
        private Vector3 _currentTarget;

        public Action<Vector3> Changed;

        public float Inversion => _baseInversion;

        private void Awake()
        {
            _camera = Camera.main;
            _offset = new Vector3(0, 0, _zOffset);
        }

        private void OnEnable()
        {
            _inputDetector.Detected += OnDetected;
            _inputDetector.Ended += OnTouchEnded;
            _levelController.Started += OnLevelStarted;
        }

        private void OnDisable()
        {
            _inputDetector.Detected -= OnDetected;
            _inputDetector.Ended -= OnTouchEnded;
            _levelController.Started -= OnLevelStarted;
        }

        private void OnDetected(Touch touch)
        {
            Ray ray = _camera.ScreenPointToRay(touch.position);

            if (Physics.Raycast(ray, out RaycastHit hit, _rayDistance, _inputMask))
            {
                _currentTarget = (hit.point + _offset) * _baseInversion * _inversionChanger.InversionModifier;
            }

            Changed?.Invoke(_currentTarget);
        }

        private void OnTouchEnded()
        {
            _currentTarget = new Vector3(transform.position.x, transform.position.y, _startPosition.transform.position.z);
            Changed?.Invoke(_currentTarget);
        }

        private void OnLevelStarted()
        {
            _currentTarget = _startPosition.transform.position;
            Changed?.Invoke(_currentTarget);
        }
    }
}