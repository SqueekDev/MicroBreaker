using UnityEngine;

namespace Level
{
    public class DependedOnPlatformRotator : BaseDependOnPlatform
    {
        protected override void Act(float targetValue)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, -targetValue, transform.rotation.z);
        }
    }
}