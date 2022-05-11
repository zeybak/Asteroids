using UnityEngine;

namespace _Asteroids.Scripts.Behaviours
{
    public class HighScoreUI : MonoBehaviour
    {
        private TMPro.TextMeshProUGUI _textRef;
        
        private void Start()
        {
            _textRef = GetComponent<TMPro.TextMeshProUGUI>();
            
            UpdateHighScore();
        }

        private void UpdateHighScore()
        {
            if (!_textRef) return;

            _textRef.text = Score.HighScore.ToString();
        }
    }
}