using Data;
using UnityEngine;

namespace ShopMenu
{
    [CreateAssetMenu(fileName = "PlatformItem", menuName = "Shop/PlatformItem", order = 51)]
    public class PlatformItem : ShopItem
    {
        [field: SerializeField] public PlatformEnum Type { get; private set; }
    }
}