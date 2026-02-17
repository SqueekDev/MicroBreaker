using System.Collections.Generic;
using Base;
using MainMenu;
using UnityEngine;

namespace ShopMenu
{
    public class InputHandlersToggler : MonoBehaviour
    {
        [SerializeField] private MainMenuCameraMover _cameraMover;
        [SerializeField] private LevelSelector _levelSelector;
        [SerializeField] private List<InvokingPanel> _panels;

        private int _openedPanelsCounter;

        private void Awake()
        {
            _openedPanelsCounter = _panels.Count;
        }

        private void OnEnable()
        {
            foreach (var item in _panels)
            {
                item.Opened += OnPanelOpened;
                item.Closed += OnPanelClosed;
            }
        }

        private void OnDisable()
        {
            foreach (var item in _panels)
            {
                item.Opened += OnPanelOpened;
                item.Closed += OnPanelClosed;
            }
        }

        private void SetHandlersState(bool state)
        {
            _cameraMover.enabled = state;
            _levelSelector.enabled = state;
        }

        private void OnPanelOpened()
        {
            _openedPanelsCounter++;
            SetHandlersState(false);
        }

        private void OnPanelClosed()
        {
            _openedPanelsCounter--;

            if (_openedPanelsCounter > 0)
            {
                return;
            }

            SetHandlersState(true);
        }
    }
}