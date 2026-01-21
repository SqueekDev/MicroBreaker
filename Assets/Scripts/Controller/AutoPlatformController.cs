using System.Collections;
using Base;
using Boosters;
using UnityEngine;

namespace Controller
{
    public class AutoPlatformController : MonoBehaviour
    {
        private const float DurationTime = 3f;

        [SerializeField] private BoostersNotifier _notifier;

        private Coroutine _flagChangingCoroutine;
        private WaitForSeconds _duration = new WaitForSeconds(DurationTime);

        public bool IsAutomatic { get; private set; } = false;

        private void OnEnable()
        {
            _notifier.AutoPlatformEnabled += OnAutoPlatformEnabled;
            _notifier.Reseted += OnBoostersReseted;
        }

        private void OnDisable()
        {
            _notifier.AutoPlatformEnabled -= OnAutoPlatformEnabled;
            _notifier.Reseted -= OnBoostersReseted;
        }

        private IEnumerator FlagChanging()
        {
            IsAutomatic = true;
            yield return _duration;
            IsAutomatic = false;
        }

        private void OnAutoPlatformEnabled()
        {
            PlayerUtilities.CheckCoroutine(_flagChangingCoroutine, this);
            _flagChangingCoroutine = StartCoroutine(FlagChanging());
        }

        private void OnBoostersReseted()
        {
            PlayerUtilities.CheckCoroutine(_flagChangingCoroutine, this);
            IsAutomatic = false;
        }
    }
}