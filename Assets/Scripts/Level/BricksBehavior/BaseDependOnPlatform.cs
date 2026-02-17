using Base;
using Platform;
using UnityEngine;

namespace Level
{
    public abstract class BaseDependOnPlatform : MonoBehaviour
    {
        [SerializeField] private PlatformMover _platform;
        [SerializeField] private float _maxValue;
        [SerializeField] private MoveBorder _rightBorder;

        private float _startPlatformPositionX;
        private float _maxPlatrofmPositionX;

        private void Awake()
        {
            _startPlatformPositionX = _platform.transform.position.x;
        }

        private void Update()
        {
            _maxPlatrofmPositionX = _rightBorder.transform.position.x - (_startPlatformPositionX + _platform.RightBorder - _platform.Center);
            float ratio = (_platform.transform.position.x - _startPlatformPositionX) / _maxPlatrofmPositionX;
            float targetValue = ratio * _maxValue;
            Act(targetValue);
        }

        protected abstract void Act(float targetValue);
    }
}