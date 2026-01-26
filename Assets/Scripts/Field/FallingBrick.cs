using Base;
using Controller;
using UnityEngine;

namespace Field
{
    public class FallingBrick : PoolObject
    {
        [SerializeField] private FallingBrickChanger _changer;

        private void OnEnable()
        {
            _changer.Destroyed += OnBrickDestroyed;
        }

        private void OnDisable()
        {
            _changer.Destroyed -= OnBrickDestroyed;
        }

        private void OnBrickDestroyed()
        {
            gameObject.SetActive(false);
        }
    }
}