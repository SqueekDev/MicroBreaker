using Controller;
using UnityEngine;

namespace Field
{
    public class NormalCube : BaseCube
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.TryGetComponent(out BallMover ball))
            {
                Triggered?.Invoke();
            }
        }
    }
}