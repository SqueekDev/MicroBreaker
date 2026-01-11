using Controller;
using UnityEngine;

namespace Field
{
    public class FortifiedCube : BaseCube
    {
        private bool _isDestroyable;

        private void OnEnable()
        {
            _isDestroyable = false;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_isDestroyable && collision.transform.TryGetComponent(out Ball ball))
            {
                Triggered?.Invoke();
            }
        }
    }
}