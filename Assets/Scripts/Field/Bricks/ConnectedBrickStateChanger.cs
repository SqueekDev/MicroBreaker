using System.Collections.Generic;
using Level;
using UnityEngine;

namespace Field
{
    public class ConnectedBrickStateChanger : MonoBehaviour
    {
        [SerializeField] private BricksChanger _parentBrick;
        [SerializeField] private List<BricksChanger> _connectedBricks;

        private void OnEnable()
        {
            _parentBrick.Destroyed += OnParentBrickDestroyed;
        }

        private void Start()
        {
            foreach (var item in _connectedBricks)
            {
                item.SetBricksKinematicState(true);
            }
        }

        private void OnDisable()
        {
            _parentBrick.Destroyed -= OnParentBrickDestroyed;
        }

        private void OnParentBrickDestroyed(BaseBrick brick)
        {
            foreach (var item in _connectedBricks)
            {
                if (item.gameObject.activeInHierarchy)
                {
                    item.transform.SetParent(null);
                    item.SetBricksKinematicState(false);

                    if (item.TryGetComponent(out LevelBrickMover brickMover))
                    {
                        brickMover.enabled = false;
                    }
                }
            }
        }
    }
}