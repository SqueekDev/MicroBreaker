using System;
using Ball;
using Field;
using UnityEngine;

namespace Controller
{
    public class BallLauncher : MonoBehaviour
    {
        [SerializeField] private InputDetector _inputDetector;
        [SerializeField] private MultiBallController _multiBallController;
        [SerializeField] private TempLevelController _levelController;
        [SerializeField] private BallStartPoint _startPoint;

        private bool _isReleased = false;
        private BallMover _ball;

        public Action Released;
        public Action Restarted;

        private void OnEnable()
        {
            _inputDetector.Ended += OnTouchEnded;
            _levelController.Started += OnLevelStarted;
            _ball = _multiBallController.MainBall;
        }

        private void OnDisable()
        {
            _inputDetector.Ended -= OnTouchEnded;
            _levelController.Started -= OnLevelStarted;
        }

        private void OnTouchEnded()
        {
            if (_isReleased == false)
            {
                _ball.transform.parent = null;
                _isReleased = true;

                if (_ball.TryGetComponent(out Rigidbody rigidbody))
                {
                    rigidbody.isKinematic = false;
                    rigidbody.AddForce(Vector3.forward, ForceMode.Impulse);
                }

                Released?.Invoke();
            }
        }

        private void OnLevelStarted()
        {
            _isReleased = false;
            _ball.transform.SetParent(_startPoint.transform);
            _ball.transform.position = _startPoint.transform.position;

            if (_ball.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.isKinematic = true;
                rigidbody.velocity = Vector3.zero;
            }

            Restarted?.Invoke();
        }
    }
}