using Platform;
using UnityEngine;

namespace Field
{
    public class BallStartPoint : MonoBehaviour
    {
        [SerializeField] private PlatformMover _platformMover;

        private Vector3 _offset;

        private void Awake()
        {
            _offset = transform.position - _platformMover.transform.position;
        }

        private void LateUpdate()
        {
            transform.position = _platformMover.transform.position + _offset;
        }
    }
}