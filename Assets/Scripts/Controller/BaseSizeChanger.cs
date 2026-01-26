using System.Collections.Generic;
using Base;
using Boosters;
using UnityEngine;

namespace Controller
{
    public class BaseSizeChanger : MonoBehaviour
    {
        [SerializeField] private BoostersNotifier _notifier;
        [SerializeField] private Vector3 _increasedSizeMultiplier;
        [SerializeField] private Vector3 _decreasedSizeMultiplier;
        [SerializeField] private List<Scaleable> _targets;

        private Vector3 _startScale;

        protected BoostersNotifier Notifier => _notifier;

        private void Awake()
        {
            if (_targets.Count > 0)
            {
                _startScale = _targets[0].transform.localScale;
            }
        }

        private void OnEnable()
        {
            _notifier.Activated += OnBoosterActivated;
        }

        private void OnDisable()
        {
            _notifier.Activated -= OnBoosterActivated;
        }

        protected virtual void ChangeSize(Vector3 multiplier)
        {
            foreach (var target in _targets)
            {
                target.transform.localScale = Vector3.Scale(_startScale, multiplier);
            }
        }

        protected void IncreaseSize()
        {
            ChangeSize(_increasedSizeMultiplier);
        }

        protected void DecreaseSize()
        {
            ChangeSize(_decreasedSizeMultiplier);
        }

        private void OnReseted()
        {
            ChangeSize(Vector3.one);
        }

        protected virtual void OnBoosterActivated(BoostersEnum type)
        {
            if (type == BoostersEnum.Reseted)
            {
                OnReseted();
            }
        }
    }
}