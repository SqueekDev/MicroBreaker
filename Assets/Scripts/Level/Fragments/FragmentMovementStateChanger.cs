using System.Collections;
using System.Collections.Generic;
using Ball;
using Base;
using Boosters;
using Data;
using UnityEngine;

namespace Level
{
    public class FragmentMovementStateChanger : MonoBehaviour
    {
        [SerializeField] private List<Attractor> _attractors;
        [SerializeField] private List<FragmentMover> _movers;
        [SerializeField] private BoostersNotifier _notifier;

        private Coroutine _attractionCoroutine;

        private void OnEnable()
        {
            _notifier.Activated += OnBoosterActivated;
        }

        private void OnDisable()
        {
            _notifier.Activated -= OnBoosterActivated;
        }

        private void SetAttractorsState(bool attracts)
        {
            foreach (var item in _attractors)
            {
                item.enabled = attracts;
            }

            foreach (var item in _movers)
            {
                item.enabled = !attracts;
            }
        }

        private IEnumerator EnablingAttraction()
        {
            SetAttractorsState(true);
            yield return PlayerUtilities.BaseBoostersDelay;
            SetAttractorsState(false);
        }

        private void OnBoosterActivated(BoostersEnum boostersEnum)
        {
            switch (boostersEnum)
            {
                case BoostersEnum.GravityEnabled:
                    PlayerUtilities.CheckCoroutine(_attractionCoroutine, this);
                    _attractionCoroutine = StartCoroutine(EnablingAttraction());
                    break;
                case BoostersEnum.Reseted:
                    PlayerUtilities.CheckCoroutine(_attractionCoroutine, this);
                    SetAttractorsState(false);
                    break;
            }
        }
    }
}