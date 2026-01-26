using UnityEngine;

namespace Boosters
{
    public class Booster : MonoBehaviour
    {
        [SerializeField] private BoostersEnum _type;

        public BoostersEnum Type => _type;
    }
}