using System;

namespace Data
{
    [Serializable]
    public class ItemState
    {
        public ItemState(bool isUnlocked)
        {
            IsUnlocked = isUnlocked;
        }

        public bool IsUnlocked { get; private set; }

        public void SetAvaliableStatus(bool isUnlocked)
        {
            IsUnlocked = isUnlocked;
        }
    }
}