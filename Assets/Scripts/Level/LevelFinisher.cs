using System;
using Base;
using Controller;
using Field;
using UnityEngine;

namespace Level
{
    public class LevelFinisher : MonoBehaviour
    {
        [SerializeField] private GamePanel _losePanel;
        [SerializeField] private GamePanel _winPanel;
        [SerializeField] private MultiBallController _multiBallController;
        [SerializeField] private DestroyedBricksCounter _counter;

        public Action Finished;

        private void OnEnable()
        {
            _multiBallController.ActiveBallsCountChanged += OnBallsCountChanged;
            _counter.AllTargetBricksDestroyed += OnTargetAchieved;
        }

        private void OnDisable()
        {
            _multiBallController.ActiveBallsCountChanged -= OnBallsCountChanged;
            _counter.AllTargetBricksDestroyed -= OnTargetAchieved;
        }

        private void FinishLevel(GamePanel panel)
        {
            Finished?.Invoke();
            Time.timeScale = 0;
            panel.gameObject.SetActive(true);
        }

        private void OnTargetAchieved()
        {
            FinishLevel(_winPanel);
        }

        private void OnBallsCountChanged(int count)
        {
            if (count <= 0)
            {
                FinishLevel(_losePanel);
            }
        }
    }
}