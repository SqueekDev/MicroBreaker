using Base;
using Data;
using Platform;
using UnityEngine;

namespace Boosters
{
    [RequireComponent(typeof(Rigidbody))]
    public class Booster : PoolObject
    {
        private const float TimeToDestroy = 5f;

        [SerializeField] private BoostersEnum _type;

        private float _timer;

        public BoostersEnum Type => _type;
        public Rigidbody Rigidbody { get; private set; }

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer >= TimeToDestroy)
            {
                gameObject.SetActive(false);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out BoostersCollector collector))
            {
                gameObject.SetActive(false);
            }
        }
    }
}