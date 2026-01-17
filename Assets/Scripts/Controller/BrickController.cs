using System.Collections;
using Base;
using Boosters;
using Field;
using UnityEngine;

namespace Controller
{
    public class BrickController : MonoBehaviour
    {
        [SerializeField] private BoostersNotifier _notifier;
        [SerializeField] private Brick _baseBrick;
        [SerializeField] private Brick _zapBrick;
        [SerializeField] private Brick _steelBrick;
        [SerializeField] private DeadZone _deadZone;

        private Coroutine _changingCoroutine;
        private Brick _activeBrick;
        private bool _isDestroyed = false;

        private void Awake()
        {
            _activeBrick = _baseBrick;
        }

        private void OnEnable()
        {
            _deadZone.Activated += OnDeadZoneActivated;
            _notifier.ZapBricksEnabled += OnZapBricksEnabled;
            _notifier.SteelBricksEnabled += OnSteelBricksEnabled;
            _notifier.Reseted += OnBoostersReseted;
        }

        private void OnDisable()
        {
            _deadZone.Activated -= OnDeadZoneActivated;
            _notifier.ZapBricksEnabled += OnZapBricksEnabled;
            _notifier.SteelBricksEnabled += OnSteelBricksEnabled;
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

        private IEnumerator Changing(Brick brick, Transform target)
        {
            ChangeBrick(brick, target);
            yield return PlayerUtilities.BaseBoostersDelay;
            ChangeBrick(_baseBrick, _activeBrick.transform);
        }

        private void OnDeadZoneActivated()
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
        }

        private void OnZapBricksEnabled()
        {
            PlayerUtilities.CheckCoroutine(_changingCoroutine, this);
            _changingCoroutine = StartCoroutine(Changing(_zapBrick, _activeBrick.transform));
        }

        private void OnSteelBricksEnabled()
        {
            PlayerUtilities.CheckCoroutine(_changingCoroutine, this);
            _changingCoroutine = StartCoroutine(Changing(_steelBrick, _activeBrick.transform));
        }

        private void OnBoostersReseted()
        {
            PlayerUtilities.CheckCoroutine(_changingCoroutine, this);
            ChangeBrick(_baseBrick, _activeBrick.transform);
        }
    }
}