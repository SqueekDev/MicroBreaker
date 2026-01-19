using System;
using System.Collections;
using Base;
using Controller;
using UnityEngine;

namespace Platform
{
    public class PlatformSizeChanger : BaseSizeChanger
    {
        private Coroutine _invokingCoroutine;
        private WaitForFixedUpdate _delay = new WaitForFixedUpdate();

        public Action Changed;

        protected override void OnEnable()
        {
            Notifier.PlatformSizeIncreased += OnSizeIncreased;
            Notifier.PlatformSizeDecreased += OnSizeDecreased;
            base.OnEnable();
        }

        protected override void OnDisable()
        {
            Notifier.PlatformSizeIncreased -= OnSizeIncreased;
            Notifier.PlatformSizeDecreased -= OnSizeDecreased;
            base.OnDisable();
        }

        protected override void ChangeSize(Vector3 multiplier)
        {
            base.ChangeSize(multiplier);
            PlayerUtilities.CheckCoroutine(_invokingCoroutine, this);
            _invokingCoroutine = StartCoroutine(Invoking());
        }

        private IEnumerator Invoking()
        {
            yield return _delay;
            Changed?.Invoke();
        }
    }
}