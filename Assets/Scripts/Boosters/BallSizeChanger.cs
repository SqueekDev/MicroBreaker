namespace Boosters
{
    public class BallSizeChanger : BaseSizeChanger
    {
        protected override void OnEnable()
        {
            Notifier.BallSizeIncreased += OnSizeIncreased;
            Notifier.BallSizeDecreased += OnSizeDecreased;
            base.OnEnable();
        }

        protected override void OnDisable()
        {
            Notifier.BallSizeIncreased -= OnSizeIncreased;
            Notifier.BallSizeDecreased -= OnSizeDecreased;
            base.OnDisable();
        }
    }
}