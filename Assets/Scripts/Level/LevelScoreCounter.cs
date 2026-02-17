using System.Collections.Generic;
using Field;
using Platform;
using UnityEngine;

namespace Level
{
    public class LevelScoreCounter : MonoBehaviour
    {
        [SerializeField] private LevelStateChanger _levelStateChanger;
        [SerializeField] private List<FragmentsCollector> _fragmentsCollectors;
        [SerializeField] private List<BonusBrick> _bonusBricks;
        [SerializeField] private BricksActionsInvoker _bricksInvoker;
        [SerializeField] private DestroyedBricksCounter _destroyedBricksCounter;
        [SerializeField] private LevelFinisher _levelFinisher;

        public bool IsBeated { get; private set; }
        public int Score { get; private set; }
        public int FragmentsCollected { get; private set; }

        private void OnEnable()
        {
            foreach (var item in _fragmentsCollectors)
            {
                item.Picked += OnFragmentPicked;
            }

            foreach (var item in _bonusBricks)
            {
                item.Destroyed += OnBonusBrickDestroyed;
            }

            _bricksInvoker.Smashed += OnBrickSmashed;
            _levelFinisher.Finished += OnLevelFinished;
        }

        private void OnDisable()
        {
            foreach (var item in _fragmentsCollectors)
            {
                item.Picked -= OnFragmentPicked;
            }

            foreach (var item in _bonusBricks)
            {
                item.Destroyed -= OnBonusBrickDestroyed;
            }

            _bricksInvoker.Smashed -= OnBrickSmashed;
            _levelFinisher.Finished -= OnLevelFinished;
        }

        private void OnFragmentPicked(Fragment fragment)
        {
            FragmentsCollected++;
            Score += fragment.Score;
        }

        private void OnBrickSmashed(BaseBrick brick)
        {
            Score += brick.Value;
        }

        private void OnBonusBrickDestroyed(int score, int money)
        {
            Score += score;
        }

        private void OnLevelFinished()
        {
            IsBeated = Score > _levelStateChanger.HightScore;
        }
    }
}