using UnityEngine;

namespace Base
{
    public static class PlayerUtilities
    {
        public static float BaseBoosterDurationTime = 6f;
        public static WaitForSeconds BaseBoostersDelay = new WaitForSeconds(BaseBoosterDurationTime);

        public static void CheckCoroutine(Coroutine coroutine, MonoBehaviour behaviour)
        {
            if (coroutine != null)
            {
                behaviour.StopCoroutine(coroutine);
            }
        }
    }
}