using System.Collections.Generic;
using UnityEngine;

namespace Level
{
    public class MoveTargetsContainer : MonoBehaviour
    {
        [SerializeField] private List<TargetPoint> _targets;

        public List<TargetPoint> Targets => _targets;
    }
}