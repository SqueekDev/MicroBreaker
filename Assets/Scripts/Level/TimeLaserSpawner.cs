using System.Collections;
using Base;
using Boosters;
using Field;
using UnityEngine;

namespace Level
{
    public class TimeLaserSpawner : MonoBehaviour
    {
        private const float SpawnDelayTime = 20f;
        private const int NumberToSpawn = 5;

        [SerializeField] private ObjectPooler _pooler;
        [SerializeField] private TargetPoint _spawnPoint;
        [SerializeField] private float _force;
        [SerializeField] private LevelTimeCounter _counter;
        [SerializeField] private BricksActionsInvoker _invoker;

        private bool _timeIsPassed;
        private bool _isSpawning;
        private bool _numberIsAchieved;
        private int _targetNumber;
        private int _currentDestroyedNumber;
        private Coroutine _spawningCoroutine;
        private WaitForSeconds _spawningDelay = new WaitForSeconds(SpawnDelayTime);

        private void OnEnable()
        {
            _counter.MinutePassed += OnTimeToLaserPassed;
            _invoker.TargetNumberInitiated += OnTargetNumberInitiated;
            _invoker.TargetDestroyed += OnTargetBrickDestroyed;
        }

        private void OnDisable()
        {
            _counter.MinutePassed -= OnTimeToLaserPassed;
            _invoker.TargetNumberInitiated -= OnTargetNumberInitiated;
            _invoker.TargetDestroyed -= OnTargetBrickDestroyed;
            PlayerUtilities.CheckCoroutine(_spawningCoroutine, this);
        }

        private Booster TryGetBooster()
        {
            if (_pooler.TryGetObject(out Booster booster))
            {
                return booster;
            }
            else
            {
                return null;
            }
        }

        private void StartSpawn()
        {
            _isSpawning = true;
            PlayerUtilities.CheckCoroutine(_spawningCoroutine, this);
            _spawningCoroutine = StartCoroutine(Spawning());
        }

        private void Spawn()
        {
            Booster booster = TryGetBooster();

            if (booster != null)
            {
                booster.transform.position = _spawnPoint.transform.position;
                booster.gameObject.SetActive(true);
                booster.Rigidbody.velocity = Vector3.zero;
                booster.Rigidbody.AddForce(Vector3.back * _force, ForceMode.Impulse);
            }
        }

        private IEnumerator Spawning()
        {
            while (enabled)
            {
                Spawn();
                yield return _spawningDelay;
            }
        }

        private void OnTimeToLaserPassed()
        {
            if (_isSpawning)
            {
                return;
            }

            _timeIsPassed = true;

            if (_numberIsAchieved)
            {
                StartSpawn();
            }
        }

        private void OnTargetNumberInitiated(int count)
        {
            _targetNumber = count;
        }

        private void OnTargetBrickDestroyed()
        {
            if (_isSpawning)
            {
                return;
            }

            _currentDestroyedNumber++;

            if ((_targetNumber - _currentDestroyedNumber) < NumberToSpawn && _timeIsPassed)
            {
                _numberIsAchieved = true;
                StartSpawn();
            }
        }
    }
}