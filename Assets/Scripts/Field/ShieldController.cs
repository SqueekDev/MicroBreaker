using System.Collections;
using Base;
using Boosters;
using UnityEngine;

namespace Field
{
    public class ShieldController : MonoBehaviour
    {
        [SerializeField] private BoostersNotifier _notifier;
        [SerializeField] private FieldBorder _shield;

        private Coroutine _shieldCoroutine;

        private void Awake()
        {
            _shield.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _notifier.ShieldEnabled += OnShieldEnabled;
            _notifier.Reseted += OnBoostersRestarted;
        }

        private void OnDisable()
        {
            _notifier.ShieldEnabled -= OnShieldEnabled;
            _notifier.Reseted -= OnBoostersRestarted;
        }

        private IEnumerator ShiledControlling()
        {
            _shield.gameObject.SetActive(true);
            yield return PlayerUtilities.BaseBoostersDelay;
            _shield.gameObject.SetActive(false);
        }

        private void OnShieldEnabled()
        {
            PlayerUtilities.CheckCoroutine(_shieldCoroutine, this);
            _shieldCoroutine = StartCoroutine(ShiledControlling());
        }

        private void OnBoostersRestarted()
        {
            PlayerUtilities.CheckCoroutine(_shieldCoroutine, this);
            _shield.gameObject.SetActive(false);
        }
    }
}