using Field;
using UnityEngine;

namespace Controller
{
    public class CubeController : MonoBehaviour
    {
        [SerializeField] private BaseCube _cube;
        [SerializeField] private DeadZone _deadZone;


        private void OnEnable()
        {
            _deadZone.Activated += OnDeadZoneActivated;
        }

        private void OnDisable()
        {
            _deadZone.Activated -= OnDeadZoneActivated;
        }

        private void OnDeadZoneActivated()
        {
            _cube.gameObject.SetActive(true);
            _cube.Triggered += OnCubeTriggered;
            _cube.transform.position = transform.position;
            _cube.transform.rotation = transform.rotation;
        }

        private void OnCubeTriggered()
        {
            _cube.Triggered -= OnCubeTriggered;
            _cube.gameObject.SetActive(false);
        }
    }
}