using Data;
using UnityEngine;

namespace Field
{
    public class PlatformLevelView : ItemLevelView
    {
        [SerializeField] private PlatformEnum _type;

        public PlatformEnum Type => _type;
    }
}