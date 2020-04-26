using UnityEngine;
using UnityEngine.SceneManagement;

namespace CardMixer.SceneManagement
{
    public class LevelLoader : MonoBehaviour
    {
        public void RestartCurrentScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
