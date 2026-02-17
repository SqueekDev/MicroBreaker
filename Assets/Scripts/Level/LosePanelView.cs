using Field;
using TMPro;
using UnityEngine;

namespace Level
{
    public class LosePanelView : MonoBehaviour
    {
        [SerializeField] private LevelScoreCounter _scoreCounter;
        [SerializeField] private DestroyedBricksCounter _bricksCounter;
        [SerializeField] private TMP_Text _score;
        [SerializeField] private TMP_Text _bricks;
        [SerializeField] private TMP_Text _newRecordText;

        protected LevelScoreCounter ScoreCounter => _scoreCounter;

        protected virtual void OnEnable()
        {
            _score.text = _scoreCounter.Score.ToString();
            _bricks.text = _bricksCounter.SmashedCounter.ToString();
            _newRecordText.enabled = ScoreCounter.IsBeated;
        }
    }
}