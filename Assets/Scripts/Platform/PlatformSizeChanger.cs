using System;
using System.Collections;
using Base;
using Controller;
using Data;
using UnityEngine;

namespace Platform
{
    public class PlatformSizeChanger : BaseSizeChanger
    {
        private const float InvokingDelayTime = 0.05f;

        private Coroutine _invokingCoroutine;
        private WaitForSeconds _delay = new WaitForSeconds(InvokingDelayTime);

        public Action Changed;

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

        protected override void OnBoosterActivated(BoostersEnum type)
        {
            base.OnBoosterActivated(type);

            if (type == BoostersEnum.PlatformSizeIncreased)
            {
                IncreaseSize();
            }
            else if (type == BoostersEnum.PlatformSizeDecreased)
            {
                DecreaseSize();
            }
        }
    }
}