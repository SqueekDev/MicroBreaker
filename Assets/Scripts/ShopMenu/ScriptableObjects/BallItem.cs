using Data;
using UnityEngine;

namespace ShopMenu
{
    [CreateAssetMenu(fileName = "BallItem", menuName = "Shop/BallItem", order = 51)]
    public class BallItem : ShopItem
    {
        [field: SerializeField] public BallsEnum Type { get; private set; }
    }
}