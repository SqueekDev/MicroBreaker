using System;
using UnityEngine;

namespace Controller
{
    public class InputDetector : MonoBehaviour
    {
        private Touch _touch;

        public event Action<Touch> Detected;
        public event Action Ended;

        private void Update()
        {
            if (Input.touchCount > 0)
            {
                _touch = Input.GetTouch(0);

                if (_touch.phase == TouchPhase.Ended || _touch.phase == TouchPhase.Canceled)
                {
                    Ended?.Invoke();
                }
                else
                {
                    Detected?.Invoke(_touch);
                }
            }
        }
    }
}
