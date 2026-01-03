using System;
using UnityEngine;

namespace Controller
{
    public class DeadZone : MonoBehaviour
    {
        public Action Activated;

        private void Start()
        {
            Activated?.Invoke();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Ball ball))
            {
                Activated?.Invoke();
                Debug.Log("Activated");
            }
        }
    }
}