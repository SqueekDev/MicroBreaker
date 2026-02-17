using System;
using Level;
using UnityEngine;

namespace Platform
{
    public class FragmentsCollector : MonoBehaviour
    {
        public Action<Fragment> Picked;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Fragment franment))
            {
                Picked?.Invoke(franment);
            }
        }
    }
}