using System;
using Field;
using Platform;
using TMPro;
using UnityEngine;

namespace Level
{
    public class WinPanelView : MonoBehaviour
    {
        private const int PercentageConverter = 100;
        private const int MinuteConverter = 60;
        private const int SingleDigitChecker = 10;
        private const int Zero = 0;

        [SerializeField] private LevelTimeCounter _timeCounter;
        [SerializeField] private FragmentsSpawner _fragmentsSpawner;
        [SerializeField] private DestroyedBricksCounter _bricksCounter;
        [SerializeField] private LevelScoreCounter _scoreCounter;
        [SerializeField] private TMP_Text _time;
        [SerializeField] private TMP_Text _fragments;
        [SerializeField] private TMP_Text _bricks;
        [SerializeField] private TMP_Text _score;
        [SerializeField] private TMP_Text _money;
        [SerializeField] private TMP_Text _newRecordText;

        private void OnEnable()
        {
            _time.text = GetFormattedTime(_timeCounter.TimePassed);
            _fragments.text = GetFragmentsPercentage();
            _bricks.text = _bricksCounter.SmashedCounter.ToString();
            _score.text = _scoreCounter.Score.ToString();
            _newRecordText.enabled = _scoreCounter.IsBeated;
        }

        private string GetFragmentsPercentage()
        {
            if (_scoreCounter.FragmentsCollected <= 0)
            {
                return Zero.ToString();
            }

            int percentage = _scoreCounter.FragmentsCollected * PercentageConverter/ _fragmentsSpawner.SpawnedNumber;
            string percentageText = $"{percentage}%";
            return percentageText;
        }

        private string GetFormattedTime(float time)
        {
            if (time <= 0)
            {
                return Zero.ToString();
            }

            int convertedTime = Convert.ToInt32(time);
            int minutes = convertedTime / MinuteConverter;
            int seconds = convertedTime % MinuteConverter;
            string minutesString = CheckDigites(minutes);
            string secondsString = CheckDigites(seconds);
            string formattedString = $"{minutesString}:{secondsString}";
            return formattedString;
        }

        private string CheckDigites(int number)
        {
            string correctedString;

            if (number < SingleDigitChecker)
            {
                correctedString = $"{Zero}{number}";
            }
            else
            {
                correctedString = number.ToString();
            }

            return correctedString;
        }
    }
}