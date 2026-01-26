using System;

namespace Controller
{
    public class FallingBrickChanger : BricksChanger
    {
        public Action Destroyed;

        protected override void OnEnable()
        {
            OnLevelStarted();
            base.OnEnable();
        }

        protected override void OnBrickTriggered()
        {
            base.OnBrickTriggered();
            Destroyed?.Invoke();
        }
    }
}