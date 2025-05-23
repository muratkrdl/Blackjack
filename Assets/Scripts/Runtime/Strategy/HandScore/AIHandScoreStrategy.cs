using Runtime.Abstracts.Classes;
using Runtime.Managers;

namespace Runtime.Strategy.HandScore
{
    public class AIHandScoreStrategy : BaseHandScoreStrategy
    {
        protected override void UpdateScoreDisplay()
        {
            int boardScore = GameSettingsManager.Instance.GetCurrentTargetScore();
            int showScore = _currentScore - _owner.GetFirstNormalCard().GetCardValue();
            
            scoreText.text = $"?+{showScore.ToString()}/{boardScore}";
        }
    }
}