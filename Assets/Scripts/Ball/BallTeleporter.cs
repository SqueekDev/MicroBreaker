using Field;
using UnityEngine;

namespace Ball
{
    public class BallTeleporter : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PortalBorder mirror))
            {
                if (mirror.IsReflecting == false)
                {
                    return;
                }

                if (mirror.IsVertical)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, mirror.ConnectedMirrorPosition.z);
                }
                else
                {
                    transform.position = new Vector3(mirror.ConnectedMirrorPosition.x, transform.position.y, transform.position.z);
                }
            }
        }
    }
}