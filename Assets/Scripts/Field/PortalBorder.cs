using System.Collections;
using System;
using Base;
using Ball;
using UnityEngine;

namespace Field
{
    public class PortalBorder : MonoBehaviour
    {
        private const float DelayTime = 0.1f;

        [SerializeField] private PortalBorder _connectedMirror;

        private Coroutine _changeReflectivityCoroutine;
        private WaitForSeconds _delay = new WaitForSeconds(DelayTime);

        public Action Triggered;

        public Vector3 ConnectedMirrorPosition => _connectedMirror.transform.position;
        public bool IsVertical { get; private set; }
        public bool IsReflecting { get; private set; }

        private void Awake()
        {
            if (transform.position.x == _connectedMirror.transform.position.x)
            {
                IsVertical = true;
            }
            else
            {
                IsVertical = false;
            }
        }

        private void OnEnable()
        {
            IsReflecting = true;
            _connectedMirror.Triggered += OnTriggered;
        }

        private void OnDisable()
        {
            _connectedMirror.Triggered -= OnTriggered;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out BallTeleporter ball))
            {
                Triggered?.Invoke();
            }
        }

        private void OnTriggered()
        {
            PlayerUtilities.CheckCoroutine(_changeReflectivityCoroutine, this);
            _changeReflectivityCoroutine = StartCoroutine(ChangeReflectivity());
        }

        private IEnumerator ChangeReflectivity()
        {
            IsReflecting = false;
            yield return _delay;
            IsReflecting = true;
        }
    }
}