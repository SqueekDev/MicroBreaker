using Ball;
using Platform;
using UnityEngine;

namespace Field
{
    public class NormalDestructableBrick : BaseDestructableBrick
    {
        [SerializeField] private bool _isFortified;
        [SerializeField] private int _dectructCounter = 0;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.TryGetComponent(out AmplifyedBall ball))
            {
                _dectructCounter--;

                if (_dectructCounter >= 0)
                {
                    return;
                }

                if (_isFortified == false || ball.IsAmplifyed)
                {
                    TakeDamage();
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out LaserProjectile laser))
            {
                TakeDamage();
            }
        }
    }
}