using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Level
{
    public class LevelBrickMover : BaseActBehavior
    {
        [SerializeField] private MoveTargetsContainer _container;

        protected override void AppendTween(Sequence sequence, Vector3 target)
        {
            sequence.Append(transform.DOMove(target, ActingTime));
        }

        protected override List<Vector3> GetTargets()
        {
            List<Vector3> targets = new List<Vector3>();

            foreach (var targetPoint in _container.Targets)
            {
                Vector3 target = targetPoint.transform.position;
                targets.Add(target);
            }

            return targets;
        }
    }
}