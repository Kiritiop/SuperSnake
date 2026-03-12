using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Use this method to load a scene by its name
    // Use this method to load a scene by its build index
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
