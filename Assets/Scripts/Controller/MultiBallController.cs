using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ball;
using Base;
using Boosters;
using UnityEngine;

namespace Controller
{
    public class MultiBallController : MonoBehaviour
    {
        private const float BoundValue = 1f;

        [SerializeField] private BoostersNotifier _notifier;
        [SerializeField] private TempLevelController _levelController;
        [SerializeField] private List<BallMover> _balls;
        [SerializeField] private MainBallChanger _mainBallChanger;

        private Coroutine _switchingBallsStateCoroutine;

        public Action<int> ActiveBallsCountChanged;

        public List<BallMover> Balls => _balls;

        private void OnEnable()
        {
            SwitchOffBalls();
            _notifier.MultiballEnabled += OnMultiballEnabled;
            _notifier.Reseted += OnBoostersReseted;

            foreach (var ball in _balls)
            {
                ball.Destroyed += OnBallDestroyed;
            }
        }

        private void OnDisable()
        {
            _notifier.MultiballEnabled -= OnMultiballEnabled;
            _notifier.Reseted -= OnBoostersReseted;

            foreach (var ball in _balls)
            {
                ball.Destroyed -= OnBallDestroyed;
            }
        }

        private void ReleaseBalls()
        {
            if (_mainBallChanger.MainBall.gameObject.activeInHierarchy == false)
            {
                return;
            }

            foreach (var ball in _balls)
            {
                ball.transform.position = _mainBallChanger.MainBall.transform.position;
                ball.gameObject.SetActive(true);
                ball.transform.parent = null;
                Vector3 direction = GetDirection();

                if (ball.TryGetComponent(out Rigidbody rigidbody))
                {
                    rigidbody.velocity = Vector3.zero;
                    rigidbody.AddForce(direction, ForceMode.Impulse);
                }
            }

            InvokeCountChange();
        }

        private Vector3 GetDirection()
        {
            float directionX = UnityEngine.Random.Range(-BoundValue, BoundValue);
            float directionZ = UnityEngine.Random.Range(-BoundValue, BoundValue);
            Vector3 direction = new Vector3(directionX, 0, directionZ).normalized;
            return direction;
        }

        private int GetCurrentActiveBallsCount()
        {
            int count = 0;

            foreach (var ball in _balls)
            {
                if (ball.gameObject.activeInHierarchy)
                {
                    count++;
                }
            }

            return count;
        }

        private void SwitchOffBalls()
        {
            foreach (var ball in _balls)
            {
                if (ball != _mainBallChanger.MainBall)
                {
                    ball.gameObject.SetActive(false);
                }
            }
        }

        private void InvokeCountChange()
        {
            int count = GetCurrentActiveBallsCount();
            ActiveBallsCountChanged?.Invoke(count);
        }

        private IEnumerator SwitchingBallsState()
        {
            ReleaseBalls();
            yield return PlayerUtilities.BaseBoostersDelay;
            SwitchOffBalls();
        }

        private void OnBallDestroyed(BallMover ball)
        {
            ball.gameObject.SetActive(false);
            InvokeCountChange();
        }

        private void OnMultiballEnabled()
        {
            PlayerUtilities.CheckCoroutine(_switchingBallsStateCoroutine, this);
            _switchingBallsStateCoroutine = StartCoroutine(SwitchingBallsState());
        }

        private void OnBoostersReseted()
        {
            PlayerUtilities.CheckCoroutine(_switchingBallsStateCoroutine, this);
            SwitchOffBalls();
        }
    }
}