using System;
using Ball;
using Platform;
using UnityEngine;

namespace Field
{
    public class Brick : MonoBehaviour
    {
        [SerializeField] private bool _isDestroyable;

        public Action Triggered;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.TryGetComponent(out AmplifyedBall ball))
            {
                if (_isDestroyable || ball.IsAmplifyed)
                {
                    Triggered?.Invoke();
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out LaserProjectile laser))
            {
                Triggered?.Invoke();
            }
        }
    }
}