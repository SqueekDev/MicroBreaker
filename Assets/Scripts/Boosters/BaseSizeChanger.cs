using UnityEngine;

namespace Boosters
{
    public class BaseSizeChanger : MonoBehaviour
    {
        private const float BoosterDuration = 5f;

        [SerializeField] private BoostersNotifier _notifier;
        [SerializeField] private Vector3 _increasedSizeMultiplier;
        [SerializeField] private Vector3 _decreasedSizeMultiplier;

        private Vector3 _startScale;
        private float _timer;

        protected BoostersNotifier Notifier => _notifier;

        private void Awake()
        {
            _startScale = transform.localScale;
        }

        protected virtual void OnEnable()
        {
            _notifier.Reseted += OnReseted;
        }

        private void Update()
        {
            if (_timer > 0)
            {
                _timer -= Time.deltaTime;
                return;
            }

            if (transform.localScale != _startScale)
            {
                transform.localScale = _startScale;
            }
        }

        protected virtual void OnDisable()
        {
            _notifier.Reseted -= OnReseted;
        }

        private void ChangeSize(Vector3 multiplier)
        {
            transform.localScale = new Vector3(_startScale.x * multiplier.x, _startScale.y * multiplier.y, _startScale.z * multiplier.z);
            _timer = BoosterDuration;
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
            _timer = 0;
        }
    }
}