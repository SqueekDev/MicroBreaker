using System;
using Boosters;
using Controller;
using UnityEngine;

namespace Platform
{
    public class BoostersCollector : MonoBehaviour
    {
        [SerializeField] private AutoPlatformController _autoPlatformController;

        public Action<BoostersEnum> Picked;

        private void OnTriggerEnter(Collider other)
        {
            if (_autoPlatformController.IsAutomatic)
            {
                return;
            }

            if (other.TryGetComponent(out Booster booster))
            {
                Picked?.Invoke(booster.Type);
            }
        }
    }
}