using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Level
{
    public abstract class BaseActBehavior : MonoBehaviour
    {
        [SerializeField] private float _delayBeforeStartTime;
        [SerializeField] private float _actingTime;
        [SerializeField] private float _delayBeforeStepTime;
        [SerializeField] private float _delayAfterStepTime;
        [SerializeField] private float _delayAfterLoopTime;

        private Sequence _sequence;

        protected float ActingTime => _actingTime;

        private void Start()
        {
            BuildSequence();
        }

        protected virtual void OnDisable()
        {
            _sequence.Kill();
        }

        protected abstract List<Vector3> GetTargets();

        protected abstract void AppendTween(Sequence sequence, Vector3 target);

        private void BuildSequence()
        {
            _sequence = DOTween.Sequence();
            _sequence.SetDelay(_delayBeforeStartTime, false);
            List<Vector3> targets = GetTargets();

            foreach (var target in targets)
            {
                Sequence sequence = DOTween.Sequence();
                sequence.AppendInterval(_delayBeforeStepTime);
                AppendTween(sequence, target);
                sequence.AppendInterval(_delayAfterStepTime);
                _sequence.Append(sequence);
            }

            _sequence.AppendInterval(_delayAfterLoopTime);
            _sequence.SetLoops(-1, LoopType.Incremental);
        }
    }
}