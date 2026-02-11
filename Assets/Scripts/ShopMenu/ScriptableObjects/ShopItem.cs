using UnityEngine;

namespace ShopMenu
{
    public abstract class ShopItem : ScriptableObject
    {
        [field: SerializeField] public Sprite Image { get; private set; }
        [field: SerializeField] public int Price { get; private set; }
    }
}