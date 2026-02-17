using System.Collections;
using Base;
using UnityEngine;

namespace MainMenu
{
    public class MainMenuCameraMover : MonoBehaviour
    {
        private const float StepTime = 0.01f;

        [SerializeField] private MainMenuInputDetector _inputDetector;
        [SerializeField] private LevelSelector _levelSelector;
        [SerializeField] private LayerMask _inputLayerMask;
        [SerializeField] private MoveBorder _bottomLeftBorder;
        [SerializeField] private MoveBorder _topRightBorder;
        [SerializeField] private Vector3 _offset;

        private bool _isMovingToLevelTarget = false;
        private Camera _camera;
        private Vector3 _startTouchPosition;
        private Vector3 _targetTouchPosition;
        private Vector3 _targetPosition;
        private Coroutine _moveCoroutine;
        private float _rayDistance = Mathf.Infinity;
        private WaitForSeconds _step = new WaitForSeconds(StepTime);

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void OnEnable()
        {
            _inputDetector.TouchBegan += OnTouchBegan;
            _inputDetector.TouchMoved += OnTouchMoved;
            _levelSelector.Selected += OnLevelSelected;
        }

        private void OnDisable()
        {
            _inputDetector.TouchBegan -= OnTouchBegan;
            _inputDetector.TouchMoved -= OnTouchMoved;
            _levelSelector.Selected -= OnLevelSelected;
        }

        private void CheckBorders()
        {
            _targetPosition.x = Mathf.Clamp(_targetPosition.x, _bottomLeftBorder.transform.position.x, _topRightBorder.transform.position.x);
            _targetPosition.z = Mathf.Clamp(_targetPosition.z, _bottomLeftBorder.transform.position.z, _topRightBorder.transform.position.z);
        }

        private IEnumerator Moving(Vector3 position)
        {
            Vector3 targetPosition = position + _offset;
            float progress = 0f;
            _isMovingToLevelTarget = true;

            while (_camera.transform.position != targetPosition)
            {
                _camera.transform.position = Vector3.Lerp(_camera.transform.position, targetPosition, progress);
                progress += StepTime;
                yield return _step;
            }

            _isMovingToLevelTarget = false;
        }

        private void OnTouchBegan(Vector3 position)
        {
            Ray ray = _camera.ScreenPointToRay(position);

            if (Physics.Raycast(ray, out RaycastHit hit, _rayDistance, _inputLayerMask))
            {
                _startTouchPosition = hit.point;
            }
        }

        private void OnTouchMoved(Vector3 position)
        {
            if (_isMovingToLevelTarget)
            {
                return;
            }

            Ray ray = _camera.ScreenPointToRay(position);

            if (Physics.Raycast(ray, out RaycastHit hit, _rayDistance, _inputLayerMask))
            {
                _targetTouchPosition = hit.point;
                Vector3 direction = _startTouchPosition - _targetTouchPosition;
                _targetPosition = _camera.transform.position + direction;
                CheckBorders();
                _camera.transform.position = _targetPosition;
            }
        }

        private void OnLevelSelected(Vector3 position)
        {
            PlayerUtilities.CheckCoroutine(_moveCoroutine, this);
            _moveCoroutine = StartCoroutine(Moving(position));
        }
    }
}