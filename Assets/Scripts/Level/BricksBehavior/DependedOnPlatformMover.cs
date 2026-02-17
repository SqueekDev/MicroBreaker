using UnityEngine;

namespace Level
{
    public class DependedOnPlatformMover : BaseDependOnPlatform
    {
        protected override void Act(float targetValue)
        {
            transform.position = new Vector3(targetValue, transform.position.y, transform.position.z);
        }
    }
}