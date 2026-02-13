using System;
using Boosters;
using Data;
using UnityEngine;

namespace Platform
{
    public class BoostersCollector : MonoBehaviour
    {
        public Action<BoostersEnum> Picked;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Booster booster))
            {
                Picked?.Invoke(booster.Type);
            }
        }
    }
}