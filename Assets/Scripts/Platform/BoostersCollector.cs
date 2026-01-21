using Controller;
using UnityEngine;

namespace Platform
{
    public class BoostersCollector : MonoBehaviour
    {
        [SerializeField] private AutoPlatformController _autoPlatformController;

        private void OnTriggerEnter(Collider other)
        {
            if (_autoPlatformController.IsAutomatic)
            {
                return;
            }
        }
    }
}