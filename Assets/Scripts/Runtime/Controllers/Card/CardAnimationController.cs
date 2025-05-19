using Runtime.Abstracts.Classes;
using Runtime.Data.ValueObject.CardAnim;
using Runtime.Abstracts.Interfaces;
using Runtime.Strategy;
using UnityEngine;

namespace Runtime.Controllers.Card
{
    public class CardAnimationController : MonoBehaviour
    {
        private CardAnimationData _data;
        
        private Transform _cardVisualTransform;
        private ICardAnimationStrategy _animationStrategy;

        public void SetData(CardAnimationData data)
        {
            _data = data;
            _cardVisualTransform = transform.GetChild(0).transform;
        }

        public void SetCardData(Abstracts.Classes.Card cardData)
        {
            _animationStrategy = cardData is SpecialCard
                ? new SpecialCardAnimationStrategy(_cardVisualTransform, _data)
                : new NormalCardAnimationStrategy(_cardVisualTransform, _data);
        }
        
        public void OnCardSpawn()
        {
            _animationStrategy.OnCardSpawn();
        }
        
        public void OnPointerEnter()
        {
            _animationStrategy.OnPointerEnter();
        }

        public void OnPointerExit()
        {
            _animationStrategy.OnPointerExit();
        }
    }
}