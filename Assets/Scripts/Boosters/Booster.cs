using Base;
using Data;
using Level;
using Platform;
using UnityEngine;

namespace Boosters
{
    [RequireComponent(typeof(Rigidbody))]
    public class Booster : PoolObject
    {
        private const float TimeToDestroy = 5f;

        [SerializeField] private BoostersEnum _type;
        [SerializeField] private LevelFinisher _levelFinisher;

        private float _timer;

        public BoostersEnum Type => _type;
        public Rigidbody Rigidbody { get; private set; }

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            _timer = 0;
            _levelFinisher.Finished += OnLevelFinished;
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer >= TimeToDestroy)
            {
                gameObject.SetActive(false);
            }
        }

        private void OnDisable()
        {
            _levelFinisher.Finished -= OnLevelFinished;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out BoostersCollector collector))
            {
                gameObject.SetActive(false);
            }
        }

        private void OnLevelFinished()
        {
            gameObject.SetActive(false);
        }
    }
}