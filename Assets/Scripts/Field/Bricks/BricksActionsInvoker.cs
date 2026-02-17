using System;
using System.Collections.Generic;
using UnityEngine;

namespace Field
{
    public class BricksActionsInvoker : MonoBehaviour
    {
        [SerializeField] private List<BricksChanger> _targetBricks;
        [SerializeField] private List<BricksChanger> _fieldBricks;
        [SerializeField] private List<BricksChanger> _fallingBricks;

        public Action<BaseBrick> SpawableDestroyed;
        public Action<BaseBrick> Smashed;
        public Action TargetDestroyed;
        public Action<int> TargetNumberInitiated;

        public List<BricksChanger> TargetBricks => _targetBricks;

        private void OnEnable()
        {
            foreach (var item in _fieldBricks)
            {
                item.Destroyed += OnSpawnableBrickDestroyed;
                item.Destroyed += OnBrickSmashed;
            }

            foreach (var item in _fallingBricks)
            {
                item.Destroyed += OnBrickSmashed;
            }

            foreach (var item in _targetBricks)
            {
                item.Destroyed += OnTargetBrickDestroyed;
            }
        }

        private void Start()
        {
            TargetNumberInitiated?.Invoke(_targetBricks.Count);
        }

        private void OnDisable()
        {
            foreach (var item in _fieldBricks)
            {
                item.Destroyed -= OnSpawnableBrickDestroyed;
                item.Destroyed -= OnBrickSmashed;
            }

            foreach (var item in _fallingBricks)
            {
                item.Destroyed -= OnBrickSmashed;
            }

            foreach (var item in _targetBricks)
            {
                item.Destroyed -= OnTargetBrickDestroyed;
            }
        }

        private void OnSpawnableBrickDestroyed(BaseBrick baseBrick)
        {
            SpawableDestroyed?.Invoke(baseBrick);
        }

        private void OnBrickSmashed(BaseBrick brick)
        {
            Smashed?.Invoke(brick);
        }

        private void OnTargetBrickDestroyed(BaseBrick brick)
        {
            TargetDestroyed?.Invoke();
        }
    }
}