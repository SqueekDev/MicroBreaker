using System.Collections.Generic;
using UnityEngine;

namespace ShopMenu
{
    [CreateAssetMenu(fileName = "ShopContent", menuName = "Shop/ShopContent", order = 51)]
    public class ShopContent : ScriptableObject
    {
        [field: SerializeField] public List<BallItem> BallItems { get; private set; }
        [field: SerializeField] public List<PlatformItem> PlatformItems { get; private set; }
    }
}