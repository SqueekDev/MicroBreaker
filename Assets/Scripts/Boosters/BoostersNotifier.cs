using System;
using System.Collections;
using Base;
using Controller;
using UnityEngine;

namespace Boosters
{
    public class BoostersNotifier : MonoBehaviour
    {
        [SerializeField] private TempLevelController _levelController;
        private Coroutine _testCoroutine;

        public Action Reseted;
        public Action BallSizeIncreased;
        public Action BallSizeDecreased;
        public Action PlatformSizeIncreased;
        public Action PlatformSizeDecreased;
        public Action LaserEnabled;
        public Action MirrorEnabled;
        public Action ShieldEnabled;
        public Action PortalEnabled;
        public Action MultiballEnabled;
        public Action GravityEnabled;
        public Action ZapBricksEnabled;
        public Action BallSpeedIncreased;
        public Action InversionEnabled;
        public Action PlatformSpeedDecreased;
        public Action SteelBricksEnabled;
        public Action BricksFallEnabled;
        public Action VisionFailureEnabled;
        public Action PlatformFrosenEnabled;
        public Action PowerPlatformEnabled;
        public Action AutoPlatformEnabled;

        private void OnEnable()
        {
            _levelController.Ended += OnLevelEnded;
        }

        private void Start()
        {
            //PlayerUtilities.CheckCoroutine(_testCoroutine, this);
            //_testCoroutine = StartCoroutine(Test());
        }

        private void OnDisable()
        {
            _levelController.Ended += OnLevelEnded;
        }

        private IEnumerator Test()
        {
            yield return new WaitForSeconds(2f);
            MultiballEnabled?.Invoke();
            yield return new WaitForSeconds(2f);
            PortalEnabled?.Invoke();
        }

        private void OnLevelEnded()
        {
            Reseted?.Invoke();
        }
    }
}