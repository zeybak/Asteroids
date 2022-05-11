using System;
using UnityEngine;

namespace _Asteroids.Scripts.Behaviours
{
    public class CurrentScoreUI : MonoBehaviour
    {
        private TMPro.TextMeshProUGUI _textRef;
        
        private void Start()
        {
            _textRef = GetComponent<TMPro.TextMeshProUGUI>();

            Score.OnCurrentScoreChanged += UpdateCurrentScore;
            
            UpdateCurrentScore();
        }

        private void UpdateCurrentScore()
        {
            if (!_textRef) return;

            _textRef.text = Score.CurrentScore.ToString();
        }

        private void OnDestroy()
        {
            Score.OnCurrentScoreChanged -= UpdateCurrentScore;
        }
    }
}
