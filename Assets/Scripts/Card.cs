using UnityEngine;
using CardMixer.Core;

namespace CardMixer.Cards
{
    enum CardValue
    {
        Ace,
        Other
    }

    public class Card : MonoBehaviour
    {
        [SerializeField] CardValue cardValue;
        [SerializeField] Sprite cardSuit = null;

        private bool isOpenned = false;
        GameSession gameSession;
        Animator animator;
        CardPosition cardPosition;

        private void Start()
        {
            gameSession = FindObjectOfType<GameSession>();
            animator = GetComponent<Animator>();
            cardPosition = GetComponentInParent<CardPosition>();
        }

        public void OpenCard()
        {
            animator.SetTrigger("turnCard");
            //transform.parent.gameObject.GetComponent<CardPosition>().CallCardOpennedDelegate();
            cardPosition.CallCardOpennedDelegate();
            cardPosition.HidePositionOrderCanvas();
        }

        public void CheckCardValue()
        {
            gameSession.UpdateCurrentTurn();

            if (cardValue == CardValue.Ace)
            {
                animator.SetBool("isAce", true);

                if (cardSuit != null)
                {
                    gameSession.SetAceOpenningTurnText();
                    gameSession.RefreshAcesOrder(cardSuit);
                }
            }
        }

        public void HighlightCard()
        {
            if (cardValue != CardValue.Ace)
            {
                animator.SetTrigger("rowOpenned");
            }
        }

        public bool CheckIsOpennedBool()
        {
            return isOpenned;
        }

        public void SetIsOpennedBool(bool b)
        {
            isOpenned = b;
        }
    }
}
