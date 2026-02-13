using Ball;
using Platform;
using UnityEngine;

namespace Field
{
    public class NormalDestructableBrick : BaseDestructableBrick
    {
        [SerializeField] private bool _isFortified;

        private void OnCollisionEnter(Collision collision)
        {
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
            if (other.TryGetComponent(out LaserProjectile laser))
            {
                Triggered?.Invoke();
            }
        }
    }
}