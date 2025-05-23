using TMPro;
using UnityEngine;

namespace Runtime.Abstracts.Classes
{
    public abstract class BaseHandScoreStrategy : MonoBehaviour
    {
        [SerializeField] protected TextMeshPro scoreText;
        
        protected int _currentScore;

        protected BaseHandManager _owner;

        private void Awake()
        {
            _owner = GetComponent<BaseHandManager>();
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