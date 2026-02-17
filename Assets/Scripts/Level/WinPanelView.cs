using System;
using TMPro;
using UnityEngine;

namespace Level
{
    public class WinPanelView : LosePanelView
    {
        private const int PercentageConverter = 100;
        private const int MinuteConverter = 60;
        private const int SingleDigitChecker = 10;
        private const int Zero = 0;

        [SerializeField] private LevelTimeCounter _timeCounter;
        [SerializeField] private FragmentsSpawner _fragmentsSpawner;
        [SerializeField] private MoneyEarner _moneyEarner;
        [SerializeField] private TMP_Text _time;
        [SerializeField] private TMP_Text _fragments;
        [SerializeField] private TMP_Text _money;

        protected override void OnEnable()
        {
            base.OnEnable();
            _time.text = GetFormattedTime(_timeCounter.TimePassed);
            _fragments.text = GetFragmentsPercentage();
            _money.text = _moneyEarner.EarnedMoney.ToString();
        }

        private string GetFragmentsPercentage()
        {
            if (ScoreCounter.FragmentsCollected <= 0)
            {
                return Zero.ToString();
            }

            int percentage = ScoreCounter.FragmentsCollected * PercentageConverter/ _fragmentsSpawner.SpawnedNumber;
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