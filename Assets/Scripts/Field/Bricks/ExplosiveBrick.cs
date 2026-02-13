using System.Collections;
using Base;
using Ball;
using Platform;
using UnityEngine;

namespace Field
{
    public class ExplosiveBrick : BaseDestructableBrick
    {
        private const float DelayTime = 2f;

        [SerializeField] private float _explodeRadius;

        private bool _isLaunched;
        private Coroutine _waitingCoroutine;
        private WaitForSeconds _delay = new WaitForSeconds(DelayTime);

        private void OnEnable()
        {
            _isLaunched = false;
        }

        private void OnDisable()
        {
            PlayerUtilities.CheckCoroutine(_waitingCoroutine, this);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.TryGetComponent(out AmplifyedBall ball))
            {
                if (_isLaunched)
                {
                    return;
                }

                PlayerUtilities.CheckCoroutine(_waitingCoroutine, this);
                _waitingCoroutine = StartCoroutine(WaitToExplode());
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out LaserProjectile laser))
            {
                Explode();
                Triggered?.Invoke();
            }
        }

        private void Explode()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, _explodeRadius);

            foreach (var item in colliders)
            {
                if (item.TryGetComponent(out IDamageable damageable))
                {
                    damageable.TakeDamage();
                }
            }
        }

        private IEnumerator WaitToExplode()
        {
            _isLaunched = true;
            yield return _delay;
            Explode();
            Triggered?.Invoke();
        }
    }
}