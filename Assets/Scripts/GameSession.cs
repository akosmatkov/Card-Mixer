using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using CardMixer.Cards;

namespace CardMixer.Core
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] GameObject[] cards;
        [SerializeField] GameObject nextTurnButton = null;
        [SerializeField] Transform[] spawnPositions;
        [SerializeField] float timeBetweenCardOpen = 0.5f;
        [SerializeField] Image[] opennedAcesOrder;
        [SerializeField] Text[] opennedAcesTurnText;
        [SerializeField] Text currentTurnText;

        GameObject[] spawnedCards = new GameObject[52];
        GameObject[] verticalRow;
        GameObject[] horizontalRow;


        private int opennedCardsCounter = 0;
        private int opennedAcesCounter = 0;
        private int currentTurn = 0;
        private int speedUpButtonPressCounter = 0;
        private float tempTimeBetweenOpen = 0;
        private bool horizontalRowOpenned = false;
        private bool verticalRowOpenned = false;
        private bool commonCardHighlighted = false;
        private bool cardsSpawned = false;

        private void Update()
        {
            if (verticalRow != null && horizontalRow != null)
            {
                HighLightCommonCard();
            }
            if (currentTurn == spawnedCards.Length)
            {
                if (nextTurnButton != null)
                {
                    nextTurnButton.SetActive(true);
                }
            }
        }

        public void OpenAllCards()
        {
            StartCoroutine(OpenCardCoroutine());
        }

        public void InstantCardsOpenning()
        {
            if (cardsSpawned)
            {
                timeBetweenCardOpen = 0;
            }
        }

        public void SpeedUpOpenning()
        {
            switch (speedUpButtonPressCounter)
            {
                case 0: //ускорение в 2 раза
                    tempTimeBetweenOpen = timeBetweenCardOpen;
                    timeBetweenCardOpen /= 2;
                    speedUpButtonPressCounter++;

                    break;

                case 1: //ускорение в 5 раз
                    timeBetweenCardOpen = tempTimeBetweenOpen;
                    timeBetweenCardOpen /= 5;
                    speedUpButtonPressCounter++;

                    break;

                case 2: //возвращение к обычной скорости
                    timeBetweenCardOpen = tempTimeBetweenOpen;
                    speedUpButtonPressCounter = 0;

                    break;
            }
        }

        IEnumerator OpenCardCoroutine()
        {
            while (opennedCardsCounter < spawnedCards.Length)
            {
                int randomCardIndex = UnityEngine.Random.Range(0, cards.Length);
                var card = spawnedCards[randomCardIndex].GetComponent<Card>();


                if (!card.CheckIsOpennedBool())
                {
                    card.OpenCard();
                    card.SetIsOpennedBool(true);

                    opennedCardsCounter++;

                    yield return new WaitForSeconds(timeBetweenCardOpen);
                }
            }
        }

        public void UpdateCurrentTurn()
        {
            currentTurn++;
            if (currentTurnText != null)
            {
                currentTurnText.text = currentTurn.ToString();
            }
        }

        private void ShuffleCards()
        {
            for (int i = 0; i < cards.Length; i++)
            {
                var temp = cards[i];

                int randomCardIndex = UnityEngine.Random.Range(0, cards.Length);

                cards[i] = cards[randomCardIndex];
                cards[randomCardIndex] = temp;
            }
        }

        public void SpawnCards()
        {
            if (!cardsSpawned)
            {
                ShuffleCards();

                for (int i = 0; i < cards.Length; i++)
                {
                    spawnedCards[i] = Instantiate(cards[i], spawnPositions[i].position, Quaternion.identity);
                    spawnedCards[i].transform.SetParent(spawnPositions[i]);
                }
                cardsSpawned = true;
            }
        }

        public void RefreshAcesOrder(Sprite aceSuit)
        {
            opennedAcesOrder[opennedAcesCounter].sprite = aceSuit;
            opennedAcesCounter++;
        }

        public void SetAceOpenningTurnText()
        {
            opennedAcesTurnText[opennedAcesCounter].text = currentTurn.ToString();
        }

        private void HighLightCommonCard()
        {
            if (!commonCardHighlighted)
            {
                foreach (var horRowCard in horizontalRow)
                {
                    if (Array.IndexOf(verticalRow, horRowCard) < 0)
                    {
                        continue;
                    }
                    else
                    {
                        horRowCard.GetComponentInChildren<Animator>().SetTrigger("isCommonCard");
                    }
                }
            }
            else return;
        }

        public void SetOpennedRowBool(RowType rowType, GameObject[] row)
        {
            if (rowType == RowType.Vertical)
            {
                if (!verticalRowOpenned)
                {
                    verticalRowOpenned = true;
                    verticalRow = row;

                    foreach (var pos in verticalRow)
                    {
                        pos.GetComponentInChildren<Card>().HighlightCard();
                    }
                }
            }
            else if (rowType == RowType.Horizontal)
            {
                if (!horizontalRowOpenned)
                {
                    horizontalRowOpenned = true;
                    horizontalRow = row;

                    foreach (var pos in horizontalRow)
                    {
                        pos.GetComponentInChildren<Card>().HighlightCard();
                    }
                }
            }
        }
    }
}
