using System;
using UnityEngine;

namespace _Asteroids.Scripts
{
    public static class Score
    {
        private static int _currentScore;
        public static int CurrentScore
        {
            get => _currentScore;
            set
            {
                _currentScore = value;
                OnCurrentScoreChanged?.Invoke();
            }
        }

        private const string HighScorePrefsName = "HighScore";
        public static int HighScore
        {
            get => PlayerPrefs.HasKey(HighScorePrefsName) ? PlayerPrefs.GetInt(HighScorePrefsName) : 0;
            set => PlayerPrefs.SetInt(HighScorePrefsName, value);
        }

        public static event Action OnCurrentScoreChanged;
    }
}