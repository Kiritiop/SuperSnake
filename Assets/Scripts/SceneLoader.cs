using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
        if (sceneIndex == 1)
        {
            GameManager.gameOver = false;
            GameManager.isPaused = false;
            Time.timeScale = 1f;
        }
    }
}
