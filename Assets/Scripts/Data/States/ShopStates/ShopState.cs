using System;
using System.Collections.Generic;

namespace Data
{
    [Serializable]
    public class ShopState
    {
        public ShopState(
            List<BallState> ballStates,
            List<PlatformState> platformStates,
            BallsEnum currentBall,
            PlatformEnum currentPlatform)
        {
            BallStates = ballStates;
            PlatformStates = platformStates;
            CurrentBall = currentBall;
            CurrentPlatform = currentPlatform;
        }

        public List<BallState> BallStates { get; private set; }
        public List<PlatformState> PlatformStates { get; private set; }
        public BallsEnum CurrentBall { get; private set; }
        public PlatformEnum CurrentPlatform { get; private set; }
    }
}