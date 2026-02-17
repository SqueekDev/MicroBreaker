using UnityEngine;

namespace Field
{
    [RequireComponent(typeof(Rigidbody))]
    public class BaseBrick : MonoBehaviour
    {
        [SerializeField] private int _value;
        [SerializeField] private Rigidbody _rigidbody;

        public int Value => _value;
        public Rigidbody Rigidbody => _rigidbody;
    }
}