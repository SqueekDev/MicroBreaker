using System;
using Ball;
using Field;
using Level;
using UnityEngine;

namespace Controller
{
    public class BallLauncher : MonoBehaviour
    {
        [SerializeField] private InputDetector _inputDetector;
        [SerializeField] private MainBallChanger _mainBallChanger;
        [SerializeField] private LevelStarter _levelController;
        [SerializeField] private BallStartPoint _startPoint;

        private bool _isReleased = false;
        private BallMover _ball;

        public Action Released;
        public Action Restarted;

        private void OnEnable()
        {
            _inputDetector.Ended += OnTouchEnded;
            _levelController.Started += OnLevelStarted;
            _mainBallChanger.Changed += OnMainBallChanged;
        }

        private void OnDisable()
        {
            _inputDetector.Ended -= OnTouchEnded;
            _levelController.Started -= OnLevelStarted;
            _mainBallChanger.Changed -= OnMainBallChanged;
        }

        private void OnTouchEnded()
        {
            if (_isReleased == false)
            {
                _ball.transform.parent = null;
                _isReleased = true;
                _ball.Rigidbody.isKinematic = false;
                _ball.Rigidbody.AddForce(Vector3.forward, ForceMode.Impulse);
                Released?.Invoke();
            }
        }

        private void OnLevelStarted()
        {
            _isReleased = false;
            _ball = _mainBallChanger.MainBall;
            _ball.gameObject.SetActive(true);
            _ball.transform.SetParent(_startPoint.transform);
            _ball.transform.position = _startPoint.transform.position;
            _ball.Rigidbody.velocity = Vector3.zero;
            _ball.Rigidbody.isKinematic = true;
            Restarted?.Invoke();
        }

        private void OnMainBallChanged(BallMover ball)
        {
            _ball = ball;
        }
    }
}