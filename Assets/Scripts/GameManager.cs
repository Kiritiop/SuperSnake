using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject gameOverPanel;

    void Awake() => Instance = this;

    public void GameOver()
    {
        ScoreManager.Instance.SaveHighScore();
        Time.timeScale = 0f;
        if (gameOverPanel) gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}