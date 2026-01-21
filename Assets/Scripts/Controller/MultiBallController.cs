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
        [SerializeField] private List<BallMover> _balls;

        private Coroutine _switchingBallsStateCoroutine;

        public Action<BallMover> MainBallChanged;
        public Action<int> ActiveBallsCountChanged;

        public BallMover MainBall { get; private set; }

        private void Awake()
        {
            MainBall = _balls.FirstOrDefault(b => b.gameObject.activeInHierarchy == true);
            SwitchOffBalls();
        }

        private void OnEnable()
        {
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

        private void TryChangeMainBall()
        {
            MainBall = _balls.FirstOrDefault(b => b.gameObject.activeInHierarchy == true);

            if (MainBall != null)
            {
                MainBallChanged?.Invoke(MainBall);
            }
        }

        private void ReleaseBalls()
        {
            foreach (var ball in _balls)
            {
                ball.transform.position = MainBall.transform.position;
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
                    Debug.Log("ACTIVE");
                }
            }

            return count;
        }

        private void SwitchOffBalls()
        {
            foreach (var ball in _balls)
            {
                if (ball != MainBall)
                {
                    ball.gameObject.SetActive(false);
                }
            }

            InvokeCountChange();
        }

        private void InvokeCountChange()
        {
            int count = GetCurrentActiveBallsCount();
            Debug.Log(count);
            //ActiveBallsCountChanged?.Invoke(count);
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

            if (ball == MainBall)
            {
                TryChangeMainBall();
            }

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