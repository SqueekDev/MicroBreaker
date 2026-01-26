using System;
using Base;
using Ball;
using Platform;
using UnityEngine;

namespace Field
{
    public class Brick : MonoBehaviour
    {
        [SerializeField] private bool _isFortified;
        [SerializeField] private bool _isIndestructable;

        public Action Triggered;

        private void OnCollisionEnter(Collision collision)
        {
            if (_isIndestructable)
            {
                return;
            }

            if (collision.transform.TryGetComponent(out AmplifyedBall ball))
            {
                if (_isFortified == false || ball.IsAmplifyed)
                {
                    Triggered?.Invoke();
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_isIndestructable)
            {
                return;
            }

            if (other.TryGetComponent(out LaserProjectile laser))
            {
                Triggered?.Invoke();
            }
        }
    }
}