using Controller;
using System;
using System.Linq;
using UnityEngine;

namespace Ball
{
    public class MainBallChanger : MonoBehaviour
    {
        [SerializeField] private MultiBallController _multiBallController;

        public Action<BallMover> Changed;

        public BallMover MainBall { get; private set; }

        private void Awake()
        {
            MainBall = _multiBallController.Balls[0];
        }

        private void OnEnable()
        {
            MainBall.Destroyed += OnMainBallDestroyed;
        }

        private void OnDisable()
        {
            if (MainBall != null)
            {
                MainBall.Destroyed -= OnMainBallDestroyed;
            }
        }

        private void OnMainBallDestroyed(BallMover ball)
        {
            MainBall.Destroyed -= OnMainBallDestroyed;
            MainBall = _multiBallController.Balls.FirstOrDefault(b => b.gameObject.activeInHierarchy == true);

            if (MainBall == null)
            {
                MainBall = _multiBallController.Balls[0];
            }

            MainBall.Destroyed += OnMainBallDestroyed;
            Changed?.Invoke(MainBall);
        }
    }
}