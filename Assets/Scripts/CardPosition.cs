using System;
using UnityEngine;

namespace CardMixer.Cards
{
    public class CardPosition : MonoBehaviour
    {
        public event Action onCardOpenned;

        public void CallCardOpennedDelegate()
        {
            onCardOpenned();
        }

        public void HidePositionOrderCanvas()
        {
            GetComponentInChildren<Canvas>().enabled = false;
        }
    }
}
