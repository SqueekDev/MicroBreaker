using Base;
using UnityEngine;

namespace Level
{
    public class TimeStopPanel : GamePanel
    {
        private void OnEnable()
        {
            Time.timeScale = 0;
        }

        private void OnDisable()
        {
            Time.timeScale = 1;
        }
    }
}