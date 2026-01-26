using Base;
using Controller;
using UnityEngine;

namespace Field
{
    public class BricksPooler : ObjectPooler
    {
        [SerializeField] private TempLevelController _levelController;

        private void OnEnable()
        {
            _levelController.Started += OnLevelStarted;
        }

        private void OnDisable()
        {
            _levelController.Started += OnLevelStarted;
        }

        private void OnLevelStarted()
        {
            foreach (var brick in PooledObjects)
            {
                brick.gameObject.SetActive(false);
            }
        }
    }
}