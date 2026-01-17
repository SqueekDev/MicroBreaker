using System.Collections;
using Base;
using Controller;
using Field;
using UnityEngine;

namespace Ball
{
    public class AmplifyedBall : MonoBehaviour
    {
        private const float CountdownDelay = 0.05f;

        [SerializeField] private int _amplifyingCount;
        [SerializeField] private float _startAmplifyingTime;

        private Coroutine _amplifyingCoroutine;
        private WaitForSeconds _delay = new WaitForSeconds(CountdownDelay);
        private int _counter = 0;
        private float _currentAmplifyingTime;

        public bool IsAmplifyed { get; private set; } = false;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.TryGetComponent(out BallLauncher launcher))
            {
                _counter++;

                if (_counter >= _amplifyingCount)
                {
                    PlayerUtilities.CheckCoroutine(_amplifyingCoroutine, this);
                    _amplifyingCoroutine = StartCoroutine(Amplifying());
                }
            }
            else if (collision.transform.TryGetComponent(out Brick brick))
            {
                _currentAmplifyingTime = CountdownDelay;
            }
        }

        private IEnumerator Amplifying()
        {
            _counter = 0;
            _currentAmplifyingTime = _startAmplifyingTime;
            IsAmplifyed = true;

            while (_currentAmplifyingTime > 0)
            {
                _currentAmplifyingTime -= CountdownDelay;
                yield return _delay;
            }

            IsAmplifyed = false;
        }
    }
}