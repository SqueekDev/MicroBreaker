using System;
using System.Collections.Generic;
using System.Linq;
using Base;
using Data;
using UnityEngine;

namespace MainMenu
{
    public class LevelStatesInitializer : MonoBehaviour
    {
        [SerializeField] private List<LevelView> _levels;

        public Action<LevelStates> Changed;

        private void Start()
        {
            CheckSave();
        }

        private void CheckSave()
        {
            if (PlayerPrefs.HasKey(PlayerPrefsKeys.LevelStates))
            {
                LoadState();
            }
            else
            {
                CreateState();
            }
        }

        private void CreateState()
        {
            List<LevelState> levels = new List<LevelState>();

            foreach (var item in _levels)
            {
                levels.Add(new LevelState(item.Number, false, false, 0));
            }

            LevelState firstLevel = levels.First(level => level.Number == LevelsEnum.Level_1);
            firstLevel.SetUnlockedStatus(true);
            LevelStates levelStates = new LevelStates(levels);
            SaveState(levelStates);
        }

        private void SaveState(LevelStates levelStates)
        {
            SaveSystem.SaveLevelStates(levelStates);
            Changed?.Invoke(levelStates);
        }

        private LevelStates LoadState()
        {
            LevelStates levels = SaveSystem.LoadLevelStates();
            Changed?.Invoke(levels);
            return levels;
        }
    }
}