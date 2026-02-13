using System;
using System.Collections.Generic;
using Ball;
using Boosters;
using Data;
using Level;
using UnityEngine;

namespace Controller
{
    public class MultiBallController : MonoBehaviour
    {
        private const float BoundValue = 1f;

        [SerializeField] private BoostersNotifier _notifier;
        [SerializeField] private LevelStarter _levelController;
        [SerializeField] private List<BallMover> _balls;
        [SerializeField] private MainBallChanger _mainBallChanger;

        public Action<int> ActiveBallsCountChanged;

        public List<BallMover> Balls => _balls;

        private void OnEnable()
        {
            SwitchOffBalls();
            _notifier.Activated += OnBoosterActivated;

            foreach (var ball in _balls)
            {
                ball.Destroyed += OnBallDestroyed;
            }
        }

        private void OnDisable()
        {
            _notifier.Activated -= OnBoosterActivated;

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
                ball.Rigidbody.velocity = Vector3.zero;
                ball.Rigidbody.AddForce(direction, ForceMode.Impulse);
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

        private void OnBallDestroyed(BallMover ball)
        {
            ball.gameObject.SetActive(false);
            InvokeCountChange();
        }

        private void EnableMultiball()
        {
            ReleaseBalls();
        }

        private void ResetBoosters()
        {
            SwitchOffBalls();
        }

        private void OnBoosterActivated(BoostersEnum type)
        {
            switch (type)
            {
                case BoostersEnum.MultiballEnabled:
                    EnableMultiball();
                    break;
                case BoostersEnum.Reseted:
                    ResetBoosters();
                    break;
            }
        }
    }
}