using System.Collections.Generic;
using System.Linq;
using Data;
using Field;
using Platform;
using UnityEngine;

namespace Level
{
    public class LevelScoreCounter : MonoBehaviour
    {
        

        [SerializeField] private LevelsEnum _currentLevel;
        [SerializeField] private List<FragmentsCollector> _fragmentsCollectors;
        [SerializeField] private BricksActionsInvoker _bricksInvoker;
        [SerializeField] private DestroyedBricksCounter _destroyedBricksCounter;

        private int _hightScore;

        public bool IsBeated { get; private set; }
        public int Score { get; private set; }
        public int FragmentsCollected { get; private set; }

        private void OnEnable()
        {
            foreach (var item in _fragmentsCollectors)
            {
                item.Picked += OnFragmentPicked;
            }

            _bricksInvoker.Smashed += OnBrickSmashed;
            _destroyedBricksCounter.AllTargetBricksDestroyed += OnAllTargetBricksDestroyed;
        }

        private void Start()
        {
            LoadLevelStatus();
        }

        private void OnDisable()
        {
            foreach (var item in _fragmentsCollectors)
            {
                item.Picked -= OnFragmentPicked;
            }

            _bricksInvoker.Smashed -= OnBrickSmashed;
            _destroyedBricksCounter.AllTargetBricksDestroyed -= OnAllTargetBricksDestroyed;
        }

        private void LoadLevelStatus()
        {
            LevelStates levelStates = SaveSystem.LoadLevelStates();
            LevelState level = levelStates.Levels.First(item => item.Number == _currentLevel);
            _hightScore = level.HightScore;
        }

        private void OnFragmentPicked(int value)
        {
            FragmentsCollected++;
            Score += value;
        }

        private void OnBrickSmashed(BaseBrick brick)
        {
            Score += brick.Value;
        }

        private void OnAllTargetBricksDestroyed()
        {
            IsBeated = Score > _hightScore;
        }
    }
}