using System.Collections.Generic;
using System.Linq;
using Data;
using Field;
using UnityEngine;

namespace Level
{
    public class LevelStateChanger : MonoBehaviour
    {
        [SerializeField] private LevelsEnum _currentLevelNumber;
        [SerializeField] private List<LevelsEnum> _nextLevels;
        [SerializeField] private LevelScoreCounter _scoreCounter;
        [SerializeField] private DestroyedBricksCounter _bricksCounter;
        [SerializeField] private LevelFinisher _levelFinisher;

        private LevelStates _levelStates;
        private LevelState _currentLevel;
        private List<LevelState> _states = new List<LevelState>();

        public int HightScore { get; private set; }

        private void Awake()
        {
            LoadLevelStatus();
        }

        private void OnEnable()
        {
            _levelFinisher.Finished += OnLevelFinished;
            _bricksCounter.AllTargetBricksDestroyed += OnAllTargetBricksDestroyed;
        }

        private void OnDisable()
        {
            _levelFinisher.Finished -= OnLevelFinished;
            _bricksCounter.AllTargetBricksDestroyed -= OnAllTargetBricksDestroyed;
        }

        private void UnlockConnectedLevels()
        {
            if (_nextLevels.Count <= 0)
            {
                return;
            }

            foreach (var item in _nextLevels)
            {
                LevelState levelState = _states.First(level => level.Number == item);
                levelState.SetUnlockedStatus(true);
            }
        }

        private void LoadLevelStatus()
        {
            LevelStates levelStates = SaveSystem.LoadLevelStates();
            _states = levelStates.Levels;
            _currentLevel = _states.First(state => state.Number == _currentLevelNumber);
            HightScore = _currentLevel.HightScore;
        }

        private void SaveLevelState()
        {
            LevelStates levelStates = new LevelStates(_states);
            SaveSystem.SaveLevelStates(levelStates);
        }

        private void OnLevelFinished()
        {
            if (_scoreCounter.Score > HightScore)
            {
                HightScore = _scoreCounter.Score;
                _currentLevel.SetHightScore(HightScore);
                SaveLevelState();
            }
        }

        private void OnAllTargetBricksDestroyed()
        {
            if (_currentLevel.IsCompleted == false)
            {
                _currentLevel.SetCompletedStatus(true);
                UnlockConnectedLevels();
                SaveLevelState();
            }
        }
    }
}