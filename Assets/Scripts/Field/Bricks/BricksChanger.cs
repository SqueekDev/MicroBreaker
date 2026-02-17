using System;
using System.Collections;
using Base;
using Boosters;
using Data;
using Level;
using UnityEngine;

namespace Field
{
    public class BricksChanger : PoolObject
    {
        private const float DirectionSpread = 1f;

        [SerializeField] private BoostersNotifier _notifier;
        [SerializeField] private BaseDestructableBrick _baseBrick;
        [SerializeField] private BaseDestructableBrick _zapBrick;
        [SerializeField] private BaseBrick _steelBrick;
        [SerializeField] private LevelStarter _levelController;
        [SerializeField] private float _kickForce = 10f;

        private Coroutine _changingCoroutine;
        private BaseBrick _activeBrick;
        private bool _isDestroyed;
        private bool _isReleased;

        public Action<BaseBrick> Destroyed;

        private void Awake()
        {
            _activeBrick = _baseBrick;
        }

        protected virtual void OnEnable()
        {
            _levelController.Started += OnLevelStarted;
            _notifier.Activated += OnBoosterActivated;
            _baseBrick.Triggered += OnBrickTriggered;
            _zapBrick.Triggered += OnBrickTriggered;
        }

        private void OnDisable()
        {
            _levelController.Started -= OnLevelStarted;
            _notifier.Activated -= OnBoosterActivated;
            _baseBrick.Triggered -= OnBrickTriggered;
            _zapBrick.Triggered -= OnBrickTriggered;
        }

        public void SetBricksKinematicState(bool state)
        {
            _baseBrick.Rigidbody.isKinematic = state;
            _zapBrick.Rigidbody.isKinematic = state;
            _steelBrick.Rigidbody.isKinematic = state;

            if (state == false)
            {
                if (_isReleased)
                {
                    return;
                }

                _isReleased = true;
                float directionX = UnityEngine.Random.Range(-DirectionSpread, DirectionSpread);
                float directionZ = UnityEngine.Random.Range(-DirectionSpread, DirectionSpread);
                Vector3 direction = new Vector3(directionX, 0, directionZ).normalized;
                _activeBrick.Rigidbody.AddForce(direction * _kickForce, ForceMode.Impulse);
            }
        }

        private void ChangeBrick(BaseBrick brick, Transform target)
        {
            if (_isDestroyed == false)
            {
                _activeBrick.gameObject.SetActive(false);
                brick.transform.position = target.position;
                brick.transform.rotation = target.rotation;
                brick.gameObject.SetActive(true);
                _activeBrick = brick;
            }
        }

        private void EnableZapBricks()
        {
            PlayerUtilities.CheckCoroutine(_changingCoroutine, this);
            _changingCoroutine = StartCoroutine(Changing(_zapBrick, _activeBrick.transform));
        }

        private void EnableSteelBricks()
        {
            PlayerUtilities.CheckCoroutine(_changingCoroutine, this);
            _changingCoroutine = StartCoroutine(Changing(_steelBrick, _activeBrick.transform));
        }

        private void ResetBoosters()
        {
            PlayerUtilities.CheckCoroutine(_changingCoroutine, this);
            ChangeBrick(_baseBrick, _activeBrick.transform);
        }

        private IEnumerator Changing(BaseBrick brick, Transform target)
        {
            ChangeBrick(brick, target);
            yield return PlayerUtilities.BaseBoostersDelay;
            ChangeBrick(_baseBrick, _activeBrick.transform);
        }

        protected void OnLevelStarted()
        {
            PlayerUtilities.CheckCoroutine(_changingCoroutine, this);
            _isDestroyed = false;
            ChangeBrick(_baseBrick, transform);
        }

        private void OnBrickTriggered()
        {
            PlayerUtilities.CheckCoroutine(_changingCoroutine, this);
            Destroyed?.Invoke(_activeBrick);
            _isDestroyed = true;
            gameObject.SetActive(false);
        }

        private void OnBoosterActivated(BoostersEnum type)
        {
            switch (type)
            {
                case BoostersEnum.ZapBricksEnabled:
                    EnableZapBricks();
                    break;
                case BoostersEnum.SteelBricksEnabled:
                    EnableSteelBricks();
                    break;
                case BoostersEnum.Reseted:
                    ResetBoosters();
                    break;
            }
        }
    }
}