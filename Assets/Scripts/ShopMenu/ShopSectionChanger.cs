using System;
using Base;
using Data;
using System.Collections.Generic;
using UnityEngine;

namespace ShopMenu
{
    public class ShopSectionChanger : MonoBehaviour
    {
        [SerializeField] private List<ShopSwitchButton> _buttons;
        [SerializeField] private SectionView _baseView;
        [SerializeField] private SectionIcon _baseIcon;
        [SerializeField] private ShopPanel _shopPanel;
        [SerializeField] private CloseShopButton _closeButton;
        [SerializeField] private List<SectionView> _sections;

        private SectionView _currentView;
        private SectionIcon _currentIcon;

        public Action<ShopButtonsEnum> Changed;

        private void OnEnable()
        {
            foreach (var item in _buttons)
            {
                item.Changed += OnSectionChanged;
            }

            _closeButton.Clicked += OnCloseButtonClicked;
        }

        private void Start()
        {
            foreach (var item in _sections)
            {
                item.gameObject.SetActive(false);
            }

            _shopPanel.gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            foreach (var item in _buttons)
            {
                item.Changed -= OnSectionChanged;
            }

            _closeButton.Clicked -= OnCloseButtonClicked;
        }

        private void SelectSection(SectionView view, SectionIcon icon)
        {
            _currentView = view;
            _currentIcon = icon;
        }

        private void ChangeSectionState(SectionView view, SectionIcon icon, bool state)
        {
            view.gameObject.SetActive(state);
            icon.gameObject.SetActive(state);
        }

        private void OnCloseButtonClicked()
        {
            if (_currentIcon != null)
            {
                _currentIcon.gameObject.SetActive(false);
            }
        }

        private void OnSectionChanged(SectionView view, SectionIcon icon, ShopButtonsEnum type)
        {
            _shopPanel.gameObject.SetActive(true);

            if (_currentView != null && _currentIcon != null)
            {
                ChangeSectionState(_currentView, _currentIcon, false);
            }

            SelectSection(view, icon);
            ChangeSectionState(_currentView, _currentIcon, true);
            Changed?.Invoke(type);
        }
    }
}