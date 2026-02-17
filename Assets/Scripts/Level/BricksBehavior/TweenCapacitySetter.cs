using DG.Tweening;
using UnityEngine;

namespace Level
{
    public class TweenCapacitySetter : MonoBehaviour
    {
        [SerializeField] private int _maxTweenersCapacity = 200;
        [SerializeField] private int _maxSequencesCapacity = 50;

        private void Awake()
        {
            DOTween.Init().SetCapacity(_maxTweenersCapacity, _maxSequencesCapacity);
        }
    }
}