using System;
using System.Collections.Generic;

namespace Data
{
    [Serializable]
    public class LevelStates
    {
        public LevelStates(List<LevelState> levels)
        {
            Levels = levels;
        }

        public List<LevelState> Levels { get; private set; }
    }
}