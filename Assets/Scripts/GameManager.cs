using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject gameOverPanel;
    public GameObject pauseMenu;
    public static bool isPaused = false;
    public static bool gameOver = false;
    [SerializeField] private AudioClip CrashSFX;

    void Awake()
    {
        Instance = this;
        Application.targetFrameRate = 60;
    }

    public void GameOver()
    {
        ScoreManager.Instance.SaveHighScore();
        Time.timeScale = 0f;
         SoundEffectManager.instance.PlaySoundEffect(CrashSFX, transform, 1f);
        if (gameOverPanel) 
        {
            gameOverPanel.SetActive(true);
        }
        
        gameOver = true;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameOver = false;
    }

    void Update()
    {
        if((Keyboard.current.escapeKey.wasPressedThisFrame || Keyboard.current.pKey.wasPressedThisFrame) && !gameOver)
        {
            if(isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }    
}