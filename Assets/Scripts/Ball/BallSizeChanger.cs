using Controller;
using Data;

namespace Ball
{
    public class BallSizeChanger : BaseSizeChanger
    {
        protected override void OnBoosterActivated(BoostersEnum type)
        {
            base.OnBoosterActivated(type);

            if (type == BoostersEnum.BallSizeIncreased)
            {
                IncreaseSize();
            }
            else if (type == BoostersEnum.BallSizeDecreased)
            {
                DecreaseSize();
            }
        }
    }
}