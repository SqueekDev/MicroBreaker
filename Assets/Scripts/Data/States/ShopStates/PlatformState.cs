using System;

namespace Data
{
    [Serializable]
    public class PlatformState : ItemState
    {
        public PlatformState(bool isUnlocked, PlatformEnum type) : base(isUnlocked)
        {
            Type = type;
        }

        public PlatformEnum Type { get; private set; }
    }
}