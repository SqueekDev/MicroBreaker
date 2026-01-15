namespace Boosters
{
    public class PlatformSizeChanger : BaseSizeChanger
    {
        protected override void OnEnable()
        {
            Notifier.PlatformSizeIncreased += OnSizeIncreased;
            Notifier.PlatformSizeDecreased += OnSizeDecreased;
            base.OnEnable();
        }

        protected override void OnDisable()
        {
            Notifier.PlatformSizeIncreased -= OnSizeIncreased;
            Notifier.PlatformSizeDecreased -= OnSizeDecreased;
            base.OnDisable();
        }
    }
}