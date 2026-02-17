using TMPro;
using UnityEngine;

namespace Field
{
    public class BricksCounterView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _bricks;
        [SerializeField] private BricksActionsInvoker _invoker;

        private int _count;

        private void Awake()
        {
            _count = 0;
            _bricks.text = _count.ToString();
        }

        private void OnEnable()
        {
            _invoker.Smashed += OnBrickSmashed;
        }

        private void OnDisable()
        {
            _invoker.Smashed -= OnBrickSmashed;
        }

        private void OnBrickSmashed(BaseBrick brick)
        {
            _count++;
            _bricks.text = _count.ToString();
        }
    }
}