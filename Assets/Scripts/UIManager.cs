using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardMixer.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] GameObject informationPanel = null;

        public void ShowInfoPanel()
        {
            if (informationPanel != null)
            {
                informationPanel.SetActive(true);
            }
        }

        public void HideInfoPanel()
        {
            if (informationPanel != null)
            {
                informationPanel.SetActive(false);
            }
        }
    }
}
