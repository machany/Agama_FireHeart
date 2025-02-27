using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.UI
{
    public class StartCanvas : MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.LoadScene("AKH");
        }
        public void Exit()
        {
            Application.Quit();
        }
    }
}
