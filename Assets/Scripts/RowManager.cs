using UnityEngine;
using CardMixer.Cards;

namespace CardMixer.Core
{
    public enum RowType
    {
        Horizontal,
        Vertical
    }

    public class RowManager : MonoBehaviour
    {
        [SerializeField] GameObject[] cardPositiosRow;
        [SerializeField] RowType rowType;

        private int rowOpennedCardsCounter = 0;
        GameSession gameSession;

        void Start()
        {
            gameSession = FindObjectOfType<GameSession>();

            foreach (var cardPos in cardPositiosRow)
            {
                cardPos.GetComponent<CardPosition>().onCardOpenned += UpdateCardCouter;
            }
        }

        private void UpdateCardCouter()
        {
            rowOpennedCardsCounter++;

            if (rowOpennedCardsCounter == cardPositiosRow.Length)
            {
                gameSession.SetOpennedRowBool(rowType, cardPositiosRow);
            }
        }
    }
}
