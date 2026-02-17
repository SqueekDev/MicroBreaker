using System;
using Base;
using Level;
using UnityEngine;

namespace Field
{
    public class DestroyedBricksCounter : MonoBehaviour
    {
        [SerializeField] private BricksActionsInvoker _invoker;
        [SerializeField] private LevelStarter _levelController;
        [SerializeField] private LevelFinisher _levelFinisher;

        private int _targetNumber;
        private int _targetCounter;

        public Action AllTargetBricksDestroyed;

        public int SmashedCounter { get; private set; }

        private void OnEnable()
        {
            _invoker.Smashed += OnBrickSmashed;
            _invoker.TargetDestroyed += OnTargetBrickDestroyed;
            _invoker.TargetNumberInitiated += OnTargetNumberInitiated;
            _levelController.Started += OnLevelStarted;
            _levelFinisher.Finished += OnLevelFinished;
        }

        private void OnDisable()
        {
            _invoker.Smashed -= OnBrickSmashed;
            _invoker.TargetDestroyed -= OnTargetBrickDestroyed;
            _invoker.TargetNumberInitiated -= OnTargetNumberInitiated;
            _levelController.Started -= OnLevelStarted;
            _levelFinisher.Finished -= OnLevelFinished;
        }

        private void OnLevelFinished()
        {
            int totalSmashedBricks = PlayerPrefs.GetInt(PlayerPrefsKeys.BricksSmashed, 0);
            totalSmashedBricks += SmashedCounter;
            PlayerPrefs.SetInt(PlayerPrefsKeys.BricksSmashed, totalSmashedBricks);
        }

        private void OnBrickSmashed(BaseBrick brick)
        {
            SmashedCounter++;
        }

        private void OnTargetBrickDestroyed()
        {
            _targetCounter++;

            if (_targetCounter >= _targetNumber)
            {
                AllTargetBricksDestroyed?.Invoke();
            }
        }

        private void OnTargetNumberInitiated(int number)
        {
            _targetNumber = number;
        }

        private void OnLevelStarted()
        {
            _targetCounter = 0;
            SmashedCounter = 0;
        }
    }
}