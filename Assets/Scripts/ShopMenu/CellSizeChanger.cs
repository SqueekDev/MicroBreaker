using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShopMenu
{
    public class CellSizeChanger : MonoBehaviour
    {
        private const float Ratio = 1.78f;

        [SerializeField] private List<GridLayoutGroup> _layoutGpoups;

        private void Awake()
        {
            float screenWidth = Screen.width;
            float screenHeight = Screen.height;

            if (screenWidth > screenHeight)
            {
                foreach (var item in _layoutGpoups)
                {
                    item.cellSize = new Vector2(item.cellSize.x * Ratio, item.cellSize.y);
                }
            }
        }
    }
}