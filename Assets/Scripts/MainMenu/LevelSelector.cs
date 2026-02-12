using System;
using UnityEngine;

namespace MainMenu
{
    public class LevelSelector : MonoBehaviour
    {
        [SerializeField] private MainMenuInputDetector _inputDetector;
        [SerializeField] private LayerMask _levelLayerMask;
        [SerializeField] private UnlockedLevelView _unlockedView;
        [SerializeField] private LockedLevelView _lockedView;

        private Camera _camera;
        private LevelView _selectedLevel;
        private bool _isSelected = false;
        private float _rayDistance = Mathf.Infinity;

        public Action<Vector3> Selected;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void OnEnable()
        {
            _inputDetector.TouchBegan += OnTouchBegan;
            _inputDetector.TouchMoved += OnTouchMoved;
            _inputDetector.TouchEdned += OnTouchEnded;
        }

        private void OnDisable()
        {
            _inputDetector.TouchBegan -= OnTouchBegan;
            _inputDetector.TouchMoved -= OnTouchMoved;
        }

        private void OpenLevelView(bool isUnlocked)
        {
            if (isUnlocked)
            {
                _lockedView.gameObject.SetActive(false);
                _unlockedView.gameObject.SetActive(true);
                _unlockedView.Init(_selectedLevel);
            }
            else
            {
                _unlockedView.gameObject.SetActive(false);
                _lockedView.gameObject.SetActive(true);
                _lockedView.Init(_selectedLevel);
            }
        }

        private void OnTouchBegan(Vector3 position)
        {
            Ray ray = _camera.ScreenPointToRay(position);

            if (Physics.Raycast(ray, out RaycastHit hit, _rayDistance, _levelLayerMask))
            {
                if (hit.collider.TryGetComponent(out LevelView level))
                {
                    _isSelected = true;
                    _selectedLevel = level;
                }
            }
        }

        private void OnTouchMoved(Vector3 position)
        {
            _isSelected = false;
        }

        private void OnTouchEnded(Vector3 position)
        {
            if (_isSelected)
            {
                Selected?.Invoke(_selectedLevel.transform.position);
                OpenLevelView(_selectedLevel.IsUnlocked);
            }

            _isSelected = false;
        }
    }
}