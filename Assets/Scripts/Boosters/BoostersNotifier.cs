using System;
using System.Collections;
using Base;
using UnityEngine;

namespace Boosters
{
    public class BoostersNotifier : MonoBehaviour
    {
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

        private void Start()
        {
            PlayerUtilities.CheckCoroutine(_testCoroutine, this);
            _testCoroutine = StartCoroutine(Test());
        }

        private IEnumerator Test()
        {
            MirrorEnabled?.Invoke();
            PlatformSizeIncreased?.Invoke();
            PlatformSpeedDecreased?.Invoke();
            BallSizeIncreased?.Invoke();
            BallSpeedIncreased?.Invoke();
            BallSizeDecreased?.Invoke();
            PlatformSizeDecreased?.Invoke();
            yield return PlayerUtilities.BaseBoostersDelay;
        }
    }
}