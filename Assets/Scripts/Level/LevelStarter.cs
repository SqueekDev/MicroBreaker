using System;
using System.Collections.Generic;
using Base;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level
{
    public class LevelStarter : MonoBehaviour
    {
        [SerializeField] private List<ActionButton> _restartButtons;

        public Action Started;

        private void OnEnable()
        {
            foreach (var item in _restartButtons)
            {
                item.Clicked += OnButtonClick;
            }
        }

        private void OnDisable()
        {
            foreach (var item in _restartButtons)
            {
                item.Clicked += OnButtonClick;
            }
        }

        private void Start()
        {
            Started?.Invoke();
            Time.timeScale = 1;
        }

        private void OnButtonClick()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}