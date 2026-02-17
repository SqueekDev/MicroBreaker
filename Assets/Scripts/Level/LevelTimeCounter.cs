using System;
using UnityEngine;

namespace Level
{
    public class LevelTimeCounter : MonoBehaviour
    {
        private const float TimeToLaser = 120f;

        [SerializeField] private LevelStarter _starter;
        [SerializeField] private LevelFinisher _finisher;

        private bool _isCounting = false;
        private bool _isInvoked;

        public Action MinutePassed;

        public float TimePassed { get; private set; }

        private void OnEnable()
        {
            _starter.Started += OnLevelStarted;
            _finisher.Finished += OnLevelFinished;
        }

        private void Update()
        {
            if (_isCounting)
            {
                TimePassed += Time.deltaTime;

                if (_isInvoked == false && TimePassed > TimeToLaser)
                {
                    MinutePassed?.Invoke();
                    _isInvoked = true;
                }
            }
        }

        private void OnDisable()
        {
            _starter.Started -= OnLevelStarted;
            _finisher.Finished -= OnLevelFinished;
        }

        private void OnLevelStarted()
        {
            TimePassed = 0;
            _isCounting = true;
        }

        private void OnLevelFinished()
        {
            _isCounting = false;
        }
    }
}