using System.Collections.Generic;
using DG.Tweening;
using Field;
using UnityEngine;

namespace Level
{
    public class LevelCenterRotator : BaseActBehavior
    {
        [SerializeField] private RotateMode _rotateMode;
        [SerializeField] private List<Vector3> _rotationTargets;
        [SerializeField] private List<BricksChanger> _bricks;

        private int _bricksCounter;

        private void Awake()
        {
            _bricksCounter = _bricks.Count;
        }

        private void OnEnable()
        {
            foreach (var item in _bricks)
            {
                item.Destroyed += OnBrickDestroyed;
            }
        }

        protected override void OnDisable()
        {
            foreach (var item in _bricks)
            {
                item.Destroyed -= OnBrickDestroyed;
            }

            base.OnDisable();
        }

        protected override List<Vector3> GetTargets()
        {
            return _rotationTargets;
        }

        protected override void AppendTween(Sequence sequence, Vector3 target)
        {
            sequence.Append(transform.DORotate(target, ActingTime, _rotateMode));
        }

        private void OnBrickDestroyed(BaseBrick brick)
        {
            _bricksCounter--;

            if (_bricksCounter <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}