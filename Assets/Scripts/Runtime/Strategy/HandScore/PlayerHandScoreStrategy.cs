using Runtime.Abstracts.Classes;
using Runtime.Managers;

namespace Runtime.Strategy.HandScore
{
    public class PlayerHandScoreStrategy : BaseHandScoreStrategy
    {
        protected override void UpdateScoreDisplay()
        {
            int boardScore = GameSettingsManager.Instance.GetCurrentTargetScore();
            
            var colorCode = _currentScore switch
            {
                _ when _currentScore > boardScore => "#FF8D8D", // Red
                _ when _currentScore == boardScore => "#8DFF8D", // Green
                _ => "#FFFFFF" // White
            };
            
            scoreText.text = $"<color={colorCode}>{_currentScore.ToString()}</color>/{boardScore}";
        }
    }
}