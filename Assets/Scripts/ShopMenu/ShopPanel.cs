using System;
using Base;

namespace ShopMenu
{
    public class ShopPanel : GamePanel
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