using Platform;
using UnityEngine;

namespace Ball
{
    [RequireComponent(typeof(Rigidbody))]
    public class Attractor : MonoBehaviour
    {
        [SerializeField] private PlatformMover _target;
        [SerializeField] private float _attractionForce;

        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            _rigidbody.velocity = Vector3.zero;
        }

        private void FixedUpdate()
        {
            if (_rigidbody.velocity.z <= 0)
            {
                Vector3 direction = (_target.transform.position - transform.position).normalized;
                _rigidbody.velocity = direction * _attractionForce;
            }
        }
    }
}