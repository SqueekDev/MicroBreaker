using System;
using System.Collections;
using Base;
using Boosters;
using Data;
using Field;
using Level;
using UnityEngine;

namespace Controller
{
    public class BricksChanger : PoolObject
    {
        [SerializeField] private BoostersNotifier _notifier;
        [SerializeField] private BaseDestructableBrick _baseBrick;
        [SerializeField] private BaseDestructableBrick _zapBrick;
        [SerializeField] private BaseBrick _steelBrick;
        [SerializeField] private LevelStarter _levelController;

        private Coroutine _changingCoroutine;
        private BaseBrick _activeBrick;
        private bool _isDestroyed = false;

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
            _activeBrick.gameObject.SetActive(false);
            _isDestroyed = true;
            Destroyed?.Invoke(_activeBrick);
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