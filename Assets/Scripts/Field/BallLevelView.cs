using Data;
using UnityEngine;

namespace Field
{
    public class BallLevelView : ItemLevelView
    {
        [SerializeField] private BallsEnum _type;

        public BallsEnum Type => _type;
    }
}