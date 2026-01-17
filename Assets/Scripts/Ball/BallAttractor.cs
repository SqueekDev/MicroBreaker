using Platform;
using UnityEngine;

namespace Ball
{
    [RequireComponent(typeof(Rigidbody))]
    public class BallAttractor : MonoBehaviour
    {
        [SerializeField] private PlatformMover _target;
        [SerializeField] private float _attractionForce;

        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (_rigidbody.velocity.z < 0)
            {
                Vector3 direction = (transform.position - _target.transform.position).normalized;
                _rigidbody.AddForce(direction * _attractionForce);
            }
        }
    }
}