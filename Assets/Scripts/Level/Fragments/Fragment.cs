using Base;
using Platform;
using UnityEngine;

namespace Level
{
    [RequireComponent(typeof(Rigidbody))]
    public class Fragment : PoolObject
    {
        [SerializeField] private int _score;
        [SerializeField] private int _money;

        public int Score => _score;
        public int Money => _money;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out FragmentsCollector collector))
            {
                gameObject.SetActive(false);
            }
        }
    }
}