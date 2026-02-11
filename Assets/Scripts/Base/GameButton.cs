using UnityEngine;
using UnityEngine.UI;

namespace Base
{
    public abstract class GameButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        public Button Button => _button;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }

        protected abstract void OnButtonClick();
    }
}