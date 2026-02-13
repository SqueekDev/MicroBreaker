using System.Collections;
using System.Collections.Generic;
using Base;
using DG.Tweening;
using Platform;
using UnityEngine;

namespace Level
{
    [RequireComponent(typeof(Rigidbody))]
    public class Fragment : PoolObject
    {
        private const float TimeToDestroy = 5f;
        private const float TargetZ = -50f;
        private const float MinTimeZ = 7f;
        private const float MaxTimeZ = 8f;
        private const float MinTimeX = 0.5f;
        private const float MaxTimeX = 0.8f;
        private const float MinSpreadX = -0.5f;
        private const float MaxSpreadX = 0.5f;
        private const float Divider = 2f;

        [SerializeField] private int _value;

        private Rigidbody _rigidbody;
        private float _timer;

        public int Value => _value;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            Move();
        }

        private void OnDisable()
        {
            _rigidbody.DOKill();
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
            if (other.TryGetComponent(out FragmentsCollector collector))
            {
                gameObject.SetActive(false);
            }
        }

        public void Move()
        {
            float targetZ = transform.position.z + TargetZ;
            float spreadX = Random.Range(MinSpreadX, MaxSpreadX);
            float timeZ = Random.Range(MinTimeZ, MaxTimeZ);
            float timeX = Random.Range(MinTimeX, MaxTimeX);
            transform.position -= new Vector3(spreadX / Divider, 0, 0);
            _rigidbody.DOMoveZ(targetZ, timeZ);
            _rigidbody.DOMoveX(spreadX, timeX).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
        }
    }
}