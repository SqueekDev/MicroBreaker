using Base;
using Field;
using UnityEngine;

namespace Platform
{
    [RequireComponent(typeof(Rigidbody))]
    public class LaserProjectile : PoolObject
    {

        [SerializeField] private float _force = 30f;
        [SerializeField] private float _lifeTime = 2f;

        private float _timer;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            _timer = _lifeTime;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.AddForce(Vector3.forward * _force, ForceMode.Impulse);
        }

        private void Update()
        {
            if (_timer > 0)
            {
                _timer -= Time.deltaTime;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Brick brick) || other.TryGetComponent(out FieldBorder border))
            {
                Debug.Log("LaserTriggered");
                gameObject.SetActive(false);
            }
        }
    }
}