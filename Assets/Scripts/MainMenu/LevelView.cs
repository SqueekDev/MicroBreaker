using System.Linq;
using Data;
using UnityEngine;

namespace MainMenu
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField] private LevelsEnum _number;
        [SerializeField] private LevelStatesInitializer _levelStateInitializer;

        public LevelsEnum Number => _number;
        public bool IsUnlocked { get; private set; }
        public bool IsCompleted { get; private set; }
        public int HightScore { get; private set; }

        private void OnEnable()
        {
            _levelStateInitializer.Changed += OnLevelStateChanged;
        }

        private void OnDisable()
        {
            _levelStateInitializer.Changed -= OnLevelStateChanged;
        }

        private void OnLevelStateChanged(LevelStates levelStates)
        {
            LevelState state = levelStates.Levels.First(level => level.Number == _number);
            IsUnlocked = state.IsUnlocked;
            IsCompleted = state.IsCompleted;
            HightScore = state.HightScore;
        }
    }
}