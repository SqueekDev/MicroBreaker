using TMPro;
using UnityEngine;

namespace MainMenu
{
    public class LockedLevelView : MonoBehaviour
    {
        private const char IncomingChar = '_';
        private const char ReplaceChar = ' ';

        [SerializeField] private TMP_Text _number;

        public virtual void Init(LevelView level)
        {
            string text = level.Number.ToString();
            string newText = text.Replace(IncomingChar, ReplaceChar);
            _number.text = newText;
        }
    }
}