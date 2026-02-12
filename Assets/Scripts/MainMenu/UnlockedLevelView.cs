using TMPro;
using UnityEngine;

namespace MainMenu
{
    public class UnlockedLevelView : LockedLevelView
    {
        [SerializeField] private TMP_Text _status;
        [SerializeField] private TMP_Text _maxScore;

        public override void Init(LevelView levelView)
        {
            base.Init(levelView);
            _status.enabled = levelView.IsCompleted;
            _maxScore.text = levelView.HightScore.ToString();
        }
    }
}