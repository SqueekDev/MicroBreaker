using System;
using UnityEngine;

namespace Controller
{
    public class BallBeater : MonoBehaviour
    {
        [SerializeField] private InputDetector _inputDetector;
        [SerializeField] private DeadZone _deadZone;
        [SerializeField] private Ball _ball;
        [SerializeField] private BallStartPoint _startPoint;

        private bool _isReleased = false;

        public Action Released;
        public Action Restarted;

        private void OnEnable()
        {
            _inputDetector.Ended += OnTouchEnded;
            _deadZone.Activated += OnLevelStarted;
        }

        private void OnDisable()
        {
            _inputDetector.Ended -= OnTouchEnded;
            _deadZone.Activated -= OnLevelStarted;
        }

        private void OnTouchEnded()
        {
            if (_isReleased == false)
            {
                _ball.transform.parent = null;
                _isReleased = true;
                Released?.Invoke();
            }
        }

        private void OnLevelStarted()
        {
            _isReleased = false;
            _ball.transform.SetParent(_startPoint.transform);
            _ball.transform.position = _startPoint.transform.position;
            Restarted?.Invoke();
        }
    }
}