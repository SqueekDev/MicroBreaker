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

        protected virtual void OnEnable()
        {
            _notifier.Reseted += OnReseted;
        }

        protected virtual void OnDisable()
        {
            _notifier.Reseted -= OnReseted;
        }

        protected virtual void ChangeSize(Vector3 multiplier)
        {
            foreach (var target in _targets)
            {
                target.transform.localScale = Vector3.Scale(_startScale, multiplier);
            }
        }

        protected void OnSizeIncreased()
        {
            ChangeSize(_increasedSizeMultiplier);
        }

        protected void OnSizeDecreased()
        {
            ChangeSize(_decreasedSizeMultiplier);
        }

        private void OnReseted()
        {
            ChangeSize(Vector3.one);
        }
    }
}