using System;
using System.Collections;
using Base;
using Boosters;
using Controller;
using UnityEngine;

namespace Platform
{
    public class PlatformSizeChanger : BaseSizeChanger
    {
        private Coroutine _invokingCoroutine;
        private WaitForFixedUpdate _delay = new WaitForFixedUpdate();

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