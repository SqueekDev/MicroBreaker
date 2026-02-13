using TMPro;
using UnityEngine;

namespace MainMenu
{
    public class UnlockedLevelView : LockedLevelView
    {
        [SerializeField] private TMP_Text _status;
        [SerializeField] private TMP_Text _maxScore;
        [SerializeField] private LoadLevelButton _loadLevelButton;

        public override void Init(LevelView levelView)
        {
            base.Init(levelView);
            _status.enabled = levelView.IsCompleted;
            _maxScore.text = levelView.HightScore.ToString();
            _loadLevelButton.SetLevelNumber(levelView.Number);
        }
    }
}