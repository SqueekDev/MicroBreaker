using System;
using System.Collections;
using Base;
using Boosters;
using Field;
using UnityEngine;

namespace Controller
{
    public class BricksChanger : PoolObject
    {
        [SerializeField] private BoostersNotifier _notifier;
        [SerializeField] private Brick _baseBrick;
        [SerializeField] private Brick _zapBrick;
        [SerializeField] private Brick _steelBrick;
        [SerializeField] private TempLevelController _levelController;

        private Coroutine _changingCoroutine;
        private Brick _activeBrick;
        private bool _isDestroyed = false;

        public Action<Brick> Destroyed;

        private void Awake()
        {
            _activeBrick = _baseBrick;
        }

        protected virtual void OnEnable()
        {
            _levelController.Started += OnLevelStarted;
            _notifier.Activated += OnBoosterActivated;
        }

        private void OnDisable()
        {
            _levelController.Started -= OnLevelStarted;
            _notifier.Activated -= OnBoosterActivated;
        }

        private void DestroyActiveBrick()
        {
            _activeBrick.Triggered -= OnBrickTriggered;
            _activeBrick.gameObject.SetActive(false);
        }

        private void ChangeBrick(Brick brick, Transform target)
        {
            if (_isDestroyed == false)
            {
                DestroyActiveBrick();
                brick.transform.position = target.position;
                brick.transform.rotation = target.rotation;
                brick.gameObject.SetActive(true);
                brick.Triggered += OnBrickTriggered;
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

        private IEnumerator Changing(Brick brick, Transform target)
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
            DestroyActiveBrick();
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