using System.Collections.Generic;
using System.Linq;
using Base;
using Boosters;
using Data;
using UnityEngine;

namespace Field
{
    public class BonusBrickEnabler : MonoBehaviour
    {
        [SerializeField] private ObjectPooler _pooler;
        [SerializeField] private BoostersNotifier _notifier;
        [SerializeField] private BricksActionsInvoker _bricksActionInvoker;

        private void OnEnable()
        {
            _notifier.Activated += OnBoosterActivated;
        }

        private void OnDisable()
        {
            _notifier.Activated -= OnBoosterActivated;
        }

        private BonusBrick TryGetBonus()
        {
            if (_pooler.TryGetObject(out BonusBrick bonusBrick))
            {
                return bonusBrick;
            }
            else
            {
                return null;
            }
        }

        private BricksChanger TryGetActiveBrick()
        {
            List<BricksChanger> bricks = _bricksActionInvoker.TargetBricks.Where(brick => brick.gameObject.activeInHierarchy == true).ToList();

            if (bricks.Count > 0)
            {
                int index = Random.Range(0, bricks.Count);
                BricksChanger brick = bricks[index];
                return brick;
            }
            else
            {
                return null;
            }
        }

        private void OnBoosterActivated(BoostersEnum boosterEnum)
        {
            if (boosterEnum == BoostersEnum.BonusTarget)
            {
                BricksChanger brick = TryGetActiveBrick();

                if (brick == null)
                {
                    return;
                }

                BonusBrick bonusBrick = TryGetBonus();

                if (bonusBrick == null)
                {
                    return;
                }

                bonusBrick.transform.SetParent(brick.transform);
                bonusBrick.transform.position = brick.transform.position;
                bonusBrick.gameObject.SetActive(true);
            }
        }
    }
}