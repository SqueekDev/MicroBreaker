using Base;
using Field;
using UnityEngine;

namespace Level
{
    public class FragmentsSpawner : MonoBehaviour
    {
        private const int MaxChance = 100;
        private const int SpawnChance = 20;
        private const int MinSpawnCount = 2;
        private const int MaxSpawnCount = 6;
        private const float SpawnSpread = 1f;

        [SerializeField] private BricksActionsInvoker _invoker;
        [SerializeField] private ObjectPooler _pooler;

        public int SpawnedNumber { get; private set; }

        private void OnEnable()
        {
            _invoker.SpawableDestroyed += OnBrickDestroyed;
        }

        private void OnDisable()
        {
            _invoker.SpawableDestroyed -= OnBrickDestroyed;
        }

        private Fragment TryGetFragment()
        {
            if (_pooler.TryGetObject(out Fragment franment))
            {
                return franment;
            }
            else
            {
                return null;
            }
        }

        private void SpawnFragments(Vector3 position)
        {
            int spawnCount = Random.Range(MinSpawnCount, MaxSpawnCount);

            for (int i = 0; i < spawnCount; i++)
            {
                Fragment fragment = TryGetFragment();

                if (fragment == null)
                {
                    return;
                }

                Vector3 spawnPosition = GetSpawnPosition(position);
                fragment.transform.position = spawnPosition;
                fragment.transform.rotation = Quaternion.identity;
                fragment.gameObject.SetActive(true);
                SpawnedNumber++;
            }
        }

        private Vector3 GetSpawnPosition(Vector3 startPosition)
        {
            float spreadX = Random.Range(-SpawnSpread, SpawnSpread);
            float spreadZ = Random.Range(-SpawnSpread, SpawnSpread);
            Vector3 position = new Vector3(startPosition.x + spreadX, startPosition.y, startPosition.z + spreadZ);
            return position;
        }

        private void OnBrickDestroyed(BaseBrick brick)
        {
            if (Random.Range(0, MaxChance) < SpawnChance)
            {
                SpawnFragments(brick.transform.position);
            }
        }
    }
}