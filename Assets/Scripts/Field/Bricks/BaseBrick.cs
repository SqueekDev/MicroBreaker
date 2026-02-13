using UnityEngine;

namespace Field
{
    public class BaseBrick : MonoBehaviour
    {
        [SerializeField] private int _value;

        public int Value => _value;
    }
}