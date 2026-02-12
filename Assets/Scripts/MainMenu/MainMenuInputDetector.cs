using System;
using UnityEngine;

namespace MainMenu
{
    public class MainMenuInputDetector : MonoBehaviour
    {
        private Touch _touch;

        public event Action<Vector3> TouchBegan;
        public event Action<Vector3> TouchMoved;
        public event Action<Vector3> TouchEdned;

        private void Update()
        {
            if (Input.touchCount <= 0)
            {
                return;
            }

            _touch = Input.GetTouch(0);

            switch (_touch.phase)
            {
                case TouchPhase.Began:
                    TouchBegan?.Invoke(_touch.position);
                    break;
                case TouchPhase.Moved:
                    TouchMoved?.Invoke(_touch.position);
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    TouchEdned?.Invoke(_touch.position);
                    break;
                default:
                    break;
            }
        }
    }
}