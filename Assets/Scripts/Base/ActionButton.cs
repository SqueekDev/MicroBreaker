using System;

namespace Base
{
    public class ActionButton : GameButton
    {
        public event Action Clicked;

        protected override void OnButtonClick()
        {
            Clicked?.Invoke();
        }
    }
}