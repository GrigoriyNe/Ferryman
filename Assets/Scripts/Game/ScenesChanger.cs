using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class ScenesChanger : MonoBehaviour
    {
        public void ReastartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}