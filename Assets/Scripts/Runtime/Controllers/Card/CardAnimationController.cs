using System;
using Runtime.Abstracts.Classes;
using Runtime.Data.ValueObject.CardAnim;
using Runtime.Abstracts.Interfaces;
using Runtime.Strategy;
using Runtime.Strategy.CardAnimation;
using UnityEngine;

namespace Runtime.Controllers.Card
{
    public class CardAnimationController : MonoBehaviour, ICardAnimationStrategy
    {
        private CardAnimationData _data;
        
        private Transform _cardVisualTransform;
        private ICardAnimationStrategy _animationStrategy;
        
        public void SetData(CardAnimationData data)
        {
            _data = data;
            _cardVisualTransform = transform.GetChild(0).transform;
        }

        public void SetCardData(Abstracts.Classes.Card cardData, CardObject cardObject)
        {
            Animator animator = GetComponent<Animator>();
            _animationStrategy = cardData is SpecialCard
                ? new SpecialCardAnimationStrategy(_cardVisualTransform, _data, animator, cardObject)
                : new NormalCardAnimationStrategy(_cardVisualTransform, _data, animator);
        }
        
        public void OnCardSpawn()
        {
            _animationStrategy.OnCardSpawn();
        }

        public void OnCardDespawn()
        {
            _animationStrategy.OnCardDespawn();
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