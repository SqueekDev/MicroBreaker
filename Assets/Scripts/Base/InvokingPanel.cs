using System;

namespace Base
{
    public class InvokingPanel : GamePanel
    {
        public Action Opened;
        public Action Closed;

        private void OnEnable()
        {
            Opened?.Invoke();
        }

        private void OnDisable()
        {
            Closed?.Invoke();
        }
    }
}