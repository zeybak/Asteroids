using System;

namespace _Asteroids.Scripts
{
    public static class Score
    {
        public static int CurrentScore { get; private set; } = 0;

        public static event Action OnCurrentScoreChanged;

        public static void AddScore(int scoreToAdd)
        {
            CurrentScore += scoreToAdd;
            
            OnCurrentScoreChanged?.Invoke();
        }
    }
}