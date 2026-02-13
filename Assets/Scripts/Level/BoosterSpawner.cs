using System.Collections.Generic;
using Boosters;
using Field;
using UnityEngine;

namespace Level
{
    public class BoosterSpawner : MonoBehaviour
    {
        private const int MaxValue = 100;
        private const int SpawnValue = 25;
        private const int GuaranteedBossterSpawnCount = 4;

        [SerializeField] private LevelStarter _levelController;
        [SerializeField] private float _force;
        [SerializeField] private List<Booster> _boosters;
        [SerializeField] private BricksActionsInvoker _invoker;

        private int _counter = 0;

        private void OnEnable()
        {
            _invoker.SpawableDestroyed += OnBrickDestroyed;
            _levelController.Started += OnLevelStarted;
        }

        private void OnDisable()
        {
            _invoker.SpawableDestroyed -= OnBrickDestroyed;
            _levelController.Started -= OnLevelStarted;
        }

        private Booster TryGetBooster()
        {
            Booster booster = null;

            while (booster == null)
            {
                int index = Random.Range(0, _boosters.Count);

                if (_boosters[index].gameObject.activeInHierarchy == false)
                {
                    booster = _boosters[index];
                }
            }

            return booster;
        }

        private bool GetSpawnState()
        {
            bool isSpawning;

            if (_counter < GuaranteedBossterSpawnCount)
            {
                isSpawning = Random.Range(0, MaxValue) < SpawnValue;
                _counter++;
            }
            else
            {
                isSpawning = true;
                _counter = 0;
            }

            return isSpawning;
        }

        private void OnBrickDestroyed(BaseBrick brick)
        {
            bool isSpawning = GetSpawnState();

            if (isSpawning == false)
            {
                return;
            }

            Booster booster = TryGetBooster();

            if (booster != null)
            {
                booster.transform.position = brick.transform.position;
                booster.gameObject.SetActive(true);
                booster.Rigidbody.velocity = Vector3.zero;
                booster.Rigidbody.AddForce(Vector3.back * _force, ForceMode.Impulse);
            }
        }

        private void OnLevelStarted()
        {
            foreach (var booster in _boosters)
            {
                booster.gameObject.SetActive(false);
            }
        }
    }
}