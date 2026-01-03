using UnityEngine;

namespace Controller
{
    [RequireComponent(typeof(Rigidbody))]
    public class Ball : MonoBehaviour
    {
        [SerializeField] private BallBeater _ballBeater;
        [SerializeField] private float _startSpeed;

        private Rigidbody _rigidbody;
        private float _currentSpeed;
        private bool _isReleased = false;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _currentSpeed = _startSpeed;
        }

        private void OnEnable()
        {
            _ballBeater.Released += OnReleased;
            _ballBeater.Restarted += OnRestarted;
        }

        private void FixedUpdate()
        {
            if (_isReleased && _rigidbody.isKinematic == false)
            {
                _rigidbody.velocity = _rigidbody.velocity.normalized * _currentSpeed;
            }
        }

        private void OnDisable()
        {
            _ballBeater.Released -= OnReleased;
            _ballBeater.Restarted -= OnRestarted;
        }

        private void OnRestarted()
        {
            _isReleased = false;
            _rigidbody.isKinematic = true;
            _rigidbody.velocity = Vector3.zero;
        }

        private void OnReleased()
        {
            _rigidbody.isKinematic = false;
            _rigidbody.AddForce(Vector3.forward * _currentSpeed, ForceMode.Impulse);
            _isReleased = true;
        }
    }
}