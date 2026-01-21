using System;
using UI;
using UnityEngine;

namespace Controller
{
    public class TempLevelController : MonoBehaviour
    {
        [SerializeField] private TempRestartButton _restartButton;
        [SerializeField] private MultiBallController _multiBallController;

        public Action Started;
        public Action Ended;

        private void OnEnable()
        {
            _restartButton.Clicked += OnButtonClick;
            _multiBallController.ActiveBallsCountChanged += OnBallsCountChanged;
        }

        private void OnDisable()
        {
            _multiBallController.ActiveBallsCountChanged -= OnBallsCountChanged;
            _restartButton.Clicked += OnButtonClick;            
        }

        private void Start()
        {
            Started?.Invoke();
        }

        private void OnButtonClick()
        {
            _restartButton.gameObject.SetActive(false);
            Time.timeScale = 1;
            Started?.Invoke();
        }

        private void OnBallsCountChanged(int count)
        {
            if (count <= 0)
            {
                Ended?.Invoke();
                _restartButton.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
}