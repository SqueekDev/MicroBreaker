using System.Collections.Generic;
using System.Linq;
using Base;
using Data;
using UnityEngine;

namespace Field
{
    public class LevelViewInitializer : MonoBehaviour
    {
        [SerializeField] private List<Container> _ballContainers;
        [SerializeField] private List<Container> _platformContainers;
        [SerializeField] private List<BallLevelView> _balls;
        [SerializeField] private List<PlatformLevelView> _platforms;

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            ShopState shopState = SaveSystem.LoadShopState();
            BallLevelView ballView = _balls.First(view => view.Type == shopState.CurrentBall);
            InstantiatePrefabs(ballView, _ballContainers);
            PlatformLevelView platformView = _platforms.First(view => view.Type == shopState.CurrentPlatform);
            InstantiatePrefabs(platformView, _platformContainers);
        }

        private void InstantiatePrefabs(ItemLevelView prefab, List<Container> containers)
        {
            foreach (var container in containers)
            {
                Instantiate(prefab, container.transform);
            }
        }
    }
}