using UnityEngine;

namespace Base
{
    public static class PlayerUtilities
    {
        public static float BaseBoosterDuration = 5f;

        public static void CheckCoroutine(Coroutine coroutine, MonoBehaviour behaviour)
        {
            if (coroutine != null)
            {
                behaviour.StopCoroutine(coroutine);
            }
        }
    }
}