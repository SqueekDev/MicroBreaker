using UnityEngine;

namespace Level
{
    public class LevelTimeCounter : MonoBehaviour
    {
        [SerializeField] private LevelStarter _starter;
        [SerializeField] private LevelFinisher _finisher;

        private bool _isCounting = false;

        public float TimePassed { get; private set; }

        private void OnEnable()
        {
            _starter.Started += OnLevelStarted;
            _finisher.Finished += OnLevelFinished;
        }

        private void Update()
        {
            if (_isCounting)
            {
                TimePassed += Time.deltaTime;
            }
        }

        private void OnDisable()
        {
            _starter.Started -= OnLevelStarted;
            _finisher.Finished -= OnLevelFinished;
        }

        private void OnLevelStarted()
        {
            TimePassed = 0;
            _isCounting = true;
        }

        private void OnLevelFinished()
        {
            _isCounting = false;
        }
    }
}