using DG.Tweening;
using UnityEngine;

namespace Level
{
    [RequireComponent(typeof(Rigidbody))]
    public class FragmentMover : MonoBehaviour
    {
        private const float TimeToDestroy = 5f;
        private const float TargetZ = -50f;
        private const float MinTimeZ = 7f;
        private const float MaxTimeZ = 8f;
        private const float MinTimeX = 1f;
        private const float MaxTimeX = 1.3f;
        private const float MinSpreadX = -0.25f;
        private const float MaxSpreadX = 0.25f;
        private const float Divider = 2f;

        private float _timer;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            _timer = 0;
            Move();
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
            _rigidbody.DOKill();
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