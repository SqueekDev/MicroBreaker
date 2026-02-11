using System.Collections.Generic;
using System.Linq;
using Data;

namespace ShopMenu
{
    public class ShopItemUnlocker
    {
        public BallsEnum UnlockBall(List<BallState> ballStates, BallsEnum type)
        {
            BallState currentBallState = ballStates.First(state => state.Type == type);
            currentBallState.SetAvaliableStatus(true);
            return currentBallState.Type;
        }

        public PlatformEnum UnlockPlatform(List<PlatformState> platformStates, PlatformEnum type)
        {
            PlatformState currentPlatformState = platformStates.First(state => state.Type == type);
            currentPlatformState.SetAvaliableStatus(true);
            return currentPlatformState.Type;
        }
    }
}