using System;
using Newtonsoft.Json;

namespace Data
{
    [Serializable]
    public class LevelState
    {
        public LevelState(LevelsEnum number, bool isUnlocked, bool isCompleted, int score)
        {
            Number = number;
            IsUnlocked = isUnlocked;
            IsCompleted = isCompleted;
            HightScore = score;
        }

        public LevelsEnum Number { get; private set; }
        public bool IsUnlocked { get; private set; }
        public bool IsCompleted { get; private set; }
        [JsonProperty]
        public int HightScore { get; private set; }

        public void SetUnlockedStatus(bool isUnlocked)
        {
            IsUnlocked = isUnlocked;
        }

        public void SetCompletedStatus(bool isCompleted)
        {
            IsCompleted = isCompleted;
        }

        public void SetHightScore(int score)
        {
            HightScore = score;
        }
    }
}