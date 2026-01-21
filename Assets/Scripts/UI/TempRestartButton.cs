using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TempRestartButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        public event Action Clicked;

        protected Button Button => _button;

        protected virtual void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }

        protected virtual void OnButtonClick()
        {
            Clicked?.Invoke();
        }
    }
}