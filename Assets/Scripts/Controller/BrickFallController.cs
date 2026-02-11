using Boosters;
using Data;
using Field;
using UnityEngine;

namespace Controller
{
    public class BrickFallController : MonoBehaviour
    {
        [SerializeField] private BoostersNotifier _notifier;
        [SerializeField] private BrickFallArea _brickFallArea;
        [SerializeField] private float _xLimit;
        [SerializeField] private float _zLimit;
        [SerializeField] private int _minBricksNumber;
        [SerializeField] private int _maxBricksNumber;
        [SerializeField] private BricksPooler _pooler;

        private void OnEnable()
        {
            _notifier.Activated += OnBoosterActivated;
        }

        private void OnDisable()
        {
            _notifier.Activated -= OnBoosterActivated;
        }

        private Vector3 GetSpawnPosition()
        {
            float xOffset = Random.Range(-_xLimit, _xLimit);
            float zOffset = Random.Range(-_zLimit, _zLimit);
            Vector3 position = _brickFallArea.transform.position + new Vector3(xOffset, 0, zOffset);
            return position;
        }

        private FallingBrick TryGetBrick()
        {
            if (_pooler.TryGetObject(out FallingBrick brick))
            {
                return brick;
            }
            else
            {
                return null;
            }
        }

        private void EnableFallingBricks()
        {
            int bricksNubmer = Random.Range(_minBricksNumber, _maxBricksNumber);

            for (int i = 0; i < bricksNubmer; i++)
            {
                FallingBrick brick = TryGetBrick();

                if (brick != null)
                {
                    brick.transform.position = GetSpawnPosition();
                    brick.transform.rotation = Random.rotation;
                    brick.gameObject.SetActive(true);
                }
            }
        }

        private void OnBoosterActivated(BoostersEnum type)
        {
            if (type == BoostersEnum.BricksFallEnabled)
            {
                EnableFallingBricks();
            }
        }
    }
}