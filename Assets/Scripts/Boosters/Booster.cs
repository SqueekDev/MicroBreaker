using Base;
using Data;
using Platform;
using UnityEngine;

namespace Boosters
{
    [RequireComponent(typeof(Rigidbody))]
    public class Booster : PoolObject
    {
        [SerializeField] private BoostersEnum _type;

        public BoostersEnum Type => _type;
        public Rigidbody Rigidbody { get; private set; }

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
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