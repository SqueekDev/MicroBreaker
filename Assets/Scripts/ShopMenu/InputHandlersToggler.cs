using MainMenu;
using UnityEngine;

namespace ShopMenu
{
    public class InputHandlersToggler : MonoBehaviour
    {
        [SerializeField] private MainMenuCameraMover _cameraMover;
        [SerializeField] private LevelSelector _levelSelector;
        [SerializeField] private ShopPanel _shopPanel;

        private void OnEnable()
        {
            _shopPanel.Opened += OnShopPanelOpened;
            _shopPanel.Closed += OnShopPanelClosed;
        }

        private void OnDisable()
        {
            _shopPanel.Opened -= OnShopPanelOpened;
            _shopPanel.Closed -= OnShopPanelClosed;
        }

        private void OnShopPanelOpened()
        {
            _cameraMover.enabled = false;
            _levelSelector.enabled = false;
        }

        private void OnShopPanelClosed()
        {
            _cameraMover.enabled = true;
            _levelSelector.enabled = true;
        }
    }
}