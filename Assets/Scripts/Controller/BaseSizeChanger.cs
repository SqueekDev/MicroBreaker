using Boosters;
using UnityEngine;

namespace Controller
{
    public class BaseSizeChanger : MonoBehaviour
    {
        [SerializeField] private BoostersNotifier _notifier;
        [SerializeField] private Vector3 _increasedSizeMultiplier;
        [SerializeField] private Vector3 _decreasedSizeMultiplier;

        private Vector3 _startScale;

        protected BoostersNotifier Notifier => _notifier;

        private void Awake()
        {
            _startScale = transform.localScale;
        }

        protected virtual void OnEnable()
        {
            _notifier.Reseted += OnReseted;
        }

        protected virtual void OnDisable()
        {
            _notifier.Reseted -= OnReseted;
        }

        private void ChangeSize(Vector3 multiplier)
        {
            transform.localScale = new Vector3(_startScale.x * multiplier.x, _startScale.y * multiplier.y, _startScale.z * multiplier.z);
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
            transform.localScale = _startScale;
        }
    }
}