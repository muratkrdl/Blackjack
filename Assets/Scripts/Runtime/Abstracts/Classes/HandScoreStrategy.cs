using TMPro;
using UnityEngine;

namespace Runtime.Abstracts.Classes
{
    public abstract class HandScoreStrategy : MonoBehaviour
    {
        [SerializeField] protected TextMeshPro scoreText;
        
        protected int _currentScore;

        protected HandManager _owner;

        private void Awake()
        {
            _owner = GetComponent<HandManager>();
        }

        public void Reset()
        {
            _currentScore = 0;
            UpdateScoreDisplay();
        }
        
        public void IncreaseScore(int value)
        {
            _currentScore += value;
            UpdateScoreDisplay();
        }

        public void DecreaseScore(int value)
        {
            _currentScore -= value;
            UpdateScoreDisplay();
        }

        protected abstract void UpdateScoreDisplay();
        
        public int GetCurrentScore() => _currentScore;
        
    }
}