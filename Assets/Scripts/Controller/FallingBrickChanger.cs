namespace Controller
{
    public class FallingBrickChanger : BricksChanger
    {
        protected override void OnEnable()
        {
            OnLevelStarted();
            base.OnEnable();
        }
    }
}