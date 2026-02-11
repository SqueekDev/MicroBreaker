using System;

namespace Data
{
    [Serializable]
    public class BallState : ItemState
    {
        public BallState(bool isUnlocked, BallsEnum type) : base(isUnlocked)
        {
            Type = type;
        }

        public BallsEnum Type { get; private set; }
    }
}